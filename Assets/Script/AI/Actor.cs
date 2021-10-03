using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Actor : StateMachineAdvance
{
    public enum eStates
    {
        Idle,
        Wander, 
        Chase,
        Circle,
        Attack,
        Retreat,
        Flee,
        Cheer
    }

    public State[] m_States;
    
    public float        m_WalkSpeed;
    public float        m_RotationSpeed;
    public GameObject   m_TargetObject;

    private float        m_ThresholdDistance = 0;
    private Rigidbody    m_Rigidbody;

    private NavMeshAgent            m_NavMeshAgent;
    private CharacterController     m_CharController;

#region Properties
    public Vector3 Position
    {
        get { return transform.position; } private set { }
    }

    public Vector3 TargetActorPosition
    {
        get { return TargetActor.transform.position; } private set { }
    }

    public NavMeshAgent NavAgent
    {
        get { return m_NavMeshAgent; } private set { }
    }

    public GameObject TargetActor
    {
        get { return m_TargetObject; } private set { }
    }

    public Rigidbody RigidBody
    {
        get { return m_Rigidbody; } private set { }
    }

    public CharacterController Controller
    {
        get { return m_CharController; } private set { }
    }

    public float ThresholdDistance
    {
        get { return m_ThresholdDistance; } set { m_ThresholdDistance = value; }
    }
#endregion

    public void RequestState(eStates state)
    {
        RequestState((int)state);
    }

	public override void Awake ()
    {
        base.Awake();

        m_Rigidbody                 = GetComponent<Rigidbody>();
        m_NavMeshAgent              = GetComponent<NavMeshAgent>();
        m_CharController            = GetComponent<CharacterController>();
        m_CharController.enabled    = false;


        //Register states here
        RegisterState(new IdleBehav(this));
        RegisterState(new WanderBehav(this));
        RegisterState(new ChaseBehav(this));
        RegisterState(new CircleBehav(this));
        RegisterState(new AttackBehav(this));
        RegisterState(new RetreatBehav(this));
        RegisterState(new CheerBehav(this));

        RequestState(eStates.Idle);

        m_States = new State[m_allStates.Count];

        for(int i= 0; i<m_allStates.Count; i++)
        {
            m_States[i] = m_allStates[i];
        }
    }

    public override void Update()
    {
        base.Update();
        // always last //
        SwitchState();

        if (Input.GetKeyUp(KeyCode.P))          //TODO : thinking of a situation to change this 
        {
            RequestState(eStates.Chase);
        }
    }

    public void LookAtTargetActor(int inverseDirection = 1)
    {
        Vector3 lookAtDirection     = (TargetActorPosition - Position) * inverseDirection;
        lookAtDirection.y           = 0;
        Quaternion targetRotation   = Quaternion.LookRotation(lookAtDirection);
        transform.rotation          = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * m_RotationSpeed);
    }
}
