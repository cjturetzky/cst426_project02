using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Base class inherited by all Robot enemies
public class Enemy : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("Highlighting " + this.gameObject.name);
        GetComponent<SpriteRenderer>().material.color = Color.blue;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Debug.Log("Deselecting " + this.gameObject.name + "...");
        GetComponent<SpriteRenderer>().material.color = Color.white;
    }
}
