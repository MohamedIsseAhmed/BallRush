using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private int score;
  

    private void OnEnable()
    {
        InvisibleWall.scoreEvent += InvisibleWall_scoreEvent;
    }
    
    private void InvisibleWall_scoreEvent()
    {
        score++;
    }

    void Update()
    {
        scoreText.text = "GOAL : " + score;
    }
    private void OnDisable()
    {
        InvisibleWall.scoreEvent -= InvisibleWall_scoreEvent;
    }
}
