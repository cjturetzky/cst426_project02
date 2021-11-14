using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Abstract class used for player abilities
public abstract class Action : MonoBehaviour
{
    public int mpCost;
    public bool isTargeted;
    public bool hasMinigame;
    public abstract void Execute(GameObject target);

    public abstract void Success();
}
