using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    public float localTimeScale = 1f;
    public bool isPlayerDeath;
    public string dieSoundName;
    public GameObject mark;
    private void OnEnable()
    {
        PlayerStats.OnPlayerDie += OnPlayerDie;
    }

    public void OnPlayerDie()
    {
        isPlayerDeath = true;
    }

    private void OnDisable()
    {
        PlayerStats.OnPlayerDie -= OnPlayerDie;
    }

    
}
