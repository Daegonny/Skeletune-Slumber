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
        moveSpeed = Random.Range(6f, 10f);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { // COLOCAR AQUI AS PARADAS DE COLISÂO
        switch(collision.gameObject.name)
        {
            case "SecondRadius":
                moveSpeed = 50f;
                break;
            case "FirstRadius":
                moveSpeed = 0f;
                break;

        }
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
