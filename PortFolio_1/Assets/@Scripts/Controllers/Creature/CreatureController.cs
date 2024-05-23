﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CreatureController : BaseController
{
    protected float _speed = 1.0f;

    public int Hp { get; set; } = 100;
    public int MaxHp { get; set; } = 100;

    public SkillBook Skills { get; protected set; }

    public override bool Init()
    {
        base.Init();

        Skills = gameObject.GetOrAddComponent<SkillBook>();

        return true;
    }

    public virtual void OnDamaged(BaseController attacker, int damage)
    {
        if (Hp <= 0)
        {
            // TODO 
            return;
        }

        Hp -= damage;
        if (Hp <= 0)
        {
            GameObject effect = Managers.Resource.Instantiate("DieEffect.prefab");
            effect.transform.position = transform.position;
            effect.GetComponent<Animator>().Play("Die");
            GameObject.Destroy(effect, 1.0f);

            Hp = 0;
            OnDead();
        }
    }

    protected virtual void OnDead()
    {

    }
}

