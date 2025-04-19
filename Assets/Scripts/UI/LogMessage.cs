using UnityEngine;

public class LogMessage
{
    public string text;
    public float timestamp;

    public LogMessage(string text, float timestamp)
    {
        this.text = text;
        this.timestamp = timestamp;
    }
}