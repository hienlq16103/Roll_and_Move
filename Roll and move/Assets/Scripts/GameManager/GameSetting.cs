using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting : MonoBehaviour {
  public static GameSetting Instance;

  public int playerCount = 4;
  public string[] playerNames = {"Player 1", "Player 2", "Player 3", "Player 4"};

  private void Awake() {
    if (Instance != null){
      Destroy(gameObject);
      return;
    }
    Instance = this;
    DontDestroyOnLoad(gameObject);
  }
}
