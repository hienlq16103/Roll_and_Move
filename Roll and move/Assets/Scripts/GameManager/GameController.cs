using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
  class PlayerReference{
    public PlayerData data;
    public PlayerControl control;
    
    public PlayerReference(PlayerData data, PlayerControl control){
      this.data = data;
      this.control = control;
    }
  }
  
  [SerializeField] int backwardStep = 3;
  [SerializeField] int bonus = 1;
  [SerializeField] float delayTime = 0.5f;
  [SerializeField] float movementDelay = 0.1f;

  [SerializeField] Dice dice;
  [SerializeField] GameObject[] playerPrefabs = new GameObject[4];
  [SerializeField] Sector[] sectorList;

  private LinkedList<PlayerReference> turnList = new LinkedList<PlayerReference>();
  private LinkedList<PlayerReference> finishedList = new LinkedList<PlayerReference>();

  private LinkedListNode<PlayerReference> turnVisitor;
  private bool removingNode = false;

  private void Start() {
    SetUpGame();
    StartCoroutine(GamePlayLoop());
  }

  private void SetUpGame(){
    if (GameSetting.Instance == null){
      return;
    }
    for (int i = 0; i < GameSetting.Instance.playerCount; i++) {
      GameObject playerClone = Instantiate(playerPrefabs[i],
          sectorList[0].standPoints[i].position,
          sectorList[0].standPoints[i].rotation);
      PlayerData cloneData = playerClone.GetComponent<PlayerData>();
      PlayerControl cloneControl = playerClone.GetComponent<PlayerControl>();
      cloneData.SetPlayerName(GameSetting.Instance.playerNames[i]);
      cloneData.SetIndex(i);
      turnList.AddLast(new PlayerReference(cloneData, cloneControl));
    }
  }
  private IEnumerator GamePlayLoop(){
    turnVisitor = turnList.First;

    while(turnList.Count > 0){
      turnVisitor.Value.data.SetTurnRemained(1);

      while(turnVisitor.Value.data.TurnRemained() > 0){
        yield return new WaitUntil(() => dice.IsRolledThisTurn);
        yield return new WaitForSeconds(delayTime);

        turnVisitor.Value.data.DecreaseTurnRemained();
        turnVisitor.Value.data.IncreaseTurnCount();

        yield return StartCoroutine(MoveForward(turnVisitor, dice.DiceValue()));

        dice.IsRolledThisTurn = false;
      }

      if (removingNode){
        if (turnList.Count == 1){
          turnList.Remove(turnVisitor);
        } else {
          LinkedListNode<PlayerReference> targetNode = turnVisitor;
          if (turnVisitor.Next == null){
            turnVisitor = turnList.First;
          } else {
            turnVisitor = turnVisitor.Next;
          }
          turnList.Remove(targetNode);
        }
        removingNode = false;
      } else {
        if (turnList.Count <= 1){
          continue;
        }
        if (turnVisitor.Next == null){
          turnVisitor = turnList.First;
        } else {
          turnVisitor = turnVisitor.Next;
        }
      }
    }

    PrintResult(finishedList.First);
  }
  private IEnumerator MoveForward(LinkedListNode<PlayerReference> node, int step){
    for (int i = 0; i < step; i++){
      if (node.Value.data.CurrentSector() == sectorList.Length - 1){
        break;
      }
      node.Value.data.IncreaseCurrentSector();
      yield return StartCoroutine(node.Value.control.Move(
            sectorList[node.Value.data.CurrentSector()].standPoints[node.Value.data.Index()]
            ));
      yield return new WaitForSeconds(movementDelay);
    }
    switch (sectorList[node.Value.data.CurrentSector()].sectorType){
      case SectorType.bonus:
        node.Value.data.IncreaseBonusSectorCount();
        node.Value.data.ChangeTurnRemainedBy(bonus);
        break;
      case SectorType.fail:
        node.Value.data.IncreaseFailSectorCount();
        yield return StartCoroutine(MoveBackward(node, backwardStep));
        break;
      case SectorType.finish:
        finishedList.AddLast(node.Value);
        removingNode = true;
        break;
      case SectorType.normal:
        break;
    }
  }
  private IEnumerator MoveBackward(LinkedListNode<PlayerReference> node, int step){
    for (int i = 0; i < step; i++){
      if (node.Value.data.CurrentSector() == 0){
        break;
      }
      node.Value.data.ChangeCurrentSectorBy(-1);
      yield return StartCoroutine(node.Value.control.Move(
            sectorList[node.Value.data.CurrentSector()].standPoints[node.Value.data.Index()]
            ));
      yield return new WaitForSeconds(movementDelay);
    }
    switch (sectorList[node.Value.data.CurrentSector()].sectorType){
      case SectorType.bonus:
        node.Value.data.IncreaseBonusSectorCount();
        node.Value.data.ChangeTurnRemainedBy(bonus);
        break;
      case SectorType.fail:
        node.Value.data.IncreaseFailSectorCount();
        yield return StartCoroutine(MoveBackward(node, backwardStep));
        break;
      case SectorType.normal:
        break;
    }
  }
  private void PrintResult(LinkedListNode<PlayerReference> node){
    if (node == null){
      return;
    }
    node.Value.data.Print();
    PrintResult(node.Next);
  }
}
