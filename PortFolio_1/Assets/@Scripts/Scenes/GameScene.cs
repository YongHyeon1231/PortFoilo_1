using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SaveData
{
    GameObject player;
    string map;
}

public class GameScene : MonoBehaviour
{
    private void Start()
    {
        Managers.Resource.LoadAllAsync<Object>("PreLoad", (key, count, totalCount) =>
        {
            Debug.Log($"{key} {count}/{totalCount}");

            if (count == totalCount)
            {
                StartLoaded();
            }
        });
    }

    SpawningPool _spawningPool;

    private void StartLoaded()
    {

        _spawningPool = gameObject.AddComponent<SpawningPool>();

        var player = Managers.Object.Spawn<PlayerController>(Vector3.zero);

        MonsterController mc1 = Managers.Object.Spawn<MonsterController>(new Vector3(Random.Range(-10, 10), 10, 0), 1);
        MonsterController mc2 = Managers.Object.Spawn<MonsterController>(new Vector3(Random.Range(-10, 10), 10, 0), 2);
        MonsterController mc3 = Managers.Object.Spawn<MonsterController>(new Vector3(Random.Range(-10, 10), 10, 0), 3);

        #region Á¶ÀÌ½ºÆ½, ¸Ê, Ä«¸Þ¶ó
        var joystick = Managers.Resource.Instantiate("UI_Joystick.prefab");
        joystick.name = "@UI_Joystick";

        var map = Managers.Resource.Instantiate("Map_Tile_01.prefab");
        map.name = "@Map";

        Camera.main.GetComponent<CameraController>().Target = player.gameObject;
        #endregion
    }
}
