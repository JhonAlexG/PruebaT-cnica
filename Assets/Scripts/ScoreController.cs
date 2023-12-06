using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;
using System;
public class ScoreController : MonoBehaviour
{
    //public GameObject scoreUI;
    public string fileName;
    private ScoreData scoreData = new ScoreData();

    void Awake()
    {
        fileName = Application.dataPath + "/scores.json";
        LoadData();
    }
 
    public IEnumerable<Score> GetHighScores()
    {
        
        return scoreData.scores.OrderByDescending(s => s.score);
    }

    public void AddScore(Score score)
    {
        scoreData.scores.Add(score);
        SaveScores();
    }

    public void SaveScores()
    {
        var json = JsonUtility.ToJson(scoreData);
        File.WriteAllText(Application.dataPath + "/scores.json", json);
    }

    public void LoadData()
    {
        if (File.Exists(Application.dataPath + "/scores.json"))
        {
            string contents = File.ReadAllText(Application.dataPath + "/scores.json");
            scoreData = JsonUtility.FromJson<ScoreData>(contents);
        }
        else
        {
            Debug.Log("No file found");
        }
    }

    //comprobar si hay un nuevo record
    public bool IsNewRecord(int newScore)
    {
        var recordsArray = GetHighScores().ToArray();

        if (newScore > recordsArray[0].score)
        {
            return true;
        }
        return false;
    }
}
