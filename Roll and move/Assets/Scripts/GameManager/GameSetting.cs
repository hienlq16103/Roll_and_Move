using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting : MonoBehaviour {
  public static GameSetting Instance;

  public int playerCount = 2;
  public int maxPlayer = 4;
  public List<string> playerNames;

  private void Awake() {
    if (Instance != null){
      Destroy(gameObject);
      return;
    }
    Instance = this;
    DontDestroyOnLoad(gameObject);
    playerNames = new List<string>(maxPlayer);
    for (int i = 0; i < maxPlayer; i++) {
      playerNames.Add("Player " + (i + 1).ToString());
    }
  }
  public void ResetSetting(){
    playerCount = 2;
    for (int i = 0; i < maxPlayer; i++) {
      playerNames[i] = "Player " + (i + 1).ToString();
    }
  }
}
