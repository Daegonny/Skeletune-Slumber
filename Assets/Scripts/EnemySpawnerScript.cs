using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    public GameObject enemyGreen, enemyPurple, enemyOrange, enemyRed ;//, enemy2;
    float randX;
    float randY;
    public float boundLeft;
    public float boundRight;
    public float boundTop;
    public float boundBottom;
    Vector2 whereToSpawn;
    public float spawnRate;
    float nextSpawn = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private GameObject determineEnemyType(){
        float r = Random.Range(0.0f, 1.0f);
        if (r <= 0.25){
            return enemyGreen;
        }
        else if(r <= 0.50){
            return enemyPurple;
        }
        else if(r <= 0.75){
            return enemyOrange;
        }
        else{
            return enemyRed;
        }
    }

    // Update is called once per frame
    void Update() {
        if(Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            randX = Random.Range(boundLeft, boundRight);
            randY = Random.Range(boundBottom, boundTop);
            
            //while (randX >= -30 && randX <= 30 && randY >= -30 && randY <= 30)
            //{
            //    randX = Random.Range(-66.6f, 66.65f);
            //    randY = Random.Range(-66.6f, 66.6f);
            //}
            //TODO: DONT LET ENEMY SPAWN INSID RADIUS
            whereToSpawn = new Vector2(randX, randY);
            Instantiate(determineEnemyType(), whereToSpawn, Quaternion.identity);            
        }
    }
}
