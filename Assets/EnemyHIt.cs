using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHIt : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D other){
       SceneManager.LoadScene("BattleScene");
    }

}
