using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreEntry : MonoBehaviour {
  [SerializeField] TMP_Text place;
  [SerializeField] TMP_Text playerName;
  [SerializeField] TMP_Text turns;
  [SerializeField] TMP_Text bonus;
  [SerializeField] TMP_Text fail;

  public void SetPlace(int value){
    place.text = value.ToString();
  }
  public void SetPlayerName(string name){
    playerName.text = name;
  }
  public void SetTurnCount(int turnCount){
    turns.text = turnCount.ToString();
  }
  public void SetBonusCount(int bonusCount){
    bonus.text = bonusCount.ToString();
  }
  public void SetFailCount(int failCount){
    fail.text = failCount.ToString();
  }
}
