using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class HUD : MonoBehaviour
{
    public Text scoreText;
    private int score;
    public int timeLeft;
    public Text countdown;


    void Start()
    {        
        StartCoroutine("LoseTime");
        Time.timeScale = 1;
    }

    void Update()
    {
        countdown.text = ("" + timeLeft);
        SetScoreText();
    }

    /*void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag ( "Pick Up"))
        {
            other.gameObject.SetActive (false);
            score = score + 1;
            SsetScoreText ();
        }
    }*/

    void SetScoreText()
    {
        scoreText.text = "$" + score.ToString("f0");
    }

    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }
}
