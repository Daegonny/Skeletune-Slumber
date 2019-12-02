using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBarScript : MonoBehaviour
{
    public GameObject secondRadius;
    private EnemiesOnRadius script;
    public Sprite zero;
    public Sprite one;
    public Sprite two;
    public Sprite three;
    public Sprite four;
    public Sprite five;
    public Sprite six;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        script = secondRadius.GetComponent<EnemiesOnRadius>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(script.timer);
        if (script.timer > 2.5){
            sr.sprite = six;
        }
        else if (script.timer > 2){
            sr.sprite = five;
        }
        else if (script.timer > 1.5){
            sr.sprite = four;
        }
        else if (script.timer > 1){
            sr.sprite = three;
        }
        else if (script.timer > 0.5){
            sr.sprite = two;
        }
        else if (script.timer > 0){
            sr.sprite = one;
        }
        else{
            sr.sprite = zero;
        }
    }
}
