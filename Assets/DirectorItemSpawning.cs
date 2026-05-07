using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities.UniversalDelegates;

public class DirectorItemSpawning : MonoBehaviour
{
    public UnityEngine.Vector2 spawnPosition;
    public float distanceFromChest;
    public GameObject[] chests;
    public GameObject spawnChest;
    public float spawnAmount;
    public float spread;
    public float amountSpawned = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(ChestSpawning());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ChestSpawning()
    {
        while (true) {
            if (spawnAmount >= amountSpawned)
            {
                spawnPosition = new Vector2(Random.Range(-50, 50), Random.Range(-50, 50));
                chests = GameObject.FindGameObjectsWithTag("Chest");

                foreach (GameObject chest in chests)
                {
                    distanceFromChest = Vector2.Distance(transform.position, chest.transform.position);
                }

                if (spread <= distanceFromChest)
                {
                    Instantiate(spawnChest, spawnPosition, UnityEngine.Quaternion.identity);
                }

                amountSpawned--;
                Debug.Log("Spawned Amount: " + (amountSpawned));
            }
            yield return new WaitForSeconds(5f);
        }
    }
}
