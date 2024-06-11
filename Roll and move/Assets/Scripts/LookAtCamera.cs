using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {

  Transform cameraTransform;

  private void Awake() {
    cameraTransform = Camera.main.transform;
    transform.rotation = cameraTransform.rotation;
  }
  private void OnEnable() {
    transform.rotation = cameraTransform.rotation;
  }
  private void Update() {
    transform.rotation = cameraTransform.rotation;
  }
}
