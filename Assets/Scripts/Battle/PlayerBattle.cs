using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Class representing the player object in battle
// Handles player stats and UI elements
public class PlayerBattle : MonoBehaviour
{
    public Text hpText;
    public int maxHp;
    public int hp;
    public int atk;
    public int mp; 
    // Start is called before the first frame update
    void Start()
    {
        // Receive stat information from memento
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        hpText.text = "HP: " + hp + "/" + maxHp;
        if (hp <= 0)
        {
            // BattleManage.Instance.GameOver();
            Debug.Log("Player has perished");
        }
    }

}
