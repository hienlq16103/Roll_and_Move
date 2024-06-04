using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour {

  [SerializeField] Sprite[] diceSides = new Sprite[6];
  [SerializeField] Image image;
  [SerializeField] Animator animator;

  private int randomSide = 0;
  private int finalSide = 0;
  private bool isRolling = false;

  public void ClickTheDice(){
    if (isRolling){
      return;
    }
    StartCoroutine("RollTheDice");
  }
  private IEnumerator RollTheDice(){
    isRolling = true;
    animator.SetBool("isRolling", isRolling);
    for (int i = 0; i <= 20; i++){
      randomSide = Random.Range(0, 6);
      image.sprite = diceSides[randomSide];
      yield return new WaitForSeconds(0.05f);
    }
    finalSide = randomSide + 1;
    Debug.Log("Dice value: " + finalSide);
    isRolling = false;
    animator.SetBool("isRolling", isRolling);
  }
}