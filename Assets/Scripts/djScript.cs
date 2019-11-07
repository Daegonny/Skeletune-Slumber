using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class djScript : MonoBehaviour{
   
    private CircleCollider2D cc2d;
    public int lives = 3;
    void Start()
    {
        cc2d = GetComponent<CircleCollider2D>();
    }

    void Update() {

    }

    private void FixedUpdate()
    {
        
    }
}
