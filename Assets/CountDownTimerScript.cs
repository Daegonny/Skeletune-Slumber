using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountDownTimerScript : MonoBehaviour
{
    public Text uiText;
    public float mainTimer;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = mainTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer >= 0.0f) {
            timer -= Time.deltaTime;
            uiText.text = timer.ToString("F");
        } else {
            uiText.text = "0.0";
            SceneManager.LoadScene(2);
        }
    }
}
