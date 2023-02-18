using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ScoreItem : MonoBehaviour
{
    [SerializeField] private TMP_Text nicknameText;
    [SerializeField] private TMP_Text scoreText;

    public void SetData(string nick, int score)
    {
        nicknameText.text = nick;
        scoreText.text = score.ToString();
    }
}
