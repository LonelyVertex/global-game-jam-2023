using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveDataController
{
    private const string kScoresKey = "scores";
    
    [Serializable]
    public class ScoreList
    {
        [Serializable]
        public class ScoreData
        {
            public string n;
            public int s;
        }

        public List<ScoreData> scores;

        public ScoreList()
        {
            scores = new List<ScoreData>();
        }
    }
    
    public void SaveHighScore(string nick, int score)
    {
        var savedScores = GetScores();

        if (savedScores.scores.Count == 0)
        {
            savedScores.scores.Add(new ScoreList.ScoreData { n = nick, s = score });
        }
        else
        {
            for (var i = 0; i < savedScores.scores.Count; i++)
            {
                if (savedScores.scores[i].s >= score)
                    continue;
            
                savedScores.scores.Insert(i, new ScoreList.ScoreData { n = nick, s = score });
                break;
            }
        }
        
        savedScores.scores = savedScores.scores.Take(10).ToList();

        PlayerPrefs.SetString(kScoresKey, JsonUtility.ToJson(savedScores));
    }

    public ScoreList GetScores()
    {
        var serializedScores = PlayerPrefs.GetString(kScoresKey);
        var savedScores = JsonUtility.FromJson<ScoreList>(serializedScores);

        return savedScores ?? new ScoreList();
    }
}
