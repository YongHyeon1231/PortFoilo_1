using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBook : MonoBehaviour
{
    // 일종의 스킬 매니저
    public List<SkillBase> Skills { get; } = new List<SkillBase>();
    public List<SkillBase> RepeatedSkills { get; } = new List<SkillBase>();
    // 프리팹 만들까?
    public List<SequenceSkill> SequenceSkills { get; } = new List<SequenceSkill>();

    public T AddSkill<T>(int templateID, Vector3 position, Transform parent = null) where T : SkillBase
    {
        // TemplateID 로 구분

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

    public T AddSkill<T>(Vector3 position, Transform parent = null) where T : SkillBase
    {
        // Type으로 구분
        System.Type type = typeof(T);

        if (type.IsSubclassOf(typeof(SequenceSkill)))
        {
            var skill = gameObject.GetOrAddComponent<T>();
            Skills.Add(skill);
            SequenceSkills.Add(skill as SequenceSkill);

            return null;
        }

        return null;
    }

    int _sequenceIndex = 0;
    bool _stopped = false;

    public void StartNextSequenceSkill()
    {
        if (_stopped)
            return;
        if (SequenceSkills.Count == 0)
            return;

        SequenceSkills[_sequenceIndex].DoSkill(OnFinishedSequenceSkill);
    }

    private void OnFinishedSequenceSkill()
    {
        _sequenceIndex = (_sequenceIndex + 1) % SequenceSkills.Count;
        StartNextSequenceSkill();
    }

    public void StopSkills()
    {
        _stopped = true;

        foreach (var skill in RepeatedSkills)
        {
            skill.StopAllCoroutines();
        }
    }
}
