using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSkill : RepeatSkill
{
    public override bool Init()
    {
        base.Init();

        if (Managers.Data.SkillDic.TryGetValue(200, out Data.SkillData skillData) == true)
            SkillData = skillData;

        Damage = SkillData.damage;

        return true;
    }

    public FireballSkill()
    {

    }

    protected override void DoSkillJob()
    {
        if (Managers.Game.Player == null)
            return;

        Vector3 spawnPos = Managers.Game.Player.FireSocket;
        Vector3 dir = Managers.Game.Player.ShootDir;

        GenerateProjectile(SkillData.templateID, Owner, spawnPos, dir, Vector3.zero);
    }
}
