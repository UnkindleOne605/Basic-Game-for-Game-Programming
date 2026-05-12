using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class CandleDroneActions : MonoBehaviour
{
    public Rigidbody2D body;
    public EnemyStats drone;
    public SpriteRenderer sprite;

    public float timer;
    public float fireTimer;
    public float reloadTime;
    public float reloadSpeed;
    public float maxAmmo;
    public float fireRate;
    private float currentAmmo;
    private float distanceToPlayer;
    private float tempMoveSpeed;
    public bool reloading = false;
    [SerializeField] private Transform DroneProjectile;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        fireTimer += Time.deltaTime;
        
        UnityEngine.Vector3 targetPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        ChaseTarget(targetPosition);

        DroneFire();

    }

    void ChaseTarget(UnityEngine.Vector3 targetPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, drone.moveSpeed * Time.deltaTime);
        UnityEngine.Vector3 direction = (targetPosition - transform.position).normalized;
        distanceToPlayer = Vector3.Distance(transform.position, targetPosition);
        
        if (distanceToPlayer < (drone.attackRange - 0.25f))
        {
            tempMoveSpeed = drone.moveSpeed;
            transform.position -= direction * Time.deltaTime * tempMoveSpeed;
        }
        else if (distanceToPlayer > (drone.attackRange + 0.25f))
        {
            tempMoveSpeed = drone.moveSpeed;
            transform.position += direction * Time.deltaTime * tempMoveSpeed;
        }
        else if (distanceToPlayer >= (drone.attackRange - 0.25f) && distanceToPlayer <= (drone.attackRange + 0.25f))
        {
            tempMoveSpeed = 0f;
        }
    }

    void DroneFire()
    {
        if (reloading == true)
        {
            timer += Time.deltaTime;
            
            currentAmmo += reloadSpeed;
            
            if (currentAmmo >= maxAmmo)
            {
                currentAmmo = maxAmmo;

                if (timer >= reloadTime)
                {
                    reloading = false;
                    timer = 0;    
                }
                
            }
        }
        else if (currentAmmo <= 0)
        {
            reloading = true;
        }
        else
        {
            if (fireTimer < fireRate)
            {
                return;
            }
            UnityEngine.Vector3 direction = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
            direction.z = 0;
            direction.Normalize();
            Transform projectileTransform = Instantiate(DroneProjectile, transform.position, UnityEngine.Quaternion.identity);
            projectileTransform.GetComponent<DroneProjectile>().Setup(direction);
            currentAmmo -= 1;
        }
    }

    void Initialize() {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        drone = GetComponent<EnemyStats>();
        tempMoveSpeed = drone.moveSpeed;
    }
}
