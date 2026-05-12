using UnityEngine;

public class ProjectileStats : MonoBehaviour
{
    public GameObject user;
    public float speed;
    public float damage;

    void Start()
    {
        if (user.CompareTag("Player"))
        {
            user = GameObject.FindGameObjectWithTag("Player");
            damage = user.GetComponent<PlayerStats>().Damage;
        }
        else if (user.CompareTag("Enemy"))
        {
            user = GameObject.FindGameObjectWithTag("Enemy");
            Debug.Log("Projectile user: " + user.name);
            damage = user.GetComponent<EnemyStats>().Damage;
        }
        else
        {
            Debug.LogError("Projectile user does not have a valid tag.");
            damage = 0;
        }
    }
}
