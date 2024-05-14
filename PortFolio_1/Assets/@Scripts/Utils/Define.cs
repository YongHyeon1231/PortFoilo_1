using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    #region Enum
    public enum Scene
    {
        LobbyScene,
        GameScene,
    }
    
    public enum Sound
    {
        Bgm,
        Effect,
    }

    public enum ObjectType
    {
        Player,
        Monster,
        Projectile,
        Env,
    }

    public enum CreatureState
    {
        Idle,
        Moving,
        Skill,
        Dead,
    }

    public enum SkillType
    {
        None,
        Melee,
        Projectile,
        Repeat,
    }
    #endregion

    public const int GOBLIN_ID = 1;
    public const int SNAKE_ID = 2;
    public const int SLIME_ID = 3;
    public const int BOSS_ID = 4;
}
