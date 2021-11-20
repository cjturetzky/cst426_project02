using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiAction : Action
{
    public float time;
    public override void Execute(GameObject target)
    {
        // stretchgoal: generate larger maze
        BattleManager.Instance.ActivatePuzzle(time);
    }

    public override void Success()
    {
        // Damage all enemies
        BattleManager.Instance.DamageAll(10);
    }
}
