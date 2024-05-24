using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ProjectileController : SkillBase
{
    CreatureController _owner;
    Vector3 _moveDir;
    float _speed = 10.0f;
    float _lifeTime = 5.0f;

    public ProjectileController() : base(Define.SkillType.None)
    {

    }

    public override bool Init()
    {
        base.Init();

        StartDestroy(_lifeTime);

        return true;
    }

    public void SetInfo(int templateID, CreatureController owner, Vector3 moveDIr)
    {
        if (Managers.Data.SkillDic.TryGetValue(templateID, out Data.SkillData data) == false)
        {
            Debug.LogError("ProjectileController SetInfo Failed");
            return;
        }

        _owner = owner;
        _moveDir = moveDIr;
        SkillData = data;
        // TODO : DataParsing
        _speed = SkillData.speed;
        Damage = SkillData.damage;
        SkillType = SkillData.SkillType;
    }

    public override void UpdateController()
    {
        base.UpdateController();

        transform.position += _moveDir * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MonsterController mc = collision.gameObject.GetComponent<MonsterController>();
        if (mc.IsValid() == false)
            return;
        if (this.IsValid() == false)
            return;

        mc.OnDamaged(_owner, SkillData.damage);

        StopDestroy();

        Managers.Object.Despawn(this);
    }
}
