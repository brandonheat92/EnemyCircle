using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType
{
    NONE = 0,
    ActorBehavChange,
    TestMessage
}

public class EventMessage
{
    public float m_InvokeTime { private set; get; }
}

public class AiEventMessage : EventMessage
{
    public Actor            m_Actor { protected set; get; }
}

public class ActorChangeBehavMessage : AiEventMessage
{
    public Actor.eStates m_CurrentState { private set; get; }

    public ActorChangeBehavMessage(Actor actor, Actor.eStates state)
    {
        this.m_Actor            = actor;
        this.m_CurrentState     = state;
    }
}