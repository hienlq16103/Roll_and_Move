using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraEntry : MonoBehaviour {
  public int entryIndex;

  public void ChangePlayerName(string value){
    if (value == ""){
      return;
    }
    GameSetting.Instance.playerNames[entryIndex - 1] = value;
  }
}
