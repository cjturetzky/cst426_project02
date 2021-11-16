using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiAction : Action
{
    public override void Execute(GameObject target)
    {
        // stretchgoal: generate larger maze
        BattleManager.Instance.ActivatePuzzle();
    }

    public override void Success()
    {
        // Damage all enemies
        BattleManager.Instance.DamageAll(10);
    }
}
