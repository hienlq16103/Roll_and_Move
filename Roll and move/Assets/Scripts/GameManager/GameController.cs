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

  [SerializeField] GameObject player;

  private LinkedList<PlayerReference> turnList;
  private LinkedList<PlayerData> finishedList;

  private GameObject playerClone;
  private PlayerData cloneData;
  private PlayerControl cloneControl;

  private void Start() {
    SetUpGame();
  }

  private void SetUpGame(){
    if (GameSetting.Instance == null){
      return;
    }
    for (int i = 0; i < GameSetting.Instance.playerCount; i++) {
      playerClone = Instantiate(player);
      cloneData = playerClone.GetComponent<PlayerData>();
      cloneControl = playerClone.GetComponent<PlayerControl>();
      cloneData.SetPlayerName(GameSetting.Instance.playerNames[i]);
      turnList.AddLast(new PlayerReference(cloneData, cloneControl));
    }
  }
}
