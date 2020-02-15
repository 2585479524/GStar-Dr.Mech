using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message
{
    public string role;
    public string content;

    public Message(string role, string content)
    {
        this.role = role;
        this.content = content;
    }
}
public class Dialog
{

    public int level;
    public Message StartMsg;
    public Message EndMsg;

    public Dialog(int level, Message startMsg, Message endMsg)
    {
        this.level= level;
        this.StartMsg = startMsg;
        this.EndMsg = endMsg;
    }

}
