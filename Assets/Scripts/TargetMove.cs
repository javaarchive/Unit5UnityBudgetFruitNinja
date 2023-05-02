using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMove : MonoBehaviour
{
    private Rigidbody targetRb;

    private GameManager gmScript;

    [SerializeField]
    private ParticleSystem splash;

    private void Awake()
    {
        targetRb = GetComponent<Rigidbody>();
        gmScript = GameObject.Find("SpawnManager").GetComponent<GameManager>();
    }

    // Start is called before the first frame update
    void Start() {
        float x = Random.Range(1f, 1.75f) * 3;
        float y = Random.Range(1f, 1.6f) * 6;
        Vector3 dir = new Vector3(x, y, 0);
        targetRb.AddForce(dir, ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), ForceMode.Impulse);
    }
    private Vector3 RandomTorque()
    {
        float x = Random.Range(-10, 10);
        float y = Random.Range(-10, 10);
        float z = Random.Range(-10, 10);
        Vector3 spin = new Vector3(x, y, 2);
        return spin;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x > 7f || transform.position.y < -1f){
            // user didn't click and we went out of frame
            if(!gameObject.name.Contains("onion")){
                gmScript.SubtractLife();    
            }
            Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        for(int i = 0; i < gmScript.targetPrefabs.Length; i++){
            Debug.Log(gmScript.targetPrefabs[i].name + " vs " + gameObject.name);
            if(gmScript.targetHalves[i].name.Contains(gameObject.name.Replace("(Clone)",""))){ // hack
                Instantiate(splash, gameObject.transform.position, gameObject.transform.rotation);
                Instantiate(gmScript.targetHalves[i], gameObject.transform.position, gameObject.transform.rotation);
                Instantiate(gmScript.targetHalves[i], gameObject.transform.position, gameObject.transform.rotation);
                // upd score
                int calculatedScoreChange = 10;
                if(gameObject.name.Contains("onion")){
                    calculatedScoreChange = -5;
                }
                gmScript.ChangeScore(calculatedScoreChange);

                Destroy(gameObject);
            }
        }
    }
}
