using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipCtrl : MonoBehaviour
{
    public PlayerStatesCtrl player;
    

    // Update is called once per frame
    void Update()
    {
        if(player.isFacingRight) 
        { 
            transform.localScale = new Vector3(1f, 1f, 1f);
        }else
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
}
