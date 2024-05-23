using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningPool : MonoBehaviour
{
    // 리스폰 주기는?
    // 몬스터 최대 개수는?
    // 스톱?
    float _spawnInterval = 0.1f;
    int _maxMonsterCount = 100;
    Coroutine _coUpdateSpawningPool;
    int _stageLevel = 1;
    public int StageLevel
    {
        get { return _stageLevel; }
        set
        {
            _stageLevel = value;
            Managers.UI.GetSceneUI<UI_GameScene>().SetWaveValueText(value);
        }
    }

    public bool Stopped { get; set; } = false;

    private void Start()
    {
        _coUpdateSpawningPool = StartCoroutine(CoUpdateSpawningPool());
    }

    IEnumerator CoUpdateSpawningPool()
    {
        while (true)
        {
            TrySpawn();
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    private void TrySpawn()
    {
        if (Stopped)
            return;

        int monsterCount = Managers.Object.Monsters.Count;
        if (monsterCount >= _maxMonsterCount)
            return;

        // TEMP : DataID ?
        Vector2 randPos = Utils.GenerateMonsterSpawnPosition(Managers.Game.Player.transform.position, 10, 15);
        MonsterController mc;
        switch (StageLevel)
        {
            case 1:
                mc = Managers.Object.Spawn<MonsterController>(randPos, 20100);
                break;
            case 2:
                mc = Managers.Object.Spawn<MonsterController>(randPos, 20200);
                break;
            case 3:
                mc = Managers.Object.Spawn<MonsterController>(randPos, 20300);
                break;
            case 4:
                //Managers.Object.DespawnAllMonsters();
                mc = Managers.Object.Spawn<MonsterController>(randPos, 20000);
                Stopped = true;
                break;
            case 5:
                Stopped = false;
                mc = Managers.Object.Spawn<MonsterController>(randPos, 20100);
                mc = Managers.Object.Spawn<MonsterController>(randPos, 20200);
                mc = Managers.Object.Spawn<MonsterController>(randPos, 20300);
                break;  
        }
        
    }
}
