using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public RowUI rowUI;
    public ScoreController scoreController;

    void Start()
    {
        var scoreArray = scoreController.GetHighScores().ToArray();
        if (scoreArray.Length < 5)
        {
            for (int i = 0; i < scoreArray.Length; i++)
            {
                var row = Instantiate(rowUI, transform).GetComponent<RowUI>();
                row.rank.text = (i + 1).ToString() + ".";
                row.score.text = scoreArray[i].score.ToString();
            }
        } else
        {
        for (int i = 0; i < 5; i++)
        {
            var row = Instantiate(rowUI, transform).GetComponent<RowUI>();
            row.rank.text = (i + 1).ToString()+".";
            row.score.text = scoreArray[i].score.ToString();
        }
        }
    }
}
