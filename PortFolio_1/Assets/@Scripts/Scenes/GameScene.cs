using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public struct SaveData
{
    GameObject player;
    public int level;
    public int maxHp;
    public int attack;
    public int totalExp;
    string map;
}

public class GameScene : MonoBehaviour
{
    public SaveData SaveDatas;

    private void Start()
    {
        Managers.Resource.LoadAllAsync<Object>("PreLoad", (key, count, totalCount) =>
        {
            Debug.Log($"PreLoad {key} {count}/{totalCount}");

            if (count == totalCount)
            {
                Managers.Resource.LoadAllAsync<TextAsset>("Data", (key1, count1, totalCount1) =>
                {
                    Debug.Log($"Data {key1} {count1}/{totalCount1}");

                    if (count1 == totalCount1)
                    {
                        Managers.Resource.LoadAllAsync<Sprite>("Sprite", (key2, count2, totalCount2) =>
                        {
                            Debug.Log($"Sprite {key2} {count2}/{totalCount2}");

                            if (count2 == totalCount2)
                            {
                                StartLoaded();
                            }
                        });
                    }
                });
            }
        });
    }

    SpawningPool _spawningPool;

    private void StartLoaded()
    {
        Managers.Data.Init();

        SaveDatas = new SaveData();

        foreach (var playerData in Managers.Data.PlayerDic.Values)
        {
            Debug.Log($"Level : {playerData.level}, Hp : {playerData.maxHp}");
        }

        foreach (var monsterData in Managers.Data.MonsterDic.Values)
        {
            Debug.Log($"name : {monsterData.name}, attack : {monsterData.attack}");
        }

        _spawningPool = gameObject.AddComponent<SpawningPool>();

        var player = Managers.Object.Spawn<PlayerController>(Vector3.zero);

        MonsterController mc1 = Managers.Object.Spawn<MonsterController>(new Vector3(Random.Range(-10, 10), 10, 0), 1);
        MonsterController mc2 = Managers.Object.Spawn<MonsterController>(new Vector3(Random.Range(-10, 10), 10, 0), 2);
        MonsterController mc3 = Managers.Object.Spawn<MonsterController>(new Vector3(Random.Range(-10, 10), 10, 0), 3);

        #region Á¶ÀÌ½ºÆ½, ¸Ê, Ä«¸Þ¶ó
        var joystick = Managers.Resource.Instantiate("UI_Joystick.prefab");
        joystick.name = "@UI_Joystick";

        var map = Managers.Resource.Instantiate("@Map.prefab");
        map.name = "@Map";

        Camera.main.GetComponent<CameraController>().Target = player.gameObject;
        #endregion
    }
}
