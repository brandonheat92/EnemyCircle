using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBehav : ActorBaseState
{
    Vector3 m_MoveDirection = Vector3.zero;
    float m_RotateSpeed = 10;

    public CircleBehav(Actor actor) : base(actor, Actor.eStates.Circle)
    { }

    public override void OnEnter()
    {
        base.OnEnter();
        m_Actor.NavAgent.enabled = false;
        m_Actor.Controller.enabled = true;
        m_Actor.RigidBody.velocity = Vector3.zero;
        
        m_Actor.StartCoroutine(InitialDelay());
        EventManager.Instance.TriggerEvent(EventType.ActorBehavChange, new ActorChangeBehavMessage(m_Actor, Actor.eStates.Circle));
    }

    public override void OnExit()
    {
        base.OnExit();

        m_Actor.NavAgent.enabled = true;
        m_Actor.Controller.enabled = false;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        m_Actor.LookAtTargetActor();
        MoveAround();

        float distance = Vector3.Distance(m_Actor.TargetActor.transform.position, m_Actor.transform.position);
        if (distance > 10)
            m_Actor.RequestState(Actor.eStates.Chase);

        if (Input.GetKeyUp(KeyCode.O))
            m_Actor.RequestState(Actor.eStates.Cheer);
    }

    void LookAt()
    {
        Vector3 lookAtDirection = (m_Actor.TargetActor.transform.position - m_Actor.transform.position);
        lookAtDirection.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(lookAtDirection);
        m_Actor.transform.rotation = Quaternion.Slerp(m_Actor.transform.rotation, targetRotation, Time.deltaTime * m_RotateSpeed);
    }

    void MoveAround()
    {
        Vector3 dir = (m_Actor.TargetActor.transform.position - m_Actor.transform.position).normalized;
        Vector3 pDir = Quaternion.AngleAxis(90, Vector3.up) * dir; 
        Vector3 movedir = Vector3.zero;

        Vector3 finalDirection = Vector3.zero;

        finalDirection = (pDir * m_MoveDirection.normalized.x);

        movedir += finalDirection * 3 * Time.deltaTime;
        movedir.y = 0;

        m_Actor.Controller.Move(movedir);
    }

    IEnumerator ChangeDirection()
    {
        yield return new WaitForSeconds(4);

        m_MoveDirection = Vector3.zero;

        yield return new WaitForSeconds(1);
        
        m_MoveDirection = Random.Range(1, 10) % 2 == 0 ? Vector3.right : -Vector3.right;
    }

    IEnumerator InitialDelay()
    {
        yield return new WaitForSeconds(2);

        m_MoveDirection = Random.Range(1, 10) % 2 == 0 ? Vector3.right : -Vector3.right;
    }

    //IEnumerator Move()
    //{
    //    angle += speed * Time.deltaTime;
    //    float x = m_Actor.TargetActor.transform.position.x + Mathf.Cos(angle) * radius;
    //    float y = m_Actor.TargetActor.transform.position.z + Mathf.Sin(angle) * radius;

    //    yield return new WaitForSeconds(2);

    //    m_Actor.NavAgent.SetDestination(new Vector3(x, m_Actor.transform.position.y, y));
    //}
}
