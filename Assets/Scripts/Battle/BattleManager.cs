using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BattleManager : MonoBehaviour
{

    public Round round;
    public GameObject[] slots;

    public GameObject player;
    PlayerBattle playerCore;
    public GameObject puzzleView;

    public GameObject actionMenu;
    public GameObject spellMenu;

    public Text infoText;
    public Text spellText;
    public Text timeText;

    private EventSystem eventSystem;
    public Action selectedAction;
    public GameObject highlightedButton;

    public float time = 15.0f;
    
    GameObject previousButton;

    int reward = 0;
    float defaultSpeed = 0.025f;
    float timeRemaining = 0.0f;

    public static BattleManager Instance {private set ; get;}
    void Start()
    {
        timeRemaining = time;
        Instance = this;
        eventSystem = this.GetComponent<EventSystem>();
        playerCore = player.GetComponent<PlayerBattle>();
        InstantiateEnemies();
    }

    void Update()
    {
        if (puzzleView.activeSelf){
            timeRemaining -= Time.deltaTime;
            timeText.text = "Time: " + timeRemaining.ToString("00.00");
            if(timeRemaining <= 0){
                EndPuzzle(true);
            }
        }

        if (eventSystem.currentSelectedGameObject == null)
        {
            eventSystem.SetSelectedGameObject(highlightedButton);
        }

        highlightedButton = eventSystem.currentSelectedGameObject;

        
    }

    void InstantiateEnemies()
    {
        for (int i = 0; i < round.enemies.Count; i++)
        {
            round.enemies[i] = GameObject.Instantiate(round.enemies[i], slots[i].transform);
        }
    }

    // Deals damage to all active enemies. Used by the Multi-Hack action. 
    public void DamageAll(int damage)
    {
        // Create a new list to account for enemies dying mid-enumeration
        List<Enemy> enemies = new List<Enemy>();
        foreach (GameObject enemy in round.enemies)
        {
            enemies.Add(enemy.GetComponent<Enemy>());
        }
        enemies.ForEach(enemy => enemy.TakeDamage(10));
    }

    // Called by enemies when their hp is reduced to 0. 
    public void RemoveEnemy(GameObject enemy)
    {
        reward += enemy.GetComponent<Enemy>().scrap;
        round.enemies.Remove(enemy);
        if (round.enemies.Count == 0)
        {
            EndBattle();
        }
    }

    // Returns to the button that was selected at the end of the previous turn
    public void ReturnToSelection()
    {
        eventSystem.SetSelectedGameObject(previousButton);
    }

    // Load an action and executes its script
    public void SetAction(GameObject action)
    {
        previousButton = highlightedButton;
        selectedAction = action.GetComponent<Action>();
        if (playerCore.mp - selectedAction.mpCost < 0)
        {
            StartDialogue("You don't have enough MP!", defaultSpeed / 2);
            return;
        }
        else
        {
            playerCore.SpendMp(selectedAction.mpCost);
        }

        if (selectedAction.isTargeted)
        {
            SetTarget();
        }
        else if (!selectedAction.hasMinigame)
        {
            selectedAction.Execute(player);
            BeginEnemyTurn();
        }
        else
        {
            selectedAction.Execute(player);
        }
    }

    // Prompts the player to select an enemy to hack
    public void SetTarget()
    {
        StartDialogue("Select a target!", defaultSpeed);
        eventSystem.SetSelectedGameObject(GameObject.FindGameObjectWithTag("Enemy"));
    }


    public void ActivatePuzzle(float time)
    {
        timeRemaining = time;
        StartDialogue("Navigate through the maze to hack the robot's CORE! You have " + time + " seconds!", defaultSpeed / 2);
        puzzleView.SetActive(true);
        eventSystem.sendNavigationEvents = false;
    }

    // Called by the puzzle manager on completion. Executes the selected action's script
    public void EndPuzzle(bool timeout)
    {
        PuzzleManager.Instance.Restart();
        puzzleView.SetActive(false);
        if(!timeout){
            selectedAction.Success();
        }
        BeginEnemyTurn();
    }

    void BeginEnemyTurn()
    {
        if (round.enemies.Count == 0)
        {
            return;
        }
        
        eventSystem.sendNavigationEvents = false;
        int damage = 0;

        for (int i = 0; i < round.enemies.Count; i++)
        {
            Enemy enemy = round.enemies[i].GetComponent<Enemy>();
            damage += enemy.atk;
        }
        StartDialogue("The enemies attacked for " + damage + " damage!", defaultSpeed);
        playerCore.TakeDamage(damage);

        if (playerCore.hp > 0)
        {
            BeginPlayerTurn();
        }
    }


    public void BeginPlayerTurn()
    {
        eventSystem.sendNavigationEvents = true;
        ReturnToSelection();
    }

    void EndBattle()
    {
        eventSystem.sendNavigationEvents = false;
        StartDialogue("You won! Received " + reward + " scrap!", defaultSpeed);
    }

    public void GameOver()
    {
        StartDialogue("You were knocked out! Game Over!", defaultSpeed);
        eventSystem.sendNavigationEvents = false;
    }

    // Swaps between the Main and Spell menus for actions
    public void ToggleMenu()
    {
        if (actionMenu.gameObject.activeInHierarchy)
        {
            actionMenu.SetActive(false);
            spellMenu.SetActive(true);
            eventSystem.SetSelectedGameObject(GameObject.Find("MultiHackButton"));
        }
        else
        {
            actionMenu.SetActive(true);
            spellMenu.SetActive(false);
            eventSystem.SetSelectedGameObject(GameObject.Find("HackButton"));
        }
    }

    [Serializable]
    public struct Round
    {
        public List<GameObject> enemies;
    }

    public void DisplaySpellInfo(string text)
    {
        spellText.text = text;
    }

    void StartDialogue(string text, float speed)
    {
        StopAllCoroutines();
        StartCoroutine(PrintDialogue(text, speed));
    }

    IEnumerator PrintDialogue(string text, float speed)
    {
        infoText.text = "";
        foreach (char c in text.ToCharArray())
        {
            infoText.text += c;
            yield return new WaitForSeconds(speed);
        }
    }
}
