using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderBehav : ActorBaseState
{

    Vector3 m_Postion;

   public WanderBehav(Actor actor) : base(actor, Actor.eStates.Wander)
    {

    }

    public override void OnEnter()
    {
        base.OnEnter();
        Vector3 direc = Random.insideUnitSphere * 10;
        direc += m_Actor.transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(direc, out hit, 10,1);
        m_Postion = hit.position;

        m_Actor.NavAgent.SetDestination(m_Postion);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (Vector3.Distance(m_Actor.transform.position, m_Postion) < 5)
        {
            m_Actor.RequestState(Actor.eStates.Idle);
        }
    }
}
