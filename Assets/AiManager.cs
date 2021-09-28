using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiManager : MonoBehaviour
{
    private List<Actor>     m_CirclingActor;
    private Actor           m_AttackingActor;
    private List<Actor>     m_CheeringActor;

    private float       m_AttackTimeCounter;

    public float        m_AttackingInterval = 3;

    void Awake()
    {
        m_CirclingActor = new List<Actor>();
        m_CheeringActor = new List<Actor>();

        EventManager.Instance.SubscribeToEvent(EventType.ActorBehavChange, ActorChangedBehav);
    }

    void OnDestroy()
    {
        EventManager.Instance.UnSubscribeFromEvent(EventType.ActorBehavChange, ActorChangedBehav);
    }

    void Update()
    {
        m_AttackTimeCounter += Time.deltaTime;

        if (m_AttackTimeCounter > m_AttackingInterval)
        {
            m_AttackTimeCounter = 0;
            PickActorToAttack();
        }
    }

    void ActorChangedBehav(EventMessage msg)
    {
        ActorChangeBehavMessage message = (ActorChangeBehavMessage)msg;

        if (m_AttackingActor == message.m_Actor)
            m_AttackingActor = null;

        switch (message.m_CurrentState)
        {
            case Actor.eStates.Attack:
                break;
            case Actor.eStates.Circle:
            {
                m_CirclingActor.Add(message.m_Actor);
                if (m_CirclingActor.Count == 5)
                    PickActorToCheer();
                break;
            }
            case Actor.eStates.Cheer:
                m_CheeringActor.Add(message.m_Actor);
                break;
            case Actor.eStates.Retreat:
                break;
        }
    }

    void PickActorToAttack()
    {
        if (m_CirclingActor.Count == 0)
        {
            Debug.Log("No circling actor to attack");
        }

        if (m_CheeringActor.Count == 0)
        {
            Debug.Log("No actor to attack");
            return;
        }

        List<Actor> actorList = m_CirclingActor.Count == 0 ? m_CheeringActor : m_CirclingActor;

        Actor actor = GetRandomActorFromList(actorList );
        if(actor)
        {
            m_AttackingActor = actor;
            m_AttackingActor.RequestState(Actor.eStates.Attack);
        }
    }

    void PickActorToCheer()
    {
        if (m_CirclingActor.Count == 0)
        {
            Debug.Log("No actor to Cheer");
            return;
        }
        Actor actor = GetRandomActorFromList(m_CirclingActor);
        if (actor)
        {
            actor.RequestState(Actor.eStates.Cheer);
        }
    }

    Actor GetRandomActorFromList(List<Actor> actorList)
    {
        int RandomIdx = Random.Range(0, actorList.Count - 1);
        Debug.Log(RandomIdx + "#@@#");
        if (actorList[RandomIdx])
        {
            Actor randomActor = actorList[RandomIdx];
            actorList.Remove(actorList[RandomIdx]);
            return randomActor;
        }
        return null;
    }
}
