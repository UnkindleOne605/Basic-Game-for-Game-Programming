using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Numerics;

public class PlayerAttacking : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public SpriteRenderer sprite;

    public InputAction playerAttack;

    public PlayerStats stats;
    public float timer;

    [SerializeField] private Transform BasicProjectile;

    private void OnEnable()
    {
        playerAttack.Enable();
    }

    private void OnDisable()
    {
        playerAttack.Disable();
    }

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (playerAttack.triggered && stats.attackCooldown <= timer)
        {
            timer = 0;
            //Attack();
            Debug.Log("Attack!");

            UnityEngine.Vector3 mouseScreenPosition = Mouse.current.position.ReadValue();
            mouseScreenPosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
            UnityEngine.Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
            UnityEngine.Vector3 direction = (mouseWorldPosition - transform.position);
            direction.z = 0;
            direction.Normalize();

            Transform projectileTransform = Instantiate(BasicProjectile, transform.position, UnityEngine.Quaternion.identity);
            projectileTransform.GetComponent<BasicProjectile>().Setup(direction);
        }
        else if (playerAttack.triggered && stats.attackCooldown > timer)
        {
            Debug.Log("On Cooldown");
        }
    }

    private void FixedUpdate()
    {
        
    }
}
