using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class PlaytestingLogger
{
    private static readonly string filePath = Application.persistentDataPath + "/playtest_log.csv";

    static PlaytestingLogger()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "Time,Event,Other\n");
        }
    }

    public static void LogAction(string eventStr, string other = "")
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(Time.time);
        string logEntry = $"{(int)timeSpan.TotalHours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2},{eventStr},{other}\n";
        File.AppendAllText(filePath, logEntry);
    }
}
