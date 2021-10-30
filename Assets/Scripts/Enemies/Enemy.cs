using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Base class inherited by all Robot enemies
public class Enemy : MonoBehaviour, ISelectHandler, IDeselectHandler, ISubmitHandler, ICancelHandler
{
    public int hp;
    public int atk;
    public int scrap;
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
        // to be replaced with: BattleManage.Instance.selectedAction.execute();
        Debug.Log("Used " + BattleManager.Instance.selectedAction + " on " + this.gameObject.name);
        BattleManager.Instance.ActivatePuzzle();
        // TakeDamage(10);
        // BattleManager.Instance.ReturnToSelection();
    }

    public void OnCancel(BaseEventData eventData)
    {
        BattleManager.Instance.ReturnToSelection();
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Reward player with scrap
        BattleManager.Instance.RemoveEnemy(this.gameObject);
        Destroy(this.gameObject);
    }
}
