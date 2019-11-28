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

    public GameObject[] spawnPoints;


    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
            
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
            return enemyRed;
        }
        else{
            return enemyOrange;
        }
    }

    // Update is called once per frame
    void Update() {
        if(Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            if (spawnPoints.Length > 0){
                GameObject randSpawn = spawnPoints[Random.Range(0, spawnPoints.Length -1)];
                whereToSpawn = new Vector2(randSpawn.GetComponent<Transform>().position.x, randSpawn.GetComponent<Transform>().position.y);
            }
            else{
                randX = Random.Range(boundLeft, boundRight);
                randY = Random.Range(boundBottom, boundTop);
            
                while (randX >= -66 && randX <= 66 && randY >= -66 && randY <= 66)
                {
                    randX = Random.Range(boundLeft, boundRight);
                    randY = Random.Range(boundBottom, boundTop);
                }
                //TODO: DONT LET ENEMY SPAWN INSID RADIUS
                whereToSpawn = new Vector2(randX, randY);
            }
            
            Instantiate(determineEnemyType(), whereToSpawn, Quaternion.identity);            
        }
    }
}
