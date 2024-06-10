using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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
  [SerializeField] CinemachineVirtualCamera cameraPrefab;
  [SerializeField] GameObject[] playerPrefabs = new GameObject[4];
  [SerializeField] Sector[] sectorList;

  private LinkedList<PlayerReference> turnList = new LinkedList<PlayerReference>();
  private LinkedList<PlayerReference> finishedList = new LinkedList<PlayerReference>();
  private LinkedList<CinemachineVirtualCamera> cameraList = new LinkedList<CinemachineVirtualCamera>();

  private LinkedListNode<PlayerReference> turnVisitor;
  private LinkedListNode<CinemachineVirtualCamera> activeCamera;
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

      CinemachineVirtualCamera camera = Instantiate(cameraPrefab,
          Vector3.zero,
          Quaternion.identity);
      camera.LookAt = playerClone.transform;
      camera.Follow = playerClone.transform;
      camera.enabled = false;
      cameraList.AddLast(camera);
    }
  }
  private IEnumerator GamePlayLoop(){
    turnVisitor = turnList.First;
    activeCamera = cameraList.First;
    activeCamera.Value.enabled = true;

    while(turnList.Count > 0){
      turnVisitor.Value.data.SetTurnRemained(1);

      while(turnVisitor.Value.data.TurnRemained() > 0){
        yield return new WaitUntil(() => dice.IsRolledThisTurn);
        yield return new WaitForSeconds(delayTime);

        turnVisitor.Value.data.DecreaseTurnRemained();
        turnVisitor.Value.data.IncreaseTurnCount();

        yield return StartCoroutine(MoveForward(turnVisitor, dice.DiceValue()));

        if (turnVisitor.Value.data.TurnRemained() > 0){
          dice.IsRolledThisTurn = false;
        }
      }


      if (removingNode){
        if (turnList.Count == 1){
          turnList.Remove(turnVisitor);
        } else {
          activeCamera.Value.enabled = false;
          LinkedListNode<PlayerReference> targetNode = turnVisitor;
          LinkedListNode<CinemachineVirtualCamera> removingCamera = activeCamera;
          if (turnVisitor.Next == null){
            turnVisitor = turnList.First;
            activeCamera = cameraList.First;
          } else {
            turnVisitor = turnVisitor.Next;
            activeCamera = activeCamera.Next;
          }
          turnList.Remove(targetNode);
          cameraList.Remove(removingCamera);
          activeCamera.Value.enabled = true;
          yield return new WaitForSeconds(delayTime);
          dice.IsRolledThisTurn = false;
        }
        removingNode = false;
      } else {
        if (turnList.Count <= 1){
          dice.IsRolledThisTurn = false;
        } else {
          activeCamera.Value.enabled = false;
          if (turnVisitor.Next == null){
            turnVisitor = turnList.First;
            activeCamera = cameraList.First;
          } else {
            turnVisitor = turnVisitor.Next;
            activeCamera = activeCamera.Next;
          }
          activeCamera.Value.enabled = true;
          yield return new WaitForSeconds(delayTime);
          dice.IsRolledThisTurn = false;
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
