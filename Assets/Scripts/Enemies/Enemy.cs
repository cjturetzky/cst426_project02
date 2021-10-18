using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Base class inherited by all Robot enemies
public class Enemy : MonoBehaviour, ISelectHandler, IDeselectHandler, ISubmitHandler, ICancelHandler
{
    public void OnSelect(BaseEventData eventData)
    {
        GetComponent<SpriteRenderer>().material.color = Color.blue;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        GetComponent<SpriteRenderer>().material.color = Color.white;
    }

    public void OnSubmit(BaseEventData eventData)
    {
        Debug.Log("Used " + BattleManager.Instance.selectedAction + " on " + this.gameObject.name);
    }

    public void OnCancel(BaseEventData eventData)
    {
        eventData.selectedObject = GameObject.Find("HackButton");
    }
}
