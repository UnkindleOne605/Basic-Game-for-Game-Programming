using UnityEngine;

public class StatueBodyMovement : MonoBehaviour
{
    public Rigidbody2D statueBody;
    public SpriteRenderer statueSprite;
    public StatueBossStats enemyStats;
    private float distanceToPlayer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        statueBody = GetComponent<Rigidbody2D>();
        statueSprite = GetComponent<SpriteRenderer>();
        enemyStats = GetComponent<StatueBossStats>();
    }

    // Update is called once per frame
    void Update()
    {
        UnityEngine.Vector3 targetPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        UnityEngine.Vector3 direction = (targetPosition - transform.position).normalized;
        distanceToPlayer = Vector3.Distance(transform.position, targetPosition);
        
        if (distanceToPlayer < (enemyStats.combatDistance - 0.25f))
        {
            enemyStats.tempMoveSpeed = enemyStats.moveSpeed;
            transform.position -= (direction * Time.deltaTime) * enemyStats.tempMoveSpeed;
        }
        else if (distanceToPlayer > (enemyStats.combatDistance + 0.25f))
        {
            enemyStats.tempMoveSpeed = enemyStats.moveSpeed;
            transform.position += (direction * Time.deltaTime) * enemyStats.tempMoveSpeed;
        }
        else if (distanceToPlayer >= (enemyStats.combatDistance - 0.25f) && distanceToPlayer <= (enemyStats.combatDistance + 0.25f))
        {
            enemyStats.tempMoveSpeed = 0f;
        }
    }
}
