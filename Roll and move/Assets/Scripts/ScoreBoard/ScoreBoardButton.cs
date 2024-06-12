using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreBoardButton : MonoBehaviour {
  public void GoToMenu(){
    StartCoroutine(LoadScene());
  }

  private IEnumerator LoadScene(){
    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(0);
    while(!asyncLoad.isDone){
      yield return null;
    }
  }

  public void ReloadScene(){
    StartCoroutine(ReloadAsync());
  }
  private IEnumerator ReloadAsync(){
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(currentSceneIndex);
    while(!asyncLoad.isDone){
      yield return null;
    }
  }
}
