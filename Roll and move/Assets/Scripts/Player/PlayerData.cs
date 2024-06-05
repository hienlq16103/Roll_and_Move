using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {
  
  private string playerName = "Player 0";
  private int turnCount = 0;
  private int turnRemained = 1;
  private int bonusSectorCount = 0;
  private int failSectorCount = 0;

  public string PlayerName(){
    return playerName;
  }
  public int TurnCount(){
    return turnCount;
  }
  public int TurnRemained(){
    return turnRemained;
  }
  public int BonusSectorCount(){
    return bonusSectorCount;
  }
  public int FailSectorCount(){
    return failSectorCount;
  }

  public void SetPlayerName(string customName){
    playerName = customName;
  }
  public void IncreaseTurnCount(){
    turnCount++;
  }
  public void SetTurnRemained(int value){
    turnRemained = value;
  }
  public void IncreaseTurnRemained(){
    turnRemained++;
  }
  public void DecreaseTurnRemained(){
    turnRemained--;
  }
  public void IncreaseBonusSectorCount(){
    bonusSectorCount++;
  }
  public void IncreaseFailSectorCount(){
    failSectorCount++;
  }
}
