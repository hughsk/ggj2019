using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshPro))]
public class ScoreFloater : MonoBehaviour {
  TextMeshPro text;
  Transform xform;

  void OnEnable () {
    text = GetComponent<TextMeshPro>();
    xform = GetComponent<Transform>();
  }

  void Update () {
    var color = text.color;
    color.a -= Time.deltaTime;
    xform.position += Vector3.up * Time.deltaTime * 1.8f;
    Debug.Log(xform.position);
    text.color = color;
    if (color.a <= 0f) {
      // Destroy(gameObject);
    }
  }

  public void SetValue (float value) {
    text.text = "$" + Mathf.Floor(value);
  }
}
