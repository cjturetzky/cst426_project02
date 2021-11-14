using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Class representing the player object in battle
// Handles player stats and UI elements
public class PlayerBattle : MonoBehaviour
{
    public Text hpText;
    public Text mpText;
    public int maxHp;
    public int hp;
    public int atk;
    public int mp; 
    public int maxMp;
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
        if (hp > maxHp)
        {
            hp = maxHp;
        }
        if (hp <= 0)
        {
            BattleManager.Instance.GameOver();
        }
    }

    public void SpendMp(int cost)
    {
        mp -= cost;
        mpText.text = "MP: " + mp + "/" + maxMp;
    }

}
