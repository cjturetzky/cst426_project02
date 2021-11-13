using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairAction : Action
{
    public override void Execute(GameObject target)
    {
        target.GetComponent<PlayerBattle>().TakeDamage(-30);
    }

    public override void Success(){}

}
