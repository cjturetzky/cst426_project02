using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHIt : MonoBehaviour
{
    
    private void OnTriggerEvent2D(Collider2D other){
        Debug.Log("hit detected");
    }

}
