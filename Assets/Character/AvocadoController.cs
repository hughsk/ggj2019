using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class AvocadoController : MonoBehaviour {
  [SerializeField] Transform cameraTransform;
  [SerializeField] LayerMask swingMask;
  [SerializeField] [Range(0f, 5f)] float swingRadius = 1.5f;
  [SerializeField] AudioClip[] swingSounds;
  [SerializeField] SpriteRenderer sprite;
  [SerializeField] float animPeriod = 0.5f;

  float animCounter = 0f;
  int walkIndex = 0;

  Rigidbody rb;
  Transform xform;
  AudioSource source;

  Vector3 forward;
  Vector3 right;
  AvoAnimation currentAnimation;

  // Left (-1) or right (+1)?
  float swingDirection = 0f;

  void OnEnable () {
    currentAnimation = animIdle;
    forward = new Vector3(cameraTransform.forward.x, 0f, cameraTransform.forward.z).normalized;
    right = new Vector3(cameraTransform.right.x, 0f, cameraTransform.right.z).normalized;
    rb = GetComponent<Rigidbody>();
    xform = GetComponent<Transform>();
    source = GetComponent<AudioSource>();
  }

  bool isSwinging = false;

  void Update () {
    if (!isSwinging && Input.GetButtonUp("Jump")) {
      var targets = GetSwingTargets();

      PlaySound(swingSounds);
      StartCoroutine(SwingAnimation());

      for (int i = 0; i < targets.targetCount; i++) {
        if (targets.inRanges[i]) {
          targets.targets[i].Smash();
        }
      }
    }

    var speed = rb.velocity.magnitude;
    if (speed > 0.01f) {
      animCounter += Time.deltaTime * Mathf.Min(1f, speed * 0.25f);
      while (animCounter > animPeriod) {
        animCounter -= animPeriod;
        walkIndex = (walkIndex + 1) % 3;
      }

      switch (walkIndex) {
        case 0: sprite.sprite = currentAnimation.standing; break;
        case 1: sprite.sprite = currentAnimation.walk1; break;
        case 2: sprite.sprite = currentAnimation.walk2; break;
      }
    } else {
      sprite.sprite = currentAnimation.standing;
    }
  }

  IEnumerator SwingAnimation () {
    for (int i = 0; i < animSwing.Length; i++) {
      yield return new WaitForSeconds(0.05f);
      currentAnimation = animSwing[i];
    }
    yield return new WaitForSeconds(0.05f);
    currentAnimation = animIdle;
    isSwinging = false;
    yield return null;
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

  [System.Serializable]
  struct AvoAnimation {
    public Sprite standing;
    public Sprite walk1;
    public Sprite walk2;
  }

  [SerializeField] AvoAnimation animIdle;
  [SerializeField] AvoAnimation[] animSwing;

  SwingTargetResults GetSwingTargets () {
    var swingVector = GetSwingMiddle();
    var swingOrigin = xform.position;

    int limit = Physics.OverlapSphereNonAlloc(
      swingOrigin,
      swingRadius,
      colliders,
      swingMask,
      QueryTriggerInteraction.Collide
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
    var relativeFrame = 30f * GetCameraRelative(
      Input.GetAxis("Horizontal"),
      Input.GetAxis("Vertical")
    );

    if (Mathf.Abs(relativeFrame.z) > 0.001f) {
      swingDirection = Mathf.Sign(relativeFrame.z);
      sprite.flipX = relativeFrame.z > 0f;
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

  void PlaySound (AudioClip[] sounds) {
    int random = Mathf.FloorToInt(Random.Range(0f, sounds.Length - 1f));
    source.PlayOneShot(sounds[random]);
  }
}
