using UnityEngine;
using System.Collections;

public class State
{
    public int                      m_Id { private set; get; }
    protected StateMachineAdvance   m_StateMachine;
    public float                    m_TimeEntered { private set; get; }

    public State(StateMachineAdvance sm, int id)
    {
        this.m_Id            = id;
        this.m_StateMachine  = sm;
    }

    public virtual void OnEnter()
    {
        if(m_StateMachine.m_IsDebug)
            Debug.Log("On Enter " + (Actor.eStates)m_Id);
        m_TimeEntered = Time.time;
    }

    public virtual void OnExit()
    {
        if (m_StateMachine.m_IsDebug)
            Debug.Log("On Exit " + (Actor.eStates)m_Id);
    }

    public virtual void OnUpdate()
    {

    }
}
