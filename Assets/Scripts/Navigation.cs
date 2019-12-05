using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Navigation : MonoBehaviour
{
    private Button exitToMainMenuButton;

    // Start is called before the first frame update
    void Start()
    {
        exitToMainMenuButton = GameObject.Find("ExitToMainMenuButton").GetComponent<Button>();
        exitToMainMenuButton.onClick.AddListener(ExitToMainMenu);
        //Debug.Log(exitToMainMenuButton);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ExitToMainMenu() {
        SceneManager.LoadSceneAsync("Menu");
    }
}
