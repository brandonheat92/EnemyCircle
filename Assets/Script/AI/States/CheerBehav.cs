using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheerBehav : ActorBaseState
{
    float m_CheerTotalTime = 4;

    float m_CheerTimeInterval = 2;
    float m_CheerTimeCounter = 0;

    public CheerBehav(Actor actor) : base(actor, Actor.eStates.Cheer)
    { }

    public override void OnEnter()
    {
        base.OnEnter();

        //m_Actor.NavAgent.enabled = false;
        ////m_Actor.NavAgent.updatePosition = false;
        ////m_Actor.NavAgent.updateUpAxis = false;
        //m_Actor.Controller.enabled = true;
        EventManager.Instance.TriggerEvent(EventType.ActorBehavChange, new ActorChangeBehavMessage(m_Actor, Actor.eStates.Cheer));
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        m_CheerTimeCounter += Time.deltaTime;
        if (m_CheerTimeCounter > m_CheerTimeInterval)
        {
            m_CheerTimeCounter = 0;
            Cheer();
        }

        if (Time.time - m_TimeEntered > m_CheerTotalTime)
            m_Actor.RequestState(Actor.eStates.Circle);
    }

    public override void OnExit()
    {
        base.OnExit();

        //m_Actor.NavAgent.enabled = true;
        ////m_Actor.NavAgent.updatePosition = true;
        ////m_Actor.NavAgent.updateUpAxis = true;
        //m_Actor.Controller.enabled = false;
    }

    void Cheer()
    {
        m_Actor.StartCoroutine(CheerColor());
    }

    IEnumerator CheerColor()
    {
        Color col = m_Actor.GetComponent<MeshRenderer>().material.color;
        m_Actor.GetComponent<MeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(1);
        m_Actor.GetComponent<MeshRenderer>().material.color = col;
    }
}
