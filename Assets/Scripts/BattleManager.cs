using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleManager : MonoBehaviour
{

    public GameObject puzzleView;
    private EventSystem eventSystem;
    public string selectedAction;

    public static BattleManager Instance {private set ; get;}
    void Start()
    {
        Instance = this;
        eventSystem = this.GetComponent<EventSystem>();
    }

    public void ReturnToSelection()
    {
        eventSystem.SetSelectedGameObject(GameObject.Find("HackButton"));
    }

    public void SetAction(string action)
    {
        selectedAction = action;
        eventSystem.SetSelectedGameObject(GameObject.FindGameObjectWithTag("Enemy"));
    }

    public void ActivatePuzzle()
    {
        puzzleView.SetActive(true);
        eventSystem.sendNavigationEvents = false;
    }

    public void EndPuzzle()
    {
        puzzleView.SetActive(false);
        eventSystem.currentSelectedGameObject.GetComponent<Enemy>().TakeDamage(10);
        eventSystem.sendNavigationEvents = true;
        ReturnToSelection();
    }
}
