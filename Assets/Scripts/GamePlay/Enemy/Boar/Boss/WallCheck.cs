using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    public BoarBossStaresCtrl bossState;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if(collision.CompareTag("Enemy")) return;
        if (collision.CompareTag("Wall")) 
        {
            Debug.Log("Wall");
            bossState.OnWallCollision();
        }
    }
}
