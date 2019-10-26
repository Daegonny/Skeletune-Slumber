using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerHandler : MonoBehaviour
{
    public float timer;
    public bool[] pressed;
    private string[] melodia;
    public int notaAtual;

    float oldTimer;
    bool isRunning = true;

    public void Start()
    {
        timer = 6;
        oldTimer = timer;
        for (int i = 0; i < 4; i++)
        {
            melodia = new string[4];
            pressed = new bool[4];
            pressed[i] = false;
            notaAtual = 0;
            melodia[0] = "z";
            melodia[1] = "x";
            melodia[2] = "z";
            melodia[3] = "x";
        }
    }

    void preencheMelodia(string tecla)
    {
        if(pressed[notaAtual] == false && melodia[notaAtual] == tecla)
        {
            pressed[notaAtual] = true;
            notaAtual++;
            return;
        }
    }

    bool melodiaPreenchida()
    {
        for(int i = 0; i < 4; i++)
        {
            if(pressed[i] == false)
            {
                return false;
            }
        }
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            if (Input.GetKey("z"))
            {
                preencheMelodia("z");
                if (melodiaPreenchida())
                {
                    gameObject.GetComponent<EnemiesOnRadius>().remove();
                }
            } else if (Input.GetKey("x"))
            {
                preencheMelodia("x");
                if (melodiaPreenchida())
                {
                    gameObject.GetComponent<EnemiesOnRadius>().remove();
                }
            }

            timer -= Time.deltaTime;
            //GetComponent<Text>().text = "Tempo: " + Mathf.RoundToInt(timer).ToString() + " s";

            if (timer < 0)
            {
                isRunning = false;
                if (melodiaPreenchida())
                {
                    //Destroy(gameObject);
                }
            }
        }
    }
}
