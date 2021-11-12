using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyAction : Action
{
    // Start is called before the first frame update
    public override void Execute(GameObject target)
    {
        Debug.Log("smile");
    }
}
