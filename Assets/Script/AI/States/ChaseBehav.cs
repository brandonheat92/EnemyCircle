using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBehav : ActorBaseState
{
    public GameObject m_ChaseTarget;

    public ChaseBehav(Actor actor) : base(actor, Actor.eStates.Chase)
    {
        
    }

    public override void OnEnter()
    {
        base.OnEnter();

        m_Actor.NavAgent.enabled = true;
        m_Actor.Controller.enabled = false;

        m_ChaseTarget = m_Actor.TargetActor;
        m_Actor.NavAgent.SetDestination(m_ChaseTarget.transform.position);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        m_Actor.NavAgent.SetDestination(m_ChaseTarget.transform.position);
        if (Vector3.Distance(m_Actor.transform.position, m_ChaseTarget.transform.position) < m_Actor.ThresholdDistance)
        {
            m_Actor.RequestState(Actor.eStates.Circle);
        }
    }
}
