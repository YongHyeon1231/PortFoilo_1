using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;

public class Move : SequenceSkill
{
    Rigidbody2D _rb;
    Coroutine _coroutine;

    float Speed { get; } = 2.0f;
    string AnimationName { get; } = "Moving";

    private void Awake()
    {
        
    }

    public override void DoSkill(Action callback = null)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(CoMove(callback));
    }

    IEnumerator CoMove(Action callback = null)
    {
        _rb = GetComponent<Rigidbody2D>();
        GetComponent<Animator>().Play(AnimationName);
        float elapsed = 0;

        while (true)
        {
            elapsed += Time.deltaTime;

            Vector3 dir = ((Vector2)Managers.Game.Player.transform.position - _rb.position).normalized;
            Vector2 targetPos = Managers.Game.Player.transform.position + dir * UnityEngine.Random.Range(1, 4);

            if (Vector3.Distance(_rb.position, targetPos) <= 0.2f)
            {
                continue;
            }

            if (elapsed > 5.0f)
                break;

            Vector2 dirVec = targetPos - _rb.position;
            Vector2 nextVec = dirVec.normalized * Speed * Time.fixedDeltaTime;
            _rb.MovePosition(_rb.position + nextVec);

            yield return null;
        }

        callback?.Invoke();
    }
}
