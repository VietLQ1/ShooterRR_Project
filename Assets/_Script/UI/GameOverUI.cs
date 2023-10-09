using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField]
    Text Score;
    [SerializeField]
    Text HiScore;
    // Start is called before the first frame update
    void Start()
    {
        Score.text = "Your Score: " + GameManager.instance.Score.ToString();
        HiScore.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
        GameManager.instance.StopAllCoroutines();
    }

}
