using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {

  private string playerName = "Player 0";
  private int turnCount = 0;
  private int turnRemained = 1;
  private int bonusSectorCount = 0;
  private int failSectorCount = 0;
  private int currentSector = 0;
  private int index = 0;

  public void Print(){
    Debug.Log("Player index: " + index + "\n"
    + "Player name: " + playerName + "\n"
    + "Current sector: " + currentSector + "\n"
    + "Turn count: " + turnCount + "\n"
    + "Turn remained: " + turnRemained + "\n"
    + "Bonus: " + bonusSectorCount + "\n"
    + "Fail: " + failSectorCount);
  }

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
  public int CurrentSector(){
    return currentSector;
  }
  public int Index(){
    return index;
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
  public void ChangeTurnRemainedBy(int value){
    turnRemained += value;
  }
  public void IncreaseBonusSectorCount(){
    bonusSectorCount++;
  }
  public void IncreaseFailSectorCount(){
    failSectorCount++;
  }
  public void IncreaseCurrentSector(){
    currentSector++;
  }
  public void ChangeCurrentSectorBy(int value){
    currentSector += value;
  }
  public void SetIndex(int index){
    this.index = index;
  }
}
