using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {
  Transform xform;
  Transform cam;

  float startHeight;

  void OnEnable () {
    xform = GetComponent<Transform>();
    cam = Camera.main.transform;
    startHeight = xform.position.y;
  }

  void Update () {
    xform.LookAt(cam.transform);
  }
}
