using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBubble : MonoBehaviour {
  [SerializeField] TextMeshPro tmpro;

  Transform xform;
  Vector3 startPos;
  bool writing = false;
  bool waiting = false;

  void OnEnable () {
    xform = GetComponent<Transform>();
    startPos = xform.localPosition;
    xform.localScale = Vector3.zero;
  }

  void Update () {
    xform.localScale = Vector3.Lerp(xform.localScale, writing ? Vector3.one : Vector3.zero, 0.2f);
    xform.localPosition = startPos + Vector3.up * (Mathf.Sin(Time.timeSinceLevelLoad * 2f) * 0.07f);
  }

  public IEnumerator WriteText (string text) {
    if (!writing && text.Length > 0 && !waiting) {
      writing = true;
      waiting = true;

      for (int i = 1; i <= text.Length; i++) {
        tmpro.text = text.Substring(0, i);
        yield return new WaitForSeconds(0.04f);
      }

      yield return new WaitForSeconds(1.2f);
      writing = false;
      yield return new WaitForSeconds(0.5f);
      waiting = false;
    }

    yield return null;
  }
}
