using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveSscript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    private djScript playerScript;
    public Sprite one;
    public Sprite two;
    public Sprite three;
    private SpriteRenderer sr;
    void Start()
    {
        playerScript = player.GetComponent<djScript>();
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.lives == 3){
            sr.sprite = three;
        }
        else if(playerScript.lives == 2){
            sr.sprite = two;
        }
        else{
            sr.sprite = one;
        }
    }
}
