using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;

public class PlayerController : CreatureController
{
    float EnvCollectDist { get; set; } = 1.0f;
    
    Vector2 _moveDir = Vector2.zero;

    public Vector2 MoveDir
    {
        get { return _moveDir; }
        set { _moveDir = value.normalized; }
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Managers.Game.OnMoveDirChanged -= HandleOnMoveDirChanged;
        Managers.Game.OnMoveDirChanged += HandleOnMoveDirChanged;

        // 데이터로 넣어주기
        _speed = 5.0f;

        return true;
    }

    private void Update()
    {
        MovePlayer();
        CollectEnv();
    }

    private void OnDestroy()
    {
        if (Managers.Game != null)
            Managers.Game.OnMoveDirChanged -= HandleOnMoveDirChanged;
    }

    #region 이동
    void HandleOnMoveDirChanged(Vector2 dir)
    {
        _moveDir = dir;
    }

    void MovePlayer()
    {
        Vector3 dir = _moveDir * _speed * Time.deltaTime;
        transform.position += dir;

        //GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }
    #endregion

    private void CollectEnv()
    {
        float sqrCollectDist = EnvCollectDist * EnvCollectDist;

        var findGems = GameObject.Find("@Grid").GetComponent<GridController>().GatherObjects(transform.position, EnvCollectDist * 0.5f);

        foreach (var go in findGems)
        {
            GemController gem = go.GetComponent<GemController>();

            Vector3 dir = gem.transform.position - transform.position;
            if (dir.sqrMagnitude <= sqrCollectDist)
            {
                Managers.Game.Gem += 1;
                Managers.Object.Despawn(gem);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        MonsterController target = collision.gameObject.GetComponent<MonsterController>();
        if (target == null)
            return;
    }

    public override void OnDamaged(BaseController attacker, int damage)
    {
        base.OnDamaged(attacker, damage);

        Debug.Log($"PlayerOnDamaged! PlayerHp : {Hp}");

        //TEMP
        CreatureController cc = attacker as CreatureController;
        cc?.OnDamaged(this, 60);
    }
}
