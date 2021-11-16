using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseBlocker : MonoBehaviour
{
    public EventSystem eventSystem;
    public GameObject highlightedButton;
    
    // Start is called before the first frame update
    void Start()
    {
        if (eventSystem == null)
        {
            eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        }
    }

    void Update()
    {
        if (eventSystem.currentSelectedGameObject == null)
        {
            eventSystem.SetSelectedGameObject(highlightedButton);
        }

        highlightedButton = eventSystem.currentSelectedGameObject;
    }
}
