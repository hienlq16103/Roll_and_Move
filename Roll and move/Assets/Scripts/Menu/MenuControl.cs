using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuControl : MonoBehaviour {
  public static MenuControl Instance;

  [SerializeField] GameObject panel;
  [SerializeField] GameObject extraEntry;
  [SerializeField] GameObject addButton;

  private void Awake() {
    if (Instance != null){
      Destroy(gameObject);
      return;
    }
    Instance = this;
  }

  public void AddEntry(){
    GameSetting.Instance.playerCount++;
    GameObject entryClone = Instantiate(extraEntry, panel.transform);
    entryClone.GetComponentInChildren<TMP_Text>().text = "Player " + GameSetting.Instance.playerCount.ToString();
    entryClone.GetComponent<ExtraEntry>().entryIndex = GameSetting.Instance.playerCount;
    if (GameSetting.Instance.playerCount < GameSetting.Instance.maxPlayer){
      Instantiate(addButton, panel.transform);
    }
  }
  public void RemoveEntry(){
    GameSetting.Instance.playerCount--;
    if (GameSetting.Instance.playerCount == GameSetting.Instance.maxPlayer - 1){
      Instantiate(addButton, panel.transform);
    }
  }
}
