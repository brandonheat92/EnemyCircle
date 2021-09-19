using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorBaseState : State
{
    protected Actor m_Actor;

    public ActorBaseState(Actor actor, Actor.eStates state) : base(actor, (int)state)
    {
        m_Actor = actor;    
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
