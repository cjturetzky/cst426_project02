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

    public static BattleManager Instance {private set ; get;}
    void Start()
    {
        Instance = this;
        eventSystem = this.GetComponent<EventSystem>();
        InstantiateEnemies();
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
        round.enemies.Remove(enemy);
    }

    public void ReturnToSelection()
    {
        eventSystem.SetSelectedGameObject(GameObject.Find("HackButton"));
    }

    public void SetAction(string action)
    {
        selectedAction = action;
        infoText.text = "Select a target!";
        eventSystem.SetSelectedGameObject(GameObject.FindGameObjectWithTag("Enemy"));
    }

    public void ActivatePuzzle()
    {
        infoText.text = "Navigate through the maze to hack the robot's CORE!";
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
        for (int i = 0; i < round.enemies.Count; i++)
        {
            player.GetComponent<PlayerBattle>().TakeDamage(round.enemies[i].GetComponent<Enemy>().atk);
        }
        BeginPlayerTurn();
    }

    public void BeginPlayerTurn()
    {
        eventSystem.sendNavigationEvents = true;
        ReturnToSelection();

    }

    [Serializable]
    public struct Round
    {
        public List<GameObject> enemies;
    }
}
