using UnityEngine;

public class TotemActions : MonoBehaviour
{
    public EnemyStats totem;
    public Rigidbody2D body;
    public SpriteRenderer sprite;
    private float timer;
    public float projectileCount;

    [SerializeField] private Transform TotemProjectile;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > totem.attackSpeed)
        {
            Instantiate(TotemProjectile);
        }
    }

    void Initialize()
    {
        body = GetComponent<Rigidbody2D>();
        totem = GetComponent<EnemyStats>();
        sprite = GetComponent<SpriteRenderer>();
    }
}
