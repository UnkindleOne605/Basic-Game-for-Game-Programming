using UnityEngine;

public class RandomItemSpawn : MonoBehaviour
{
    public GameObject[] chestItems;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    public void SpawnObject()
    {
        if (chestItems == null || chestItems.Length == 0)
        {
            Debug.Log("No Items");
            return;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.Log("No Player");
            return;
        }

        UnityEngine.Vector3 spawnPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        
        int randomIndex = Random.Range(0, chestItems.Length);
        
        Debug.Log(chestItems[randomIndex]);
        Instantiate(chestItems[randomIndex], spawnPosition, Quaternion.identity);
    }

}
