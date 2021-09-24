using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetreatBehav : ActorBaseState
{

    float m_BackingDistance = 0;
    float m_MovingSpeed = 0;

    public RetreatBehav(Actor actor) : base(actor, Actor.eStates.Retreat)
    { }

    public override void OnEnter()
    {
        base.OnEnter();

        m_Actor.NavAgent.enabled    = false;
        m_Actor.Controller.enabled  = true;
        m_BackingDistance           = Random.Range(6, 10);
        m_MovingSpeed               = Random.Range(1,10) % 2 == 0 ? 1 : 5;    //fast retreat or normal one
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        m_Actor.LookAtTargetActor();
        MoveBack();

        if (Vector3.Distance(m_Actor.Position, m_Actor.TargetActorPosition) > m_BackingDistance)
            m_Actor.RequestState(Actor.eStates.Circle);
    }

    public override void OnExit()
    {
        base.OnExit();

        m_Actor.NavAgent.enabled = true;
        m_Actor.Controller.enabled = false;
    }

    void MoveBack()
    {
        Vector3 moveDir = (m_Actor.Position - m_Actor.TargetActorPosition).normalized * m_MovingSpeed * Time.deltaTime;
        m_Actor.Controller.Move(moveDir);
    }
}
