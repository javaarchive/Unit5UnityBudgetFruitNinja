using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public GameObject[] targetPrefabs;

    [SerializeField]
    public GameObject[] targetHalves;

    private float spawnRate = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnTarget());
    }

    private IEnumerator SpawnTarget(){
        while(true){
            yield return new WaitForSeconds(spawnRate);
            int choice = Random.Range(0, targetPrefabs.Length);
            GameObject fruit = targetPrefabs[choice];
            Instantiate(fruit, StartingPosition(), fruit.transform.rotation);
        }
    }

    private Vector3 StartingPosition(){
        float x = Random.Range(-4.5f, -5f);
        Vector3 location = new Vector3(x,0, -1f);
        return location;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
