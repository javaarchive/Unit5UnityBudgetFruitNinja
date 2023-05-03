using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this is run on the chopped pieces to add a spin effect to make things feel nicer
public class Spin : MonoBehaviour
{

    private Rigidbody targetRb;

    private void Awake(){
        targetRb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        targetRb.AddForce(Force(), ForceMode.Impulse);
        targetRb.AddTorque(Torque(), Torque(), Torque(), ForceMode.Impulse);
    }

    // rand force
    private Vector3 Force(){
        float x = Random.Range(-3f, 3f);
        float y = Random.Range(-2f, 5f);
        Vector3 direction = new Vector3(x, y, -1f);
        return direction;
    }

    // rand axis of torque 
    private float Torque(){
        float spin = Random.Range(-10, 10);
        return spin;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x > 7f || transform.position.y < -1f){
            Destroy(gameObject);
        }
    }
}
