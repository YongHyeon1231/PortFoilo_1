using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonsterController
{
    public override bool Init()
    {
        base.Init();

        _animator = GetComponent<Animator>();
        CreatureState = Define.CreatureState.Moving;
        if (Managers.Data.MonsterDic.TryGetValue("Boss_01", out Data.MonsterData boss) == false)
        {
            Debug.Log("BossController Error");
            return true;
        }
        Hp = boss.maxHp;
        Debug.Log($"BossMonster Hp : {Hp}");

        #region Sequence Skill
        Skills.AddSkill<Move>(transform.position);
        Skills.AddSkill<Dash>(transform.position);
        Skills.AddSkill<Dash>(transform.position);
        Skills.AddSkill<Dash>(transform.position);
        Skills.StartNextSequenceSkill();
        #endregion

        return true;
    }

    private void FixedUpdate()
    {
        if (CreatureState != Define.CreatureState.Moving)
            return;
        PlayerController pc = Managers.Object.Player;
        if (pc == null)
            return;

        Vector3 dir = pc.transform.position - transform.position;

        GetComponent<SpriteRenderer>().flipX = dir.x > 0;
    }

    public override void UpdateAnimation()
    {
        switch (CreatureState)
        {
            case Define.CreatureState.Idle:
                _animator.Play("Idle");
                break;
            case Define.CreatureState.Moving:
                _animator.Play("Moving");
                break;
            case Define.CreatureState.Skill:
                _animator.Play("Attack");
                break;
            case Define.CreatureState.Dead:
                _animator.Play("Death");
                break;
            default:
                break;
        }
    }

    protected override void UpdateSkill()
    {
        base.UpdateSkill();

        PlayerController pc = Managers.Game.Player;
        if (pc == null)
            return;

        Vector3 dir = pc.gameObject.transform.position - transform.position;

        if (dir.magnitude >= 3.0f)
        {
            Skills.StartNextSequenceSkill();
            CreatureState = Define.CreatureState.Moving;
        }
    }

    protected override void UpdateMoving()
    {
        base.UpdateMoving();

        PlayerController pc = Managers.Game.Player;
        if (pc == null)
            return;

        Vector3 dir = pc.gameObject.transform.position - transform.position;

        if (dir.magnitude <= 2.0f)
            CreatureState = Define.CreatureState.Skill;
    }

    protected override void UpdateDead()
    {
        if (_coWait == null)
        {
            Managers.Object.Despawn(this);
        }
    }

    #region Wait Coroutine
    Coroutine _coWait;

    void Wait(float waitSeconds)
    {
        if (_coWait != null)
            StopCoroutine(_coWait);
        _coWait = StartCoroutine(CoStartWait(waitSeconds));
    }

    IEnumerator CoStartWait(float waitSeconds)
    {
        yield return new WaitForSeconds(waitSeconds);
        _coWait = null;
    }
    #endregion

    public override void OnDamaged(BaseController attacker, int damage)
    {
        base.OnDamaged(attacker, damage);
        Debug.Log($"BossMonster Hp : {Hp}");
    }

    protected override void OnDead()
    {
        Skills.StopSkills();
        CreatureState = Define.CreatureState.Dead;
        Wait(2.0f);
    }
}
