using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDetail
{
    public EventType                    m_EventType;
    public Action<EventMessage>         m_EventCallBack;

    public EventDetail(EventType eventType, Action<EventMessage> actions)
    {
        m_EventType = eventType;
        m_EventCallBack += actions;
    }
}

public class EventManager : MonoBehaviour
{

    public static EventManager m_Instance { private set; get; }

    public event EventHandler<EventMessage> m_EventHandler;

    private List<EventDetail> m_EventDetailList;

#region Properties

    public static EventManager Instance
    {
        get { return m_Instance; }
    }

#endregion


    private void Awake()
    {
        if (m_Instance == null)
            m_Instance = this;

        m_EventDetailList = new List<EventDetail>();
    }

    public void SubscribeToEvent(EventType eventType, Action<EventMessage> action)
    {
        EventDetail detail = m_EventDetailList?.Find((x) => x.m_EventType == eventType);
        if (detail == null)
        {
            Debug.Log("detail is null");
            detail = new EventDetail(eventType, action);
            m_EventDetailList.Add(detail);
        }
        else
            detail.m_EventCallBack += action;   
    }

    public void UnSubscribeFromEvent(EventType eventType, Action<EventMessage> action)
    {
        EventDetail detail = m_EventDetailList?.Find((x) => x.m_EventType == eventType);
        if (detail == null)
        {
            Debug.Log("detail is null");
            return;
        }
        detail.m_EventCallBack -= action;
        if (detail.m_EventCallBack == null)
            m_EventDetailList.Remove(detail);
    }

    public void TriggerEvent(EventType eventType, EventMessage eventMessage)
    {
        EventDetail detail = m_EventDetailList.Find((x) => x.m_EventType == eventType);
        if (detail == null)
        {
            Debug.Log("detail is null");
            return;
        }
        detail.m_EventCallBack?.DynamicInvoke(eventMessage);
    }
}
