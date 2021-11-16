using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpellButton : MonoBehaviour, ISelectHandler
{
    public string infoText;
    // Start is called before the first frame update
    public void OnSelect(BaseEventData eventData)
    {
        BattleManager.Instance.DisplaySpellInfo(infoText);
    }
}
