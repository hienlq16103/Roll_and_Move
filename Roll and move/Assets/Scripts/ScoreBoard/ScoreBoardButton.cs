using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class ScoreBoardButton : MonoBehaviour {
  [SerializeField] AudioSource audioSource;
  [SerializeField] AudioClip buttonEnterClip;
  [SerializeField] AudioClip buttonClickClip;

  public void GoToMenu(){
    if (GameSetting.Instance != null){
      audioSource.PlayOneShot(buttonClickClip);
    }
      GameSetting.Instance.ResetSetting();
      StartCoroutine(LoadScene());
  }

  private IEnumerator LoadScene(){
    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(0);
    while(!asyncLoad.isDone){
      yield return null;
    }
  }

  public void ReloadScene(){
    audioSource.PlayOneShot(buttonClickClip);
    StartCoroutine(ReloadAsync());
  }
  private IEnumerator ReloadAsync(){
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(currentSceneIndex);
    while(!asyncLoad.isDone){
      yield return null;
    }
  }

  public void EnterButton(){
    audioSource.PlayOneShot(buttonEnterClip);
  }
}
