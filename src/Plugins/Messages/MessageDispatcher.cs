using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// MonoBehaviour is necessary because the Update Method has to be called for the delayed messages
public class MessageDispatcher {
    private static MessageDispatcher Inst = null;
    private List<Message> Messages = new List<Message>();

    private MessageDispatcher() { }

    public static MessageDispatcher Instance()
    {
        if (MessageDispatcher.Inst == null)
            MessageDispatcher.Inst = new MessageDispatcher();
        return MessageDispatcher.Inst;
    }

    public void Update()
    {
        MessageDispatcher.Instance()._Update();
    }
      
    private void _Update()
    {
        List<Message> new_Messages = new List<Message>();

        for (int i = 0; i < this.Messages.Count; i++)
        {
            Message Message = this.Messages[i];
            if (Message.DispatchTime <= Time.time)
            {
                this.Discharge(Message);
            }
            else {
                new_Messages.Add(Message);
            }
        }
        this.Messages = new_Messages;
    }

    // Instantly send Message to receivers
    public void Discharge(Message Message)
    {
        List<object> group;
        if (EntityManager.IsGroup(Message.To))
        {
            group = EntityManager.GetGroup(Message.To);
        }
        else {
            object to = EntityManager.GetEntity(Message.To);
            group = new List<object>();
            group.Add(to);
        }

        foreach (IMessageReceiver entity in group)
        {
            if (entity != null && entity is  IMessageReceiver)
            {
                Debug.Log("fff");
                ((IMessageReceiver)entity).OnMessage(Message);
            }
        }

    }

    public void Dispatch(float delay, string from, string to)
    {
        this.Dispatch(delay, from, to, "", new Hashtable());
    }

    public void Dispatch(float delay, string from, string to, string message)
    {
        this.Dispatch(delay, from, to, message, new Hashtable());
    }

    // Send delayed message
    public void Dispatch(float delay, string from, string to, string message, Hashtable args)
    {
        Message Message = new Message(Time.time + delay, from, to, message, args);
        Debug.Log("received... directly init?");
        if (delay <= 0)
        {
            Debug.Log("YES");
            this.Discharge(Message);
        }
        else
        {
            this.Messages.Add(Message);
        }

    }

}