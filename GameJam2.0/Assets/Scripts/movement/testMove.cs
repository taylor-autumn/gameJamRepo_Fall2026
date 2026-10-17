using UnityEngine;

public class testMove : MonoBehaviour
{
    public float speed = 5;
    public Rigidbody2D rb;
    private Vector2 movementInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        movementInput.x = Input.GetAxisRaw("Horizontal");
        //movementInput.y = Input.GetAxisRaw("Vertical");


        movementInput = movementInput.normalized;
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = movementInput * speed;
    }

}
