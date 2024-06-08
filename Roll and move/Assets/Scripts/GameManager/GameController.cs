using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
  class PlayerReference{
    public PlayerData playerData;
    public PlayerControl playerControl;
    
    public PlayerReference(PlayerData data, PlayerControl control){
      playerData = data;
      playerControl = control;
    }
  }

  [SerializeField] GameObject[] playerPrefabs = new GameObject[4];
  [SerializeField] Sector[] sectorList;

  private LinkedList<PlayerReference> turnList = new LinkedList<PlayerReference>();
  private LinkedList<PlayerData> finishedList = new LinkedList<PlayerData>();

  private void Start() {
    SetUpGame();
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
}
