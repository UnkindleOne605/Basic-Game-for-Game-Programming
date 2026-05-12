using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

public class SlimeActions : MonoBehaviour
{
    public Rigidbody2D body;
    public EnemyStats slime;
    public SpriteRenderer sprite;
    public GameObject slimeObject;
    public PlayerStats player;
    public float doubleTime;
    private float timer;

    void Awake()
    {
        Initialize();
    }

    void Update()
    {
        timer += Time.deltaTime;

        UnityEngine.Vector3 targetPosition = player.transform.position;
        ChaseTarget(targetPosition);

        if (timer >= doubleTime)
        {
            timer = 0;
            Duplicate();
        }
    }

    public void ChaseTarget(UnityEngine.Vector3 targetPosition)
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, slime.moveSpeed * Time.deltaTime);
    }

    void Initialize()
    {
        body = GetComponent<Rigidbody2D>();
        slime = GetComponent<EnemyStats>();
        sprite = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    void Duplicate()
    {
        Instantiate(slimeObject);
    } 
}
