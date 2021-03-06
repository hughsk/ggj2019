﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGroups : MonoBehaviour {
  [SerializeField] AvocadoController[] player;
  [SerializeField] Transform[] blockers;
  [SerializeField] float groupSize = 5f;
  [SerializeField] Transform[] items;

  Transform[] playerTransform;
  Transform cameraTransform;
  Vector3 playerDiff;
  Vector3[] blockerDiffs;
  Dictionary<int, List<Transform>> groups = new Dictionary<int, List<Transform>>();

  void OnEnable () {
    cameraTransform = Camera.main.transform;
    playerTransform = new List<AvocadoController>(player)
      .ConvertAll(player => player.transform)
      .ToArray();
    playerDiff = cameraTransform.position - playerTransform[0].position;
    blockerDiffs = new List<Transform>(blockers)
      .ConvertAll(blocker => blocker.position - playerTransform[0].position)
      .ToArray();
  }

  List<int> flaggedForDeletion = new List<int>();

  void Update () {
    int groupId = Mathf.RoundToInt(playerTransform[0].position.x / groupSize);

    for (int i = groupId - 1; i <= groupId + 1; i++) {
      if (!groups.ContainsKey(i)) {
        groups[i] = CreateGroup(i);
      }
    }

    flaggedForDeletion.Clear();
    foreach (var index in groups.Keys) {
      if (System.Math.Abs(index - groupId) > 2) {
        var group = groups[index];
        for (var i = 0; i < group.Count; i++) {
          if (group[i] != null) Destroy(group[i].gameObject);
        }

        flaggedForDeletion.Add(index);
      }
    }

    for (int i = 0; i < flaggedForDeletion.Count; i++) {
      groups.Remove(flaggedForDeletion[i]);
    }

    var cameraPosition = cameraTransform.position;
    var playerPositionAverage = 0f;

    for (int i = 0; i < playerTransform.Length; i++) {
      playerPositionAverage += playerTransform[i].position.x / (float)playerTransform.Length;
    }
    cameraTransform.position = new Vector3(
      playerPositionAverage + playerDiff.x,
      cameraPosition.y,
      cameraPosition.z
    );

    for (int i = 0; i < blockers.Length; i++) {
      var position = blockers[i].position;
      blockers[i].position = new Vector3(
        playerPositionAverage + blockerDiffs[i].x,
        position.y,
        position.z
      );
    }
  }

  List<Transform> CreateGroup (int index) {
    var list = new List<Transform>();
    var xMax = ((float)index + 0.5f) * groupSize;
    var xMin = ((float)index - 0.5f) * groupSize;
    var zMax = +3f;
    var zMin = -3f;

    for (int i = 0; i < 30; i++) {
      var prefab = items[Random.Range(0, items.Length)];
      var position = new Vector3(
        Random.Range(xMin, xMax), 0f, Random.Range(zMin, zMax)
      );

      var item = Instantiate<Transform>(prefab);
      item.position = position;
      list.Add(item);
    }

    return list;
  }
}
