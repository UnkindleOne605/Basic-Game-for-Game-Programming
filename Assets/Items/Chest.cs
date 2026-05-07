using UnityEngine;

public class Chest : MonoBehaviour
{
    public Rigidbody2D chest;
    public RandomItemSpawn spawn;
    public PlayerStats player;
    public float chestCost;
    // Update is called once per frame
    void Awake()
    {
        initialize();
    }
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided");
        if (collision.CompareTag("Player"))
        {
            if (player.goldAmount >= chestCost)
            {
                Debug.Log("Player Bought Chest");
                player.LoseGold(chestCost);
                
                spawn.SpawnObject();
                Debug.Log("Item Given");
            
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Can't Afford Chest");
            }
        }
        
    }

    void initialize()
    {
        chest = GetComponent<Rigidbody2D>();
        spawn = GetComponent<RandomItemSpawn>(); 
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }
}
