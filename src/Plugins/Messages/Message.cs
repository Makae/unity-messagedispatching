using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Message
{
    public float DispatchTime { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public string Content { get; set; }
    public Hashtable Args { get; set; }

    public Message(float dispatchTime, string from, string to, string content, Hashtable args)
    {
        this.DispatchTime = dispatchTime;
        this.From = from;
        this.To = to;
        this.Content = content;
        this.Args = args;
    }

}