using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGenerator : MonoBehaviour
{
    [Header("Rock Settings")]
    [SerializeField] Transform[] rockPrefabs = null;
    [SerializeField] float minSize = 0.8f;
    [SerializeField] float maxSize = 1.2f;

    [Header("Spawn Settings")]
    [SerializeField] Transform spawnPosition = null;
    [SerializeField] float minDistanceFromCenter = 1;
    [SerializeField] float maxDistanceFromCenter = 5;
    [SerializeField] int minRocksInGame = 2;
    [SerializeField] int targetRocksInGame = 4;
    [SerializeField] float averageTimeToSpawnRock = 5;

    // Start is called before the first frame update
    void Start()
    {
        InitRocks();
    }

    // Update is called once per frame
    void Update()
    {
        bool spawnRock = false;
        if(RockInGame.rockCount<minRocksInGame)
        {
            //Spawn a rock
            spawnRock = true;
        }

        int differenceWithTargetRocks = targetRocksInGame - RockInGame.rockCount;
        if (differenceWithTargetRocks>0)
        {
            if(Random.Range(0f, 1f)<Time.deltaTime * differenceWithTargetRocks / averageTimeToSpawnRock)
            {
                spawnRock = true;
            }
        }

        if(spawnRock)
        {
            SpawnRock();
        }
    }

    void InitRocks()
    {
        for (int i = 0; i < minRocksInGame; i++)
        {
            Transform newRock = SpawnRock();
            newRock.gameObject.name = "InitRock " + i.ToString();
            newRock.Translate(Vector3.right * 15 * (i - 1), Space.World);
        }
    }

    [ContextMenu("SpawRock")]
    Transform SpawnRock()
    {
        //Find the rock to spawn
        int randRock = Random.Range(0, rockPrefabs.Length);

        Quaternion randRot = Random.rotation;
        Vector3 size = new Vector3(Random.Range(minSize, maxSize), Random.Range(minSize, maxSize), Random.Range(minSize, maxSize));

        Vector3 pos = new Vector3(spawnPosition.position.x, spawnPosition.position.y, Random.Range(minDistanceFromCenter, maxDistanceFromCenter));
        //choose between negative or positive
        if(Random.Range(0f, 1f)>0.5f)
        {
            pos.z *= -1;
        }

        Transform newRock = Instantiate(rockPrefabs[randRock], pos, randRot, transform);
        newRock.localScale = size;
        return newRock;
    }
}
