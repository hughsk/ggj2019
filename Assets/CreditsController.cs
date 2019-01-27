using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour {
  void Update () {
    if (Input.GetButtonUp("Cancel") || Input.GetButtonUp("Jump") || Input.GetButtonUp("Submit")) {
      SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
  }
}
