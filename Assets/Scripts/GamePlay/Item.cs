using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Heal,
        Thunder,
        Fire,
        Ice
    }
    
    public ItemType Type;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            if(Type== ItemType.Heal)
            {
                AudioManager.Instance.PlaySE("Heal");
            } else
            {
                AudioManager.Instance.PlaySE("NewWeapon");
            }
           
        }
            
    }
}
