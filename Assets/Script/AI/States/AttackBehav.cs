using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehav : ActorBaseState
{
    Vector3 m_MoveDirection = Vector3.forward;

    public AttackBehav(Actor actor) : base(actor, Actor.eStates.Attack)
    { }

    public override void OnEnter()
    {
        base.OnEnter();

        m_Actor.NavAgent.enabled = false;
        m_Actor.Controller.enabled = true;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        m_Actor.LookAtTargetActor();
        MoveTowards();

        if (Vector3.Distance(m_Actor.Position, m_Actor.TargetActorPosition) < 2)
        {
            InflectDamage();
            m_Actor.RequestState(Actor.eStates.Retreat);
        }
    }

    public override void OnExit()
    {
        base.OnExit();

        m_Actor.NavAgent.enabled = true;
        m_Actor.Controller.enabled = false;
    }

    void MoveTowards()
    {
        Vector3 moveDir = (m_Actor.TargetActorPosition - m_Actor.Position).normalized * 2 * Time.deltaTime;
        m_Actor.Controller.Move(moveDir);
    }

    void InflectDamage()
    {
        Debug.Log("Target actor damaged!!");
    }
}
