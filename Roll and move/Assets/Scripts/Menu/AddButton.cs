using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddButton : MonoBehaviour {
  public void AddPlayer(){
    MenuControl.Instance.AddEntry();
    Destroy(gameObject);
  }
}
