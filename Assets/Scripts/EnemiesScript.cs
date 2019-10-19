using UnityEngine;

public class EnemiesScript : MonoBehaviour
{
    Rigidbody2D rb;
    Transform target;
    float moveSpeed;
    Vector3 directionToTarget;
    //public GameObject explosion;


    // Start is called before the first frame update
    void Start() {
        target = GameObject.Find("UFO").GetComponent<Transform>();
        moveSpeed = Random.Range(2f, 10f);
        
    }

    // Update is called once per frame
    void Update() {
        MoveEnemy();
    }

    private void MoveEnemy()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }
}
