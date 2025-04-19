using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class GameLog : MonoBehaviour
{
    public TextMeshProUGUI uiText;

    public int maxMessages = 6;

    public float messageDuration = 5f;

    private List<LogMessage> messages = new List<LogMessage>();

    public void PushMessage(string message)
    {
        string formattedMessage = System.DateTime.Now.ToString("HH:mm:ss") + " - " + message;

        if (messages.Count >= maxMessages)
        {
            messages.RemoveAt(0);
        }

        messages.Add(new LogMessage(formattedMessage, Time.time));
        UpdateLog();
    }

    void OnEnable()
    {
        EventManager.OnAnyEvent += HandleAnyEvent;
    }

    void OnDisable()
    {
        EventManager.OnAnyEvent -= HandleAnyEvent;
    }

    void HandleAnyEvent(System.Type eventType, object[] parameters)
    {
        string paramInfo = "";
        if (parameters != null)
        {
            foreach (var param in parameters)
            {
                paramInfo += param.ToString() + " ";
            }
            PushMessage($"{eventType.Name}: {paramInfo.Trim()}");
        } else
        {
            PushMessage($"{eventType.Name}");
        }
        
    }

    void Update()
    {
        bool messageRemoved = false;
        for (int i = messages.Count - 1; i >= 0; i--)
        {
            if (Time.time - messages[i].timestamp > messageDuration)
            {
                messages.RemoveAt(i);
                messageRemoved = true;
            }
        }

        if (messageRemoved)
        {
            UpdateLog();
        }
    }

    private void UpdateLog()
    {
        string allMessages = "";
        foreach (var msg in messages)
        {
            allMessages += msg.text + "\n";
        }
        uiText.text = allMessages;
    }
}
