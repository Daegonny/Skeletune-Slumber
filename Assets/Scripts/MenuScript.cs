using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        Debug.Log("dkjfnsdkjfn");
    }

    public void goesToTutorialScene() {
         Debug.Log("goesToTutorial");
    }

    public void goesToAboutScene() {
         Debug.Log("goesToTutorial");
    }
}
