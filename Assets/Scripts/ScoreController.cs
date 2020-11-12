using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public int score;
    private Text metinKısmı;

    private void Start()
    {
        metinKısmı = GetComponent<Text>();
        ScoreSifirlama();
    }
    public void ScoreArttir(int puan)
    {
        score += puan;
        Debug.Log(score);
        metinKısmı.text = score.ToString();
    }
    public void ScoreSifirlama()
    {
        score = 0;
        metinKısmı.text = score.ToString();
    }
}
