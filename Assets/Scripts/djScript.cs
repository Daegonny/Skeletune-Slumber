using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class djScript : MonoBehaviour{
    public float rotationSpeed = 3.0f;
    public float speed;
    private Rigidbody2D rb2d;
    private Vector2 moveVelocity;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update() {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;
        transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime*rotationSpeed);
    }

    private void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + moveVelocity * Time.fixedDeltaTime);
    }
}
