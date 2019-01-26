using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AvocadoController : MonoBehaviour {
  [SerializeField] Transform cameraTransform;
  [SerializeField] LayerMask swingMask;
  [SerializeField] [Range(0f, 5f)] float swingRadius = 1.5f;
  Rigidbody rb;
  Transform xform;

  Vector3 forward;
  Vector3 right;

  // Left (-1) or right (+1)?
  float swingDirection = 0f;

  void OnEnable () {
    forward = new Vector3(cameraTransform.forward.x, 0f, cameraTransform.forward.z).normalized;
    right = new Vector3(cameraTransform.right.x, 0f, cameraTransform.right.z).normalized;
    rb = GetComponent<Rigidbody>();
    xform = GetComponent<Transform>();
  }

  void Update () {
    if (Input.GetButtonUp("Jump")) {
      var targets = GetSwingTargets();

      for (int i = 0; i < targets.targetCount; i++) {
        if (targets.inRanges[i]) {
          targets.targets[i].Smash();
        }
      }
    }
  }

  Collider[] colliders = new Collider[10];
  SwingTarget[] targets = new SwingTarget[10];
  bool[] inRange = new bool[10];

  struct SwingTargetResults {
    public Collider[] colliders;
    public SwingTarget[] targets;
    public bool[] inRanges;
    public int targetCount;
  };

  SwingTargetResults GetSwingTargets () {
    var swingVector = GetSwingMiddle();
    var swingOrigin = xform.position;

    int limit = Physics.OverlapSphereNonAlloc(
      swingOrigin,
      swingRadius,
      colliders,
      swingMask,
      QueryTriggerInteraction.Ignore
    );

    for (int j = 0; j < limit; j++) {
      var origin = colliders[j].transform.position;

      if (!(inRange[j] = (Mathf.Sign((origin - swingOrigin).z) == Mathf.Sign(swingDirection)))) continue;

      targets[j] = colliders[j].GetComponent<SwingTarget>();
      if (!(inRange[j] = targets[j] != null)) continue;
    }

    return new SwingTargetResults {
      colliders = colliders,
      targetCount = limit,
      inRanges = inRange,
      targets = targets,
    };
  }

  void FixedUpdate () {
    var relativeFrame = 20f * GetCameraRelative(
      Input.GetAxis("Horizontal"),
      Input.GetAxis("Vertical")
    );

    if (Mathf.Abs(relativeFrame.z) > 0.001f) {
      swingDirection = Mathf.Sign(relativeFrame.z);
    }

    rb.AddForce(relativeFrame, ForceMode.Acceleration);
  }

  Vector3 GetCameraRelative (float x, float y) {
    var frame = new Vector3(x, 0f, y);
    return new Vector3(
      -Vector3.Dot(right, frame), 0f, -Vector3.Dot(forward, frame)
    );
  }

  Vector3 GetSwingMiddle () {
    return GetCameraRelative(swingDirection, 0f);
  }
}
