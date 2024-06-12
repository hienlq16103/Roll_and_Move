using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimation : MonoBehaviour {
  [SerializeField] Animator animator;
  private void OnMouseEnter() {
    animator.SetBool("isSelecting", true);
  }
  private void OnMouseExit() {
    animator.SetBool("isSelecting", false);
  }
}
