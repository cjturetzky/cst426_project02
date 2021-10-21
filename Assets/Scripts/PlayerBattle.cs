using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattle : MonoBehaviour
{
    public int maxHp;
    public int hp;
    public int atk;
    public int mp; 
    // Start is called before the first frame update
    void Start()
    {
        // Receive stat information from memento
    }

    void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            // BattleManage.Instance.GameOver();
            Debug.Log("Player has perished");
        }
    }
}
