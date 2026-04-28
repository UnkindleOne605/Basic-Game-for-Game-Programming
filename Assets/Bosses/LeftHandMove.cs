using UnityEngine;

public class LeftHandMove : MonoBehaviour
{
    public Rigidbody2D handBodyL;
    public SpriteRenderer handSpriteL;
    public StatueBossStats handStatsL;
    private float bodyDistance;
    private float difference;
    private Transform target;
    private Transform mainBody;
    private Vector3 leftHandPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initialize();
        Debug.Log($"Player null? {target == null}");
        Debug.Log($"Enemy null? {mainBody == null}");
    }

    // Update is called once per frame
    void Update()
    {
        FollowBody();
    }

    void FollowBody()
    {
        Debug.Log("body: " + mainBody.position);
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
        //will periodically heal statue under certain conditions
    }

    void Punch()
    {
        //Hand will attempt to punch player
    }

    void Initialize()
    {
        handBodyL = GetComponent<Rigidbody2D>();
        handSpriteL = GetComponent<SpriteRenderer>();
        handStatsL = GetComponent<StatueBossStats>();
        mainBody = GameObject.FindGameObjectWithTag("CorruptedStatueBody").transform;  
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
