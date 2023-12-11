using UnityEngine;
using Mirror;

public class PlayerMovement : NetworkBehaviour
{
    private float playerSpeed = 1f;

    private Rigidbody2D rb;
    private Vector2 input;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;

        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + input * playerSpeed * Time.fixedDeltaTime);
    }
}
