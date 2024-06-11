using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour {
  [SerializeField] float destroyDelay = 3.0f;

  public TMP_Text textObject;
  
  private void Awake() {
    Destroy(gameObject, destroyDelay);
  }
}
