using System.Collections.Generic;
using UnityEngine;

public abstract class StatUpgrade<T> : Upgrade
{
    protected Dictionary<int, T> valuesByLevel;

    protected override void Awake()
    {
        base.Awake();
        if (valuesByLevel != null)
        {
            maxLevel = valuesByLevel.Count - 1;
        }
    }

    public override void ApplyEffect()
    {
        if (valuesByLevel == null)
        {
            Debug.LogError($"(StatUpgrade) valuesByLevel dictionary is not initialized.");
            return;
        }

        if (!valuesByLevel.ContainsKey(CurrentLevel))
        {
            Debug.LogError($"(StatUpgrade) Upgrade level {CurrentLevel} not included in dictionary...");
            return;
        }

        ApplyStatEffect(valuesByLevel[CurrentLevel]);
        Debug.Log($"({GetType().Name}) Applied effect at level {CurrentLevel}.");
    }

    // Each child class must define how the stat is applied
    protected abstract void ApplyStatEffect(T value);
}

