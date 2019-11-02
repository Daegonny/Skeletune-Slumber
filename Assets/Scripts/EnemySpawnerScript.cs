using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    public GameObject enemy, enemy2;
    float randX;
    float randY;
    Vector2 whereToSpawn;
    public float spawnRate = 5f;
    float nextSpawn = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        if(Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            randX = Random.Range(-100f, 100.5f);
            randY = Random.Range(-80f, 80f);
            whereToSpawn = new Vector2(randX, randY);
            Instantiate(enemy, whereToSpawn, Quaternion.identity);
            whereToSpawn = new Vector2(randX, randY);
            Instantiate(enemy2, whereToSpawn, Quaternion.identity);
        }
    }
}
