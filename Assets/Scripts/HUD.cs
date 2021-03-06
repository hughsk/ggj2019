﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using UnityEngine.SceneManagement;
using TMPro;
using PlayerNumber = AvocadoController.PlayerNumber;
using String = System.String;

public class HUD : MonoBehaviour
{
    [UnityEngine.Serialization.FormerlySerializedAs("scoreText")]
    public TextMeshProUGUI scoreText1;
    public TextMeshProUGUI scoreText2;
    [Range(1, 2)]
    public int playerCount = 1;
    public Text finalScore;
    public float[] score;
    public float minScore = 0;
    public float maxScore = 16000000;
    public Slider scoreBar;
    public float pointValue;
    public int timeLeft;
    public TextMeshProUGUI countdown;
    private string scoreDesc;
    bool keepTiming;
    public bool hasFinished = false;
    public Selectable selectOnEnd;

    public string MenuName;
    public string CreditsName;

    GameObject[] pauseObjects;
    GameObject[] scoreObjects;

    void OnEnable () {
      score = new float[playerCount];
    }

    void Start()
    {
        StartCoroutine("LoseTime");
        Time.timeScale = 1;

        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        scoreObjects = GameObject.FindGameObjectsWithTag("ShowOnFinish");

        hideScore();
        hidePaused();
    }

    public void BoostScore (PlayerNumber player, float boost) {
      switch (player) {
        case PlayerNumber.Player1: SetScoreText(scoreText1, score[0] += boost); break;
        case PlayerNumber.Player2: SetScoreText(scoreText2, score[1] += boost); break;
      }


      scoreBar.value = GetTotal();
    }

    float GetTotal () {
      var total = 0f;
      for (int i = 0; i < score.Length; i++) total += score[i];
      return total;
    }

    void Update()
    {
        countdown.text = ("Time: " + Mathf.Max(timeLeft, 0));

        if (timeLeft == 0)
        {
            showScore();
        }

        if (
          Input.GetKeyDown(KeyCode.Escape) ||
          Input.GetKeyDown(KeyCode.Joystick1Button8) ||
          Input.GetKeyDown(KeyCode.Joystick1Button9) ||
          Input.GetKeyDown(KeyCode.Joystick2Button8) ||
          Input.GetKeyDown(KeyCode.Joystick2Button9)
        ) {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                showPaused();
            }
            else if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                hidePaused();
            }
        }
    }

    void SetScoreText(TextMeshProUGUI textMesh, float score)
    {
       textMesh.text = "$" + String.Format("{0:n0}", score) + " smashed";
    }

    IEnumerator SelectMenu () {
      yield return new WaitForSeconds(1f);
      selectOnEnd.Select();
      yield return null;
    }

    public void showScore()
    {
        hasFinished = true;

        foreach (GameObject g in scoreObjects)
        {
            g.SetActive(true);
        }

        var score = GetTotal();
        var prefix = playerCount > 1
          ? "Together, you earned"
          : "You earned";

        finalScore.text = finalScore.text = prefix + " $" + String.Format("{0:n0}", score) + ". That's just enough to " + scoreDesc;
        StartCoroutine(SelectMenu());

        if (score >= 0 && score <= 499)
        {
            scoreDesc = "live at home!!";
        }

        else if (score >= 500 && score <= 99999)
        {
            scoreDesc = "get a plane ticket!!";
        }

        else if (score >= 100000 && score <= 149999)
        {
            scoreDesc = "buy an apartment!!";
        }

        else if (score >= 150000 && score <= 299999)
        {
            scoreDesc = "live in a motor home!!";
        }

        else if (score >= 300000 && score <= 499999)
        {
            scoreDesc = "own a house!!";
        }

        else if (score >= 500000 && score <= 999999)
        {
            scoreDesc = "live free in a boat house!!";
        }

        else if (score >= 1000000 && score <= 1999999)
        {
            scoreDesc = "own a farm!!";
        }

        else if (score >= 2000000 && score <= 2999999)
        {
            scoreDesc = "have a private mansion!!";
        }

        else if (score >= 3000000 && score <= 3999999)
        {
            scoreDesc = "get an actual plane!!";
        }

        else if (score >= 4000000 && score <= 15999999)
        {
            scoreDesc = "casually purchase a skyscraper!!";
        }

        else if (score >= 16000000 && score <= 999999999)
        {
            scoreDesc = "purchase a private island!!";
        }
    }

    public void hideScore()
    {
        foreach (GameObject g in scoreObjects)
        {
            g.SetActive(false);
        }
    }

    public void showPaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    public void hidePaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }

    public void OnMenu_Clicked()
    {
        SceneManager.LoadScene(MenuName);
    }

    public void OnCredit_Clicked()
    {
        SceneManager.LoadScene(CreditsName);
    }

    public void OnReturn_Clicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
