using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    public PlayerStats playerStats;

    public ProjectileStats projectile;
    private UnityEngine.Vector3 direction;

    private UnityEngine.Vector3 startPosition;
    public void Setup(UnityEngine.Vector3 direction)
    {
        //Debug.Log("Setting direction: " + direction);
        this.direction = direction;
        startPosition = transform.position;
    }

    private void Update()
    {
        transform.position += direction * projectile.speed * Time.deltaTime;

        if (UnityEngine.Vector3.Distance(startPosition, transform.position) >= playerStats.modifiedAttackRange)
        {
            Destroy(gameObject);
        }
        //Destroy(gameObject, 5f);
    }
}

