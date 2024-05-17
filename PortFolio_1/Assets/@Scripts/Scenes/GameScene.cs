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
    int _stageLevel = 1;

    private void StartLoaded()
    {
        Managers.Data.Init();
        Managers.UI.ShowSceneUI<UI_GameScene>();

        SaveDatas = new SaveData();

        _spawningPool = gameObject.AddComponent<SpawningPool>();

        var player = Managers.Object.Spawn<PlayerController>(Vector3.zero);

        MonsterController mc1 = Managers.Object.Spawn<MonsterController>(new Vector3(Random.Range(-10, 10), 10, 0), 20100);
        MonsterController mc2 = Managers.Object.Spawn<MonsterController>(new Vector3(Random.Range(-10, 10), 10, 0), 20200);
        MonsterController mc3 = Managers.Object.Spawn<MonsterController>(new Vector3(Random.Range(-10, 10), 10, 0), 20300);

        #region 조이스틱, 맵, 카메라
        var joystick = Managers.Resource.Instantiate("UI_Joystick.prefab");
        joystick.name = "@UI_Joystick";

        var map = Managers.Resource.Instantiate("@Map.prefab");
        map.name = "@Map";

        Camera.main.GetComponent<CameraController>().Target = player.gameObject;
        #endregion

        #region CallBack
        Managers.Game.OnKillCountChanged -= HandleOnKillCountChanghed;
        Managers.Game.OnKillCountChanged += HandleOnKillCountChanghed;
        Managers.Game.OnGemCountChanged -= HandleOnGemCountChanged;
        Managers.Game.OnGemCountChanged += HandleOnGemCountChanged;
        #endregion
    }

    #region UI_GameScene
    int _collectedGemCount = 0;
    int _remainingTotalGemCount = 2;

    public void HandleOnGemCountChanged(int gemCount)
    {
        _collectedGemCount++;

        // 스킬 레벨업
        if (_collectedGemCount == _remainingTotalGemCount)
        {
            // Managers.UI.ShowPopup<UI_SkillSelectPopup>();
            _collectedGemCount = 0;
            _remainingTotalGemCount *= 2;
            _stageLevel += 1;
            _spawningPool.StageLevel = _stageLevel;
            Managers.UI.ShowPopup<UI_SkillSelectPopup>();
        }

        Managers.UI.GetSceneUI<UI_GameScene>().SetGemCountRatio((float)_collectedGemCount / _remainingTotalGemCount);
    }

    public void HandleOnKillCountChanghed(int killCount)
    {
        Managers.UI.GetSceneUI<UI_GameScene>().SetKillCount(killCount);

        if (killCount == 10)
        {
            // Boss or change stage

            /*Managers.Object.DespawnAllMonsters();

            Vector2 spawnPos = Utils.GenerateMonsterSpawnPosition(Managers.Game.Player.transform.position, 5, 10);

            Managers.Object.Spawn<MonsterController>(spawnPos, 20000);*/
        }
    }
    #endregion

    private void OnDestroy()
    {
        if (Managers.Game != null)
            Managers.Game.OnGemCountChanged -= HandleOnGemCountChanged;
    }
}
