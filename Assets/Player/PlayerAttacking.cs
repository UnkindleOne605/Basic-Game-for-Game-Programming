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
    private UnityEngine.Vector2 BulletSpawnOffset = new UnityEngine.Vector2(0, -0.5f);
    private float timer;
    private float fireTimer;

    [SerializeField] private Transform BasicProjectile;

    private void OnEnable()
    {
        playerAttack.Enable();
    }

    private void OnDisable()
    {
        playerAttack.Disable();
    }

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (PauseMenu.isPaused) {
            return;
        }
        else {
            timer += Time.deltaTime;

            if (playerAttack.triggered && stats.modifiedFireRate <= timer)
            {
                timer = 0;
                //Attack();
                //Debug.Log("Attack!");

                UnityEngine.Vector3 mouseScreenPosition = Mouse.current.position.ReadValue();
                mouseScreenPosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
                UnityEngine.Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
                UnityEngine.Vector3 direction = (mouseWorldPosition - transform.position);
                direction.z = 0;
                direction.Normalize();

                UnityEngine.Vector2 bulletSpawnPosition = (UnityEngine.Vector2)transform.position + BulletSpawnOffset;

                Transform projectileTransform = Instantiate(BasicProjectile, bulletSpawnPosition, UnityEngine.Quaternion.identity);
                projectileTransform.GetComponent<BasicProjectile>().Setup(direction);
            }
            else if (playerAttack.triggered && stats.modifiedFireRate > timer)
            {
                Debug.Log("On Cooldown");
            }
        }
    }

    private void FixedUpdate()
    {
        
    }
}
