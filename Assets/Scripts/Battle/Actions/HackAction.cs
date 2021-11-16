using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackAction : Action
{
    // Start is called before the first frame update
    GameObject target;
    public override void Execute(GameObject target)
    {
        // target.GetComponent<Enemy>().TakeDamage(10);
        this.target = target;
        BattleManager.Instance.ActivatePuzzle();
    }

    public override void Success()
    {
        target.GetComponent<Enemy>().TakeDamage(10);
    }


}
