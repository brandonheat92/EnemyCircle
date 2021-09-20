using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBehav : ActorBaseState
{
    public GameObject m_ChaseTarget;

    public float m_TriggerDistance = 0;

    public ChaseBehav(Actor actor) : base(actor, Actor.eStates.Chase)
    {
        
    }

    public override void OnEnter()
    {
        base.OnEnter();
        m_ChaseTarget = m_Actor.TargetActor;
        m_Actor.NavAgent.SetDestination(m_ChaseTarget.transform.position);
        m_TriggerDistance = Random.Range(5,15);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        m_Actor.NavAgent.SetDestination(m_ChaseTarget.transform.position);
        if (Vector3.Distance(m_Actor.transform.position, m_ChaseTarget.transform.position) < m_TriggerDistance)
        {
            m_Actor.RequestState(Actor.eStates.Circle);
        }
    }
}
