using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateMachineAdvance : MonoBehaviour
{
    protected List<State> m_allStates;

    State m_currentState;
    State m_requestedState;

    public virtual void Awake()
    {
        m_allStates = new List<State>();
    }

	// Use this for initialization
	public virtual void Start ()
   {

	}


    protected void RegisterState(State state)
    {
        for(int i=0; i<m_allStates.Count; i++)
        {
            if(m_allStates[i].m_id == state.m_id)
            {
                Debug.LogError("state already present");
                return;
            }
        }
        Debug.Log("Registered " + state.m_id);
        m_allStates.Add(state);
    }

	// Update is called once per frame
	public virtual void Update ()
    {
	    if(m_currentState != null)
        {
            m_currentState.OnUpdate();
        }
	}

    protected void SwitchState()
    {
        if(m_currentState != m_requestedState)
        {
            if(m_requestedState == null)
            {
                Debug.LogError("requested state is null");
                return;
            }

            if(m_currentState != null)
            {
                m_currentState.OnExit();
            }

            m_currentState = m_requestedState;
            m_currentState.OnEnter();
        }
    }

    public void RequestState(int id)
    {
        for(int i=0; i<m_allStates.Count; i++)
        {
            if(m_allStates[i].m_id == id)
            {
                m_requestedState = m_allStates[i];
                return;
            }
        }

        Debug.LogWarning("requesting a unregistered state " + id);
    }
}
