using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScoreKeeper : MonoBehaviour
{
   [SerializeField] TextMeshProUGUI scoreText;

   ScoreKeeper scoreKeeper;
   
   
    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    
    void Start()
    {
        scoreText.text = "Death is Only the Beginning\n" + scoreKeeper.GetScore();
    }

}
