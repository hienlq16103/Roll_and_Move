using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimation : MonoBehaviour {
  [SerializeField] Animator animator;

  public void SetSelectState(bool isSelecting){
    animator.SetBool("isSelecting", isSelecting);
  }
}
