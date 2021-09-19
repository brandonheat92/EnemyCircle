using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehav : ActorBaseState
{
    public IdleBehav(Actor actor) : base(actor, Actor.eStates.Idle)
    {

    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if(Time.time - m_timeEntered > 2.0f)
        {
            m_Actor.RequestState(Actor.eStates.Wander);
        }
    }

}
