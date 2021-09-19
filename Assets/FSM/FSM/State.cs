using UnityEngine;
using System.Collections;

public class State
{
    public int m_id { private set; get; }
    protected StateMachineAdvance m_stateMachine;
    public float m_timeEntered { private set; get; }

    public State(StateMachineAdvance sm, int id)
    {
        m_id = id;
    }

    public virtual void OnEnter()
    {
        Debug.Log("On Enter " + m_id);
        m_timeEntered = Time.time;
    }

    public virtual void OnExit()
    {
        Debug.Log("On Exit " + m_id);
    }

    public virtual void OnUpdate()
    {

    }
}
