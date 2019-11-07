using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScript : MonoBehaviour
{
    public string key;
    public Sprite off;
    public Sprite on;
    private SpriteRenderer sr;
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        sr.sprite = off;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(key)) 
        {
        sr.sprite = on;
        }
        else{
            sr.sprite = off;
        }
    }
}
