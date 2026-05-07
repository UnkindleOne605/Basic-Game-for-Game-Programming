using System.Numerics;
using System.Threading;
using System.Timers;
using UnityEngine;

public class RightHandMove : MonoBehaviour
{
    public Rigidbody2D handBodyR;
    public SpriteRenderer handSpriteR;
    public StatueBossStats handStatsR;
    public StatueBossStats statueBody;
    public PlayerStats player;
    private Transform target;
    private Transform mainBody;
    private UnityEngine.Vector3 rightHandPosition;
    private UnityEngine.Vector3 slamPosition;
    private bool lockedOn = false;
    private bool slamming = false;
    private bool shielding = false;
    private float timer = 0f;
    private float slamTimer = 0f;
    private float shieldTimer = 0f;
    public float slamCooldown;
    public float shieldCooldown;
    public float shieldDuration;

    void Awake()
    {
        Initialize();
    }

    void Update()
    {
        Timers();

        if (slamTimer >= slamCooldown || slamming == true)
        {
            Debug.Log("Slamming");
            Slam();
        }
        else
        {
            FollowBody();
        }
    }

    //Follows the main body with a slight offset
    void FollowBody()
    {
        //Debug.Log("Following: " + mainBody.position);
        rightHandPosition = mainBody.position + new UnityEngine.Vector3(-handStatsR.offset, 0, 0); 
        transform.position = UnityEngine.Vector3.MoveTowards(transform.position, rightHandPosition, handStatsR.moveSpeed * Time.deltaTime);
        if (UnityEngine.Vector3.Distance(transform.position, rightHandPosition) <= handStatsR.tolerance)
        {
            handStatsR.moveSpeed = handStatsR.initialMoveSpeed;
        }
    }

    //Hand flies above player's position and slams down before returning to original position
    void Slam()
    {
        if (lockedOn == false)
        {
            slamPosition = target.position;
            lockedOn = true;
        }
        else if (lockedOn == true)
        {
            //if hand in correct position start the slam
            if (UnityEngine.Vector2.Distance(transform.position, slamPosition + new UnityEngine.Vector3(0, handStatsR.slamHeight, 0)) <= handStatsR.tolerance)
            {
                handStatsR.Damage = handStatsR.modifiedDamage;
                lockedOn = false;
                slamming = true;
            }
            //else if hand not in position get to position
            else {
                transform.position = UnityEngine.Vector2.MoveTowards(transform.position, slamPosition + new UnityEngine.Vector3(0, handStatsR.slamHeight, 0), handStatsR.moveSpeed * Time.deltaTime);
            }
        }
        else if (slamming == true)
        {
            transform.position = UnityEngine.Vector2.MoveTowards(transform.position, slamPosition, handStatsR.moveSpeed * Time.deltaTime);

            if (transform.position == slamPosition)
                {
                    handStatsR.Damage = handStatsR.attackDamage;
                    slamTimer = 0f;
                    handStatsR.moveSpeed = handStatsR.returnMoveSpeed;
                    slamming = false;
                    lockedOn = false;
                }
        }
    }

    void Shield()
    {
        //Animation here
        if (shielding == false)
        {
            statueBody.Armor -= handStatsR.Armor;     
        }
        if (shielding == true)
        {
            statueBody.Armor += handStatsR.Armor;
        }
        
    }

    void Initialize()
    {
        handBodyR = GetComponent<Rigidbody2D>();
        handSpriteR = GetComponent<SpriteRenderer>();
        handStatsR = GetComponent<StatueBossStats>();
        mainBody = GameObject.FindGameObjectWithTag("CorruptedStatueBody").transform;  
        target = GameObject.FindGameObjectWithTag("Player").transform;
        handStatsR.moveSpeed = handStatsR.initialMoveSpeed;
        statueBody = GameObject.FindGameObjectWithTag("CorruptedStatueBody").GetComponent<StatueBossStats>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (timer > player.invincibilityDuration)
            {
                player.TakeDamage(CombatCalculation.CalculateDamage(handStatsR, player));
                timer = 0f;
            }
        }
    }

    void Timers()
    {
        timer += Time.deltaTime;
        slamTimer += Time.deltaTime;
        shieldTimer += Time.deltaTime;
    }

}
