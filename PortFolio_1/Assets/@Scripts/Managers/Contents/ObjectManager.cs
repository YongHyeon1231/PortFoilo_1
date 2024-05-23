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
    public HashSet<GemController> Gems { get; } = new HashSet<GemController>();
    public HashSet<ProjectileController> Projectiles { get; } = new HashSet<ProjectileController>();
    
    public T Spawn<T>(Vector3 position, int templateID = 0, string prefabName = "") where T : BaseController
    {
        Type type = typeof(T);
        
        if( type == typeof(PlayerController))
        {
            // TODO : Data
            // 나중에 데이터 시트를 뒤져가지고 이 해당하는 템플릿 아이디에
            // 해당하는 프리팹이 몇번인지를 뒤져서 체크를 해줍니다.
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
                case 20000:
                    name = "Boss_01";
                    break;
                case 20100:
                    name = "Goblin_01_1";
                    break;
                case 20200:
                    name = "Snake_01";
                    break;
                case 20300:
                    name = "Slime_01_1";
                    break;
            };
            GameObject go = Managers.Resource.Instantiate(name + ".prefab", pooling: true);
            go.transform.position = position;

            if (templateID % 10000 == 0)
            {
                BossController bc = go.GetOrAddComponent<BossController>();
                Monsters.Add(bc);
                bc.Init();

                return bc as T;
            }
            Managers.Data.MonsterDic.TryGetValue(name, out Data.MonsterData monsterData);
            MonsterController mc = go.GetOrAddComponent<MonsterController>();
            mc.Hp = monsterData.maxHp;
            Monsters.Add(mc);
            mc.Init();

            return mc as T;
        }
        else if (type == typeof(GemController))
        {
            GameObject go = Managers.Resource.Instantiate("EXPGem.prefab", pooling: true);
            go.transform.position = position;

            GemController gc = go.GetOrAddComponent<GemController>();
            Gems.Add(gc);
            gc.Init();

            string key = UnityEngine.Random.Range(0, 2) == 0 ? "EXPGem_01.sprite" : "EXPGem_02.sprite";
            Sprite sprite = Managers.Resource.Load<Sprite>(key);
            go.GetComponent<SpriteRenderer>().sprite = sprite;

            GameObject.Find("@Grid").GetComponent<GridController>().Add(go);

            return gc as T;
        }
        else if (type == typeof(ProjectileController))
        {
            if (Managers.Data.SkillDic.TryGetValue(templateID, out Data.SkillData skilldata) == false)
            {
                Debug.LogError("ObjectManager Spawn Projectile Failed");
                return null;
            }
            GameObject go = Managers.Resource.Instantiate(skilldata.prefab, pooling: true);
            go.transform.position = position;
            go.name = skilldata.name;

            ProjectileController pc = go.GetOrAddComponent<ProjectileController>();
            Projectiles.Add(pc);
            pc.Init();

            return pc as T;
        }
        else if(typeof(T).IsSubclassOf(typeof(SkillBase)))
        {
            if (Managers.Data.SkillDic.TryGetValue(templateID, out Data.SkillData skillData) == false)
            {
                Debug.LogError($"ObjectManager Spawn Skill Failed {templateID}");
                return null;
            }

            GameObject go = Managers.Resource.Instantiate(skillData.prefab, pooling: false);
            go.transform.position = position;

            T t = go.GetOrAddComponent<T>();
            t.Init();

            return t;
        }

        return null;
    }

    public void Despawn<T>(T obj) where T: BaseController
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
        else if (type == typeof(GemController))
        {
            Gems.Remove(obj as GemController);
            Managers.Resource.Destroy(obj.gameObject);

            GameObject.Find("@Grid").GetComponent<GridController>().Remove(obj.gameObject);
        }
        else if (type == typeof(ProjectileController))
        {
            Projectiles.Remove(obj as ProjectileController);
            Managers.Resource.Destroy(obj.gameObject);
        }
    }

    public void DespawnAllMonsters()
    {
        var monsters = Monsters.ToList();

        foreach ( var monster in monsters)
            Despawn<MonsterController>(monster);
    }
}
