using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectManager
{
    public PlayerController Player { get; private set; }
    public HashSet<PlayerController> Players { get; } = new HashSet<PlayerController>();
    public HashSet<MonsterController> Monsters { get; } = new HashSet<MonsterController>();
    
    public T Spawn<T>(Vector3 position, int templateID = 0, string prefabName = "") where T : BaseController
    {
        Type type = typeof(T);
        
        if( type == typeof(PlayerController))
        {
            // TODO : Data
            // ���߿� ������ ��Ʈ�� ���������� �� �ش��ϴ� ���ø� ���̵�
            // �ش��ϴ� �������� ��������� ������ üũ�� ���ݴϴ�.
            GameObject go = Managers.Resource.Instantiate("Player.prefab", pooling: true);
            go.name = "Player";
            go.transform.position = position;

            PlayerController pc = go.GetOrAddComponent<PlayerController>();
            Player = pc;
            pc.Init();

            return pc as T;
        }
        else if (type == typeof(MonsterController))
        {
            string name = "";

            switch (templateID)
            {
                case Define.GOBLIN_ID:
                    name = "Goblin_01_1";
                    break;
                case Define.SNAKE_ID:
                    name = "Snake_01";
                    break;
                case Define.SLIME_ID:
                    name = "Slime_01_1";
                    break;
            };

            GameObject go = Managers.Resource.Instantiate(name + ".prefab", pooling: true);
            go.transform.position = position;

            MonsterController mc = go.GetOrAddComponent<MonsterController>();
            Monsters.Add(mc);
            mc.Init();

            return mc as T;
        }

        return null;
    }

    public void Despanw<T>(T obj) where T: BaseController
    {
        if (obj.IsValid() == false)
            return;

        Type type = typeof(T);

        if (type == typeof(PlayerController))
        {
            // ?
        }
        else if (type == typeof(MonsterController) || type.IsSubclassOf(typeof(MonsterController)))
        {
            Monsters.Remove(obj as MonsterController);
            Managers.Resource.Destroy(obj.gameObject);
        }
    }

    public void DespawnAllMonsters()
    {
        var monsters = Monsters.ToList();

        foreach ( var monster in monsters)
            Despanw<MonsterController>(monster);
    }
}
