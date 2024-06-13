using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Dice : MonoBehaviour {
  
  [HideInInspector] public bool IsRolledThisTurn = false;

  [SerializeField] Sprite[] diceSides = new Sprite[6];
  [SerializeField] Image image;
  [SerializeField] Animator animator;
  [SerializeField] AudioSource source;
  [SerializeField] AudioClip diceRoll;

  private int randomSide = 0;
  private int finalSide = 0;
  private bool isRolling = false;

  public int DiceValue(){
    return finalSide;
  }

  public void ClickTheDice(){
    if (isRolling){
      return;
    }
    if (IsRolledThisTurn){
      return;
    }
    source.PlayOneShot(diceRoll);
    StartCoroutine(RollTheDice());
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
    isRolling = false;
    animator.SetBool("isRolling", isRolling);
    IsRolledThisTurn = true;
  }
}
