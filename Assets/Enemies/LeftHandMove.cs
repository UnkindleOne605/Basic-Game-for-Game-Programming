using UnityEngine;

public class LeftHandMove : MonoBehaviour
{
    public Rigidbody2D handBodyL;
    public SpriteRenderer handSpriteL;
    public HandStats handStatsL;
    public float tolerance = 0.15f;
    public float offset = 1.5f;
    private float bodyDistance;
    private float difference;
    private Transform target;
    private Transform mainBody;
    private Vector3 leftHandPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        handBodyL = GetComponent<Rigidbody2D>();
        handSpriteL = GetComponent<SpriteRenderer>();
        handStatsL = GetComponent<HandStats>();
        mainBody = GameObject.FindGameObjectWithTag("CorruptedStatueBody").transform;  
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        FollowBody();
    }

    void FollowBody()
    {
        //Debug.Log("body: " + mainBody.position);
        leftHandPosition = mainBody.position + new UnityEngine.Vector3(offset, 0, 0); 
        transform.position = UnityEngine.Vector2.MoveTowards(transform.position, leftHandPosition, handStatsL.moveSpeed * Time.deltaTime);
        //bodyDistance = Vector2.Distance(transform.position, mainBody.position);
        if (UnityEngine.Vector2.Distance(transform.position, leftHandPosition) <= tolerance)
        {
            handStatsL.moveSpeed = handStatsL.initialMoveSpeed;
        }
    }

    void Heal()
    {
        //will periodically heal statue under certain conditions
    }

    void Punch()
    {
        //Hand will attempt to punch player
    }
}
