using System.Numerics;
using System.Threading;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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
    private bool returning = false;
    private float statueBodyOriginalArmor;
    private float timer = 0f;
    private float slamTimer = 0f;
    private float shieldTimer = 0f;
    private float shieldDurationTimer = 0f;
    public float slamCooldown;
    public float shieldCooldown;
    public float shieldDuration;

    void Awake()
    {
        Initialize();
        statueBodyOriginalArmor = statueBody.armor;
    }

    //Basic logic and timers for how the code manages the cooldowns and actions
    void Update()
    {
        Timers();

        
        if (slamming == false && shielding == false && lockedOn == false && slamTimer >= slamCooldown || slamming == true || lockedOn == true || returning == true)
        {
            Slam();
        }
        else if (slamming == false && shielding == false && lockedOn == false && shieldTimer >= shieldCooldown || shielding == true)
        {
            Shield();
        }
        else if (slamming == false && shielding == false && lockedOn == false && returning == false)
        {
             FollowBody();
        }
    }

    //Follows the main body with a slight offset
    void FollowBody()
    {
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
        if (returning == true) 
        {
            transform.position = UnityEngine.Vector3.MoveTowards(transform.position, rightHandPosition, handStatsR.moveSpeed * Time.deltaTime);
            if (transform.position == rightHandPosition)
                {
                    handStatsR.moveSpeed = handStatsR.initialMoveSpeed;
                    returning = false;
                }
        }
        else if (lockedOn == false && slamming == false)
        {
            slamPosition = target.position;
            lockedOn = true;
        }
        else if (lockedOn == true && slamming == false)
        {
            //if hand in correct position start the slam
            if (UnityEngine.Vector2.Distance(transform.position, slamPosition + new UnityEngine.Vector3(0, handStatsR.slamHeight, 0)) <= handStatsR.tolerance)
            {
                handStatsR.Damage = handStatsR.modifiedDamage;
                lockedOn = false;
                slamming = true;
                slamPosition = slamPosition + new UnityEngine.Vector3(0, -handStatsR.slamHeight, 0);
            }
            //else if hand not in position get to position
            else {
                transform.position = UnityEngine.Vector2.MoveTowards(transform.position, slamPosition + new UnityEngine.Vector3(0, handStatsR.slamHeight, 0), handStatsR.moveSpeed * Time.deltaTime);
            }
        }
        else if (slamming == true && lockedOn == false)
        {
            transform.position = UnityEngine.Vector2.MoveTowards(transform.position, slamPosition, handStatsR.moveSpeed * Time.deltaTime);

            if (transform.position == slamPosition)
                {
                    Debug.Log("Damage" + handStatsR.Damage);
                    Debug.Log("Damage" + CombatCalculation.CalculateDamage(handStatsR, player));
                    slamTimer = 0f;
                    handStatsR.moveSpeed = handStatsR.returnMoveSpeed;
                    slamming = false;
                    lockedOn = false;
                    returning = true;
                }
        }
    }

    void Shield()
    {
        Debug.Log("Shielding");
        //Animation here
        shielding = true;
        handSpriteR.color = Color.blue;
        statueBody.armor = statueBodyOriginalArmor + handStatsR.armor;

        rightHandPosition = mainBody.position; 
        transform.position = UnityEngine.Vector3.MoveTowards(transform.position, rightHandPosition, handStatsR.moveSpeed * Time.deltaTime);
        if (UnityEngine.Vector3.Distance(transform.position, rightHandPosition) <= handStatsR.tolerance)
        {
            handStatsR.moveSpeed = handStatsR.initialMoveSpeed;
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
        
        if (shielding == true)
        {
            shieldDurationTimer += Time.deltaTime;

            if (shieldDurationTimer >= shieldDuration)
            {
                shielding = false;
                handSpriteR.color = Color.white;
                statueBody.armor = statueBodyOriginalArmor;
                shieldDurationTimer = 0f;
                shieldTimer = 0f;
            }
        }
    }

}
