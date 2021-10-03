using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    TextMeshProUGUI scoreText;

    public long totalScore;
    public long maxScore = 0;

    private void Awake()
    {
        scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        var cratesValues = FindObjectsOfType<ObjValue>();
        var scores = new List<long>();

        foreach (var item in cratesValues)
        {
            scores.Add(item.computedValue);
        }

        totalScore = 0;
        foreach (var value in scores)
        {
            totalScore += value;
        }
        if (totalScore >= maxScore)
        {
            maxScore = totalScore;
        }
        scoreText.text = $"Current score : {totalScore}.\n" +
        $"Max score : {maxScore}.\n";


    }
}
