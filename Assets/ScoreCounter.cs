using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreCounter : MonoBehaviour
{
    TextMeshProUGUI scoreText;

    private void Awake()
    {
        scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        scoreText.text = $"Score : {transform.position.y.ToString("F1")}.";
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Box")) return;
        transform.position += Vector3.up / 100;
    }
}
