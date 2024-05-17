using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBook : MonoBehaviour
{
    // ������ ��ų �Ŵ���
    public List<SkillBase> Skills { get; } = new List<SkillBase>();
    public List<SkillBase> RepeatedSkills { get; } = new List<SkillBase>();
    // ������ �����?
    public List<SequenceSkill> SequenceSkills { get; } = new List<SequenceSkill>();

    public T AddSkill<T>(int templateID, Vector3 position, Transform parent = null) where T : SkillBase
    {
        // TemplateID �� ����

        switch(templateID)
        {
            case 100:
                var egoSword = Managers.Object.Spawn<EgoSword>(position, templateID);

                egoSword.transform.SetParent(parent);
                egoSword.ActivateSkill();

                Skills.Add(egoSword);
                RepeatedSkills.Add(egoSword);

                return egoSword as T;
            case 200:
                var fireball = Managers.Object.Spawn<FireballSkill>(position, templateID);

                fireball.transform.SetParent(parent);
                fireball.ActivateSkill();

                Skills.Add(fireball);
                RepeatedSkills.Add(fireball);

                return fireball as T;
        }

        return null;
    }
}
