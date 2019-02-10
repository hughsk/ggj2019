using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour {
  void Update () {
    if (Input.anyKeyDown) {
      SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
  }
}
