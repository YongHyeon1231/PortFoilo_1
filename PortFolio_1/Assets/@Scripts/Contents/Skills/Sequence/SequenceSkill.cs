using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public abstract class SequenceSkill : SkillBase
{
    public SequenceSkill() : base(Define.SkillType.Sequence)
    {
    }

    public abstract void DoSkill(Action callback = null);
}
