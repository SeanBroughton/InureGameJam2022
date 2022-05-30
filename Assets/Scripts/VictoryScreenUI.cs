using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VictoryScreenUI : MonoBehaviour
{
 [SerializeField] TextMeshProUGUI scoreText;

   ScoreKeeper scoreKeeper;
   
   
    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    
    void Start()
    {
        scoreText.text = "This is Only the Beginning\n" + scoreKeeper.GetScore();
    }
}
