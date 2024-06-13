using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {
  [SerializeField] AudioSource audioSource;
  [SerializeField] AudioClip buttonEnterClip;
  [SerializeField] GameObject panel;

  public void MouseEnter(){
    audioSource.PlayOneShot(buttonEnterClip);
  }

  public void StartTheGame(){
    StartCoroutine(LoadSceneAsync());
  }
  IEnumerator LoadSceneAsync(){
    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    while (!asyncLoad.isDone){
      yield return null;
    }
  }
}
