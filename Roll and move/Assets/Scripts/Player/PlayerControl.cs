using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerControl : MonoBehaviour {

  [SerializeField] Attributes attributes;
  [SerializeField] GameObject canva;
  [SerializeField] TMP_Text textObject;

  private float step;

  public IEnumerator Move(Transform targetTransform){
    while(Vector3.Distance(transform.position, targetTransform.position) > Mathf.Epsilon){
      step = attributes.movementSpeed * Time.deltaTime;
      transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, step);
      yield return null;
    }
  }
  public void SetText(string name, Color textColor){
    textObject.text = name + "\n▼";
    textObject.color = textColor;
  }
  public void EnableCanva(){
    canva.SetActive(true);
  }
  public void DisableCanva(){
    canva.SetActive(false);
  }
}
