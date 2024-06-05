using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

  [SerializeField] float movementSpeed = 5.0f;

  private float step;

  public IEnumerator Move(Transform targetTransform){
    while(Vector3.Distance(transform.position, targetTransform.position) > Mathf.Epsilon){
      step = movementSpeed * Time.deltaTime;
      transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, step);
      yield return null;
    }
  }
}
