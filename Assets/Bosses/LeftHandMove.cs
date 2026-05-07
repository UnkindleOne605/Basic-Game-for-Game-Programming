using UnityEngine;

public class LeftHandMove : MonoBehaviour
{
    public Rigidbody2D handBodyL;
    public SpriteRenderer handSpriteL;
    public StatueBossStats handStatsL;
    public StatueBossStats statueBody;
    public float healMoveY;
    public float healCastTime;
    public float healAmount;
    private float bodyDistance;
    private float difference;
    private Transform target;
    private Transform mainBody;
    private Vector3 leftHandPosition;
    private bool healing = false;
    private bool healOnCooldown = false;

    private float healTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (healing == false)
        {
            FollowBody();   
        }
        else if ((statueBody.currentHealth <= statueBody.maxHealth/2 || healOnCooldown == false) && healing == true)
        {
            healing = true;
            Heal();
        }
        
    }

    void FollowBody()
    {
        //Debug.Log("body: " + mainBody.position);
        leftHandPosition = mainBody.position + new UnityEngine.Vector3(handStatsL.offset, 0, 0); 
        transform.position = UnityEngine.Vector3.MoveTowards(transform.position, leftHandPosition, handStatsL.moveSpeed * Time.deltaTime);
        //bodyDistance = Vector2.Distance(transform.position, mainBody.position);
        if (UnityEngine.Vector3.Distance(transform.position, leftHandPosition) <= handStatsL.tolerance)
        {
            handStatsL.moveSpeed = handStatsL.initialMoveSpeed;
        }
    }

    void Heal()
    {
        healTimer += Time.deltaTime;
        //will periodically heal statue under certain conditions
        leftHandPosition = mainBody.position + new UnityEngine.Vector3(handStatsL.offset, healMoveY, 0); 
        transform.position = UnityEngine.Vector3.MoveTowards(transform.position, leftHandPosition, handStatsL.moveSpeed * Time.deltaTime);
        //bodyDistance = Vector2.Distance(transform.position, mainBody.position);
        if (UnityEngine.Vector3.Distance(transform.position, leftHandPosition) <= handStatsL.tolerance)
        {
            handStatsL.moveSpeed = handStatsL.initialMoveSpeed;
        }

        if (healTimer >= healCastTime)
        {
            statueBody.currentHealth += healAmount;    
            healing = false;
        }

    }

    void Initialize()
    {
        handBodyL = GetComponent<Rigidbody2D>();
        handSpriteL = GetComponent<SpriteRenderer>();
        handStatsL = GetComponent<StatueBossStats>();
        mainBody = GameObject.FindGameObjectWithTag("CorruptedStatueBody").transform;  
        target = GameObject.FindGameObjectWithTag("Player").transform;
        statueBody = GameObject.FindGameObjectWithTag("CorruptedStatueBody").GetComponent<StatueBossStats>();
    }

}
