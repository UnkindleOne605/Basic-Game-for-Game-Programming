using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    public PlayerStats playerStats;

    private UnityEngine.Vector3 direction;
    public float speed = 10f;
    public float range = 5f;
    //public float damage = 10f;
    private UnityEngine.Vector3 startPosition;
    public void Setup(UnityEngine.Vector3 direction)
    {
        Debug.Log("Setting direction: " + direction);
        this.direction = direction;
        startPosition = transform.position;
    }

    private void Update()
    {
        //Debug.Log("Projectile direction: " + direction);
        //Debug.Log("Projectile position: " + transform.position);
        transform.position += direction * speed * Time.deltaTime;

        if (UnityEngine.Vector3.Distance(startPosition, transform.position) >= playerStats.modifiedAttackRange)
        {
            Destroy(gameObject);
        }
        //Destroy(gameObject, 5f);
    }
}

