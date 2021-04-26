using System.Collections.Generic;


public static class TypeChart
{
    public enum CreatureType
    {
        None,
        Normal,
        Fire,
        Water,
        Grass,
        Electric,
        Flying
    }
    
    // Proper usage is DamageMult[AttackType][DefendingCreatureType]
    public static readonly Dictionary<CreatureType, Dictionary<CreatureType, float>> DamageMult =
        new Dictionary<CreatureType, Dictionary<CreatureType, float>>()
    {
        {
            // A catch case for bad attacks, NO attack should have the None type
            CreatureType.None, new Dictionary<CreatureType, float>()
            {
                {CreatureType.None, 0},
                {CreatureType.Normal, 0},
                {CreatureType.Fire, 0},
                {CreatureType.Water, 0},
                {CreatureType.Grass, 0},
                {CreatureType.Electric, 0},
                {CreatureType.Flying, 0}
            }
        },
        {
            CreatureType.Normal, new Dictionary<CreatureType, float>()
            {
                {CreatureType.None, 1},
                {CreatureType.Normal, 1},
                {CreatureType.Fire, 1},
                {CreatureType.Water, 1},
                {CreatureType.Grass, 1},
                {CreatureType.Electric, 1},
                {CreatureType.Flying, 1}
            }
        },
        {
            CreatureType.Fire, new Dictionary<CreatureType, float>()
            {
                {CreatureType.None, 1},
                {CreatureType.Normal, 1},
                {CreatureType.Fire, 0.5f},
                {CreatureType.Water, 0.5f},
                {CreatureType.Grass, 2},
                {CreatureType.Electric, 1},
                {CreatureType.Flying, 1}
            }
        },
        {
            CreatureType.Water, new Dictionary<CreatureType, float>()
            {
                {CreatureType.None, 1},
                {CreatureType.Normal, 1},
                {CreatureType.Fire, 2},
                {CreatureType.Water, 0.5f},
                {CreatureType.Grass, 0.5f},
                {CreatureType.Electric, 1},
                {CreatureType.Flying, 1}
            }
        },
        {
            CreatureType.Grass, new Dictionary<CreatureType, float>()
            {
                {CreatureType.None, 1},
                {CreatureType.Normal, 1},
                {CreatureType.Fire, 0.5f},
                {CreatureType.Water, 2},
                {CreatureType.Grass, 0.5f},
                {CreatureType.Electric, 1},
                {CreatureType.Flying, 0.5f}
            }
        },
        {
            CreatureType.Electric, new Dictionary<CreatureType, float>()
            {
                {CreatureType.None, 1},
                {CreatureType.Normal, 1},
                {CreatureType.Fire, 1},
                {CreatureType.Water, 2},
                {CreatureType.Grass, 0.5f},
                {CreatureType.Electric, 0.5f},
                {CreatureType.Flying, 2}
            }
        }
    };
}
