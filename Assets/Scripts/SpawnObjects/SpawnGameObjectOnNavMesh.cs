using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class SpawnGameObjectOnNavMesh : MonoBehaviour
{
    [SerializeField] private GameObject weaponBox;
    [SerializeField] private GameObject pointBox;
    public float spawnRadius = 500f;
    public float groundHeight = 0.5f;

    [SerializeField] private bool FirstRound = true;
    /*
    public void WeaponAirSpawnBoxOnNavMesh()
    {
        float spawnHeight = Random.Range(5, 10);
        SpawnObjectOnNavMesh(spawnHeight, weaponBox);
    }
    */
 
    void Start()
    {
        //Spawn ground box:

        //Use State machine to swawpn on new round.
        //Spawn on ground
        
        //RemoveAllBoxes();
    }

    private void RemoveAllBoxes()
    {
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("SpawnedBox");

        if (boxes == null) { return; }
        
        foreach (GameObject box in boxes)
        {
            Destroy(box);
        }
    }

    //refactor:
    //STRUCTPointBoxParameters and STRUCTWEAPONBoxParameters are diffrent so I have to create two different functions.
    //Need to make it an object then recreate. Reference more in PointBoxParatmeter class.
    public void SpawnPointBox(STRUCTPointBoxParameters parameters, GameObject objectToSpawn)
    {
        int amountOfBoxes = Random.Range(parameters.MinimumAmount, parameters.MaximumAmount);

        for (int i = 0; i < amountOfBoxes; i++)
        {
            float spawnHeight = Random.Range(parameters.MinimumHeight, parameters.MaximumHeight);
            SpawnObjectOnNavMesh(spawnHeight, objectToSpawn);
        }
    }
    
    public void SpawnWeaponBox(STRUCTWeaponBoxParameters parameters, GameObject objectToSpawn)
    {
        int amountOfBoxes = Random.Range(parameters.MinimumAmount, parameters.MaximumAmount);

        for (int i = 0; i < amountOfBoxes; i++)
        {
            float spawnHeight = Random.Range(parameters.MinimumHeight, parameters.MaximumHeight);
            SpawnObjectOnNavMesh(spawnHeight, objectToSpawn);
        }
    }
    
    public void SpawnKeyBox(STRUCTKeyBoxParameters parameters, GameObject objectToSpawn, GameObject ghostObject)
    {
        int amountOfBoxes = Random.Range(parameters.MinimumAmount, parameters.MaximumAmount);

        for (int i = 0; i < amountOfBoxes; i++)
        {
            float spawnHeight = Random.Range(parameters.MinimumHeight, parameters.MaximumHeight);
            SpawnObjectOnNavMesh(spawnHeight, objectToSpawn);
            SpawnObjectOnNavMesh(spawnHeight, ghostObject);
        }
        
    }
    private void SpawnObjectOnNavMesh(float spawnHeight, GameObject spawnObject)
    {
        Vector3 randomPoint = RandomNavmeshLocation(spawnRadius);

        if (randomPoint != Vector3.zero)
        {
            Vector3 spawnPoint = new Vector3(randomPoint.x, randomPoint.y + spawnHeight, randomPoint.z);
            Instantiate(spawnObject, spawnPoint, Quaternion.identity);
            print("PointBoxes spawned");

        }
    }
    

    Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return Vector3.zero;
    }
    
    public void ClearAllPointBoxes()
    {
        GameObject[] pointBoxes = GameObject.FindGameObjectsWithTag("SpawnedBox");
        foreach (GameObject pointBox in pointBoxes)
        {
            Destroy(pointBox);
        }

        print("PointBoxes removed");
    }
}