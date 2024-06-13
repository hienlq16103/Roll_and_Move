using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovePlayer : MonoBehaviour {
  [SerializeField] ExtraEntry extraEntry;
  public void Remove(){
    if(extraEntry.entryIndex != GameSetting.Instance.playerCount){
      return;
    }
    MenuControl.Instance.RemoveEntry();
    Destroy(gameObject);
  }
}
