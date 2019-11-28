using UnityEngine;

public class EnemiesScript : MonoBehaviour
{
    Rigidbody2D rb;
    Transform target;
    private Vector3 velocity;
    public float moveSpeed;
    public Animator anim;
    int state = 0;
    //public GameObject explosion;


    // Start is called before the first frame update
    void Start() {
        target = GameObject.Find("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        velocity = Vector3.zero;
        //moveSpeed = Random.Range(6f, 10f);
        moveSpeed = 3;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { // COLOCAR AQUI AS PARADAS DE COLISÂO
        switch(collision.gameObject.name)
        {
            case "SecondRadius":
                FindObjectOfType<AudioManager>().Play("GreenColision1");
                break;
            case "FirstRadius":
                break;

        }
    }

    // Update is called once per frame
    void FixedUpdate() {
        MoveEnemy();
    }

    void Update(){
        checkStatus();
        anim.SetInteger("EnemyState", state);
    }

    private void MoveEnemy()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        
        //pegar speed do rb para fazer as animações

        velocity = target.position - transform.position;
    }

    void checkStatus(){
        //Debug.Log(velocity.x+" - "+velocity.y);
        if (Mathf.Abs(velocity.x) >= Mathf.Abs(velocity.y)){
            if (velocity.x > 0){
                state = 0;
            }
            else{
                state = 1;
            }
        }
        else{
            if (velocity.y > 0){
                state = 2;
            }
            else{
                state = 3;
            }
        }
    }
}
