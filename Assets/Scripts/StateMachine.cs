using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    BaseState currentState;

    void Start()
    {
        
        //currentState = GetInitState();
        StartSM();
    }
    
    protected virtual void UpdateSM() {}
    protected virtual void StartSM() { Debug.Log("Get Component Father"); }
    void Update()
    {
        if(currentState != null) 
        {
           // Debug.Log("Update running");
            currentState.UpdateLogic(); 
        }
        UpdateSM();
        Debug.Log("State : " + currentState.name);
    }

    void FixedUpdate()
    {
        if (currentState != null) { currentState.UpdatePhysics(); }

    }

    public void ChangeState(BaseState newState)
    {
        if(currentState != null) currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    protected virtual BaseState GetInitState()
    {
        return null;
    }

    private void OnGUI()
    {
        string content = currentState != null ? currentState.name : "{no current steate}";
        GUILayout.Label($"<color='black'><size=40>{content}</size><color>");
    }
}
