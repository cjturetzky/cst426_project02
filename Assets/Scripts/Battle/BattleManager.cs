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
    public GameObject puzzleView;

    public Text infoText;
    private EventSystem eventSystem;
    public string selectedAction;
    public GameObject highlightedButton;

    int reward = 0;
    float defaultSpeed = 0.025f;

    public static BattleManager Instance {private set ; get;}
    void Start()
    {
        Instance = this;
        eventSystem = this.GetComponent<EventSystem>();
        InstantiateEnemies();
    }

    void Update()
    {
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

    public void RemoveEnemy(GameObject enemy)
    {
        reward += enemy.GetComponent<Enemy>().scrap;
        round.enemies.Remove(enemy);
        if (round.enemies.Count == 0)
        {
            EndBattle();
        }
    }

    public void ReturnToSelection()
    {
        eventSystem.SetSelectedGameObject(GameObject.Find("HackButton"));
    }

    public void SetAction(string action)
    {
        selectedAction = action;
        //infoText.text = "Select a target!";
        StartDialogue("Select a target!", defaultSpeed);
        eventSystem.SetSelectedGameObject(GameObject.FindGameObjectWithTag("Enemy"));
    }

    public void ActivatePuzzle()
    {
        //infoText.text = "Navigate through the maze to hack the robot's CORE!";
        StartDialogue("Navigate through the maze to hack the robot's CORE!", defaultSpeed / 2);
        puzzleView.SetActive(true);
        PuzzleManager.Instance.Restart();
        eventSystem.sendNavigationEvents = false;
    }

    public void EndPuzzle()
    {
        puzzleView.SetActive(false);
        eventSystem.currentSelectedGameObject.GetComponent<Enemy>().TakeDamage(10);
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
        //infoText.text = "The enemies attacked for " + damage + " damage!";
        player.GetComponent<PlayerBattle>().TakeDamage(damage);
        BeginPlayerTurn();
    }


    public void BeginPlayerTurn()
    {
        eventSystem.sendNavigationEvents = true;
        ReturnToSelection();
    }

    void EndBattle()
    {
        eventSystem.sendNavigationEvents = false;
        //infoText.text = "You won! Received " + reward + " scrap!";
        StartDialogue("You won! Received " + reward + " scrap!", defaultSpeed);
    }

    [Serializable]
    public struct Round
    {
        public List<GameObject> enemies;
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
