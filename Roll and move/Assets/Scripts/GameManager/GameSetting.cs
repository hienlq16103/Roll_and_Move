using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting : MonoBehaviour {
  public static GameSetting Instance;

  public int playerCount;
  public string[] playerNames;

  private void Awake() {
    if (Instance != null){
      Destroy(gameObject);
      return;
    }
    Instance = this;
    DontDestroyOnLoad(gameObject);
  }
}
