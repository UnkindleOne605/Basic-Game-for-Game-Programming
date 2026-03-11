using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public SpriteRenderer sprite;

    public PlayerStats playerStats;

    public InputAction playerMove;

    Vector2 moveDirection = Vector2.zero;

    private void OnEnable()
    {
        playerMove.Enable();
    }

    private void OnDisable()
    {
        playerMove.Disable();
    }

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        moveDirection = playerMove.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        myRigidbody.linearVelocity = new Vector2(moveDirection.x * playerStats.moveSpeed, moveDirection.y * playerStats.moveSpeed);

        if (moveDirection.x > 0)
        {
            sprite.flipX = false;
        }
        else if (moveDirection.x < 0)
        {
            sprite.flipX = true;
        }
    }
}