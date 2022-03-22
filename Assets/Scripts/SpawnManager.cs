using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    //spawn z 12
    //spawn between -17 and 17 x

    public GameObject[] enemyPrefab;
    private int enemyCount;
    private float xPos = 17;
    private float yPos = 5;
    private float zPos = 12;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnMinion();
    }

    void SpawnMinion()
    {
        int randNum = Random.Range(0, enemyPrefab.Length);
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (enemyCount == 0)
        {
            Instantiate(enemyPrefab[randNum], RandomSpawnLocation(), enemyPrefab[randNum].transform.rotation);
        }
    }

    Vector3 RandomSpawnLocation()
    {
        Vector3 location;
        float randNum = Random.Range(-xPos, xPos);

        location = new Vector3(randNum, yPos, zPos);

        return location;
    }
}
