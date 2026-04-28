using System.Numerics;
using System.Threading;
using UnityEngine;

public class RightHandMove : MonoBehaviour
{
    public Rigidbody2D handBodyR;
    public SpriteRenderer handSpriteR;
    public StatueBossStats handStatsR;
    private Transform target;
    private Transform mainBody;
    private UnityEngine.Vector3 rightHandPosition;
    private UnityEngine.Vector3 slamPosition;
    private bool lockedOn = false;
    private bool slamming = false;
    private float timer = 0f;

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 5f)
        {
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
        Debug.Log("body: " + mainBody.position);
        rightHandPosition = mainBody.position + new UnityEngine.Vector3(-handStatsR.offset, 0, 0); 
        transform.position = UnityEngine.Vector3.MoveTowards(transform.position, rightHandPosition, handStatsR.moveSpeed * Time.deltaTime);
        //bodyDistance = Vector2.Distance(transform.position, mainBody.position);
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
            //Debug.Log("Target: " + target.position);
            slamPosition = target.position;
            lockedOn = true;
        }
        else
        {
            if (UnityEngine.Vector2.Distance(transform.position, slamPosition + new UnityEngine.Vector3(0, handStatsR.slamHeight, 0)) <= handStatsR.tolerance || slamming == true)
            {
                slamming = true;
                //Debug.Log("Slamming down!");
                transform.position = UnityEngine.Vector2.MoveTowards(transform.position, slamPosition, handStatsR.moveSpeed * Time.deltaTime);
                if (transform.position == slamPosition)
                {
                    //Debug.Log("Slammed!");
                    lockedOn = false;
                    slamming = false;
                    timer = 0f;
                    handStatsR.moveSpeed = handStatsR.returnMoveSpeed;
                }
            }
            else {
                //Debug.Log("Moving to slam position!");
                transform.position = UnityEngine.Vector2.MoveTowards(transform.position, slamPosition + new UnityEngine.Vector3(0, handStatsR.slamHeight, 0), handStatsR.moveSpeed * Time.deltaTime);
                //Debug.Log("Right hand position: " + transform.position + " Slam position: " + slamPosition);
            }
        }
    }

    void Shield()
    {
        //Hand will cover main body blocking incoming attacks, right hand has higher defense than left hand
    }

    void Initialize()
    {
        handBodyR = GetComponent<Rigidbody2D>();
        handSpriteR = GetComponent<SpriteRenderer>();
        handStatsR = GetComponent<StatueBossStats>();
        mainBody = GameObject.FindGameObjectWithTag("CorruptedStatueBody").transform;  
        target = GameObject.FindGameObjectWithTag("Player").transform;
        handStatsR.moveSpeed = handStatsR.initialMoveSpeed;
    }
}
