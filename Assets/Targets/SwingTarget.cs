using UnityEngine;
using System.Collections;

public class SwingTarget : MonoBehaviour {
  ParticleSystem particleSystem;
  bool smashed = false;

  public void Smash (ParticleSystem system) {
    if (smashed) return;
    smashed = true;
    particleSystem = Instantiate<ParticleSystem>(system);
    particleSystem.transform.position = transform.position + Vector3.up;
    StartCoroutine(ExitRoutine());
  }

  IEnumerator ExitRoutine () {
    for (float i = 0f; i < 90f; i += 10f) {
      yield return new WaitForEndOfFrame();
      transform.Rotate(new Vector3(0f, 0f, 10f));
    }
    yield return new WaitForSeconds(2f);
    Destroy(particleSystem.gameObject);
    Destroy(gameObject);
  }
}
