using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Abstract class used for player abilities
public abstract class Action : MonoBehaviour
{
    public bool isTargeted;
    public abstract void Execute(GameObject target);
}
