using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class StartText : MonoBehaviour {
  [SerializeField] int sceneIndex;
  TextMeshProUGUI text;
  bool loading = false;

  void OnEnable () {
    text = GetComponent<TextMeshProUGUI>();
  }

  void Update () {
    if (!loading && Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape)) {
      SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);
      loading = true;
    }

    if (Mathf.Repeat(Time.realtimeSinceStartup, 1.0f) < 0.5f) {
      text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);
    } else {
      text.color = new Color(text.color.r, text.color.g, text.color.b, 0.5f);
    }
  }
}
