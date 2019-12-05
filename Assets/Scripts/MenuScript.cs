using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void goesToGameScene() {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneIndex);
        Debug.Log("dkjfnsdkjfn");
        PlayerPrefs.SetInt ("highscore", 0);
    }

    public void goesToTutorialScene() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(6);
    }

    public void goesToAboutScene() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(7);
    }

    public void goesToMenuPrincipal() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    public void exitGame()
    {
        Application.Quit();
    }
}
