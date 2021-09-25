using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeBehav : ActorBaseState
{

    float m_FleeingSpeed = 10;

    public FleeBehav(Actor actor) : base(actor, Actor.eStates.Flee)
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

        MoveAway();
        m_Actor.LookAtTargetActor(-1);

        if (Vector3.Distance(m_Actor.Position, m_Actor.TargetActorPosition) > 15)
            m_Actor.RequestState(Actor.eStates.Idle);
    }

    public override void OnExit()
    {
        base.OnExit();
        m_Actor.NavAgent.enabled = true;
        m_Actor.Controller.enabled = false;
    }

    void MoveAway()
    {
        Vector3 moveDir = (m_Actor.Position - m_Actor.TargetActorPosition).normalized * m_FleeingSpeed * Time.deltaTime;
        m_Actor.Controller.Move(moveDir);
    }
}
