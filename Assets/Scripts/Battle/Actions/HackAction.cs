using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackAction : Action
{
    // Start is called before the first frame update
    public override void Execute(GameObject target)
    {
        // target.GetComponent<Enemy>().TakeDamage(10);
        BattleManager.Instance.ActivatePuzzle();
    }
}
