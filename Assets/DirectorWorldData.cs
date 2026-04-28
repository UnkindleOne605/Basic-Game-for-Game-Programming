using UnityEngine;

public class DirectorWorldData : MonoBehaviour
{
    public float worldTimer;
    public float minutes;
    public float seconds;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        worldTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        worldTimer += Time.deltaTime;

        minutes = Mathf.Floor(worldTimer / 60);
        seconds = Mathf.Floor(worldTimer - (minutes * 60));
        //Debug.Log("World Timer: " + minutes + ":" + seconds);
    }
}
