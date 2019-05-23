using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class Rocket : MonoBehaviour {
    private Rigidbody rigidBody;
    // Start is called before the first frame update
    void Start() {
        rigidBody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput() {
        if (Input.GetKey(KeyCode.Space)) { //can thrust while rotating
            rigidBody.AddRelativeForce(Vector3.up);
            print("Thrusting");
        } 
        
        if (Input.GetKey(KeyCode.D)) { // can rotate only one way simuntaniously
            transform.Rotate(Vector3.forward); //forward means z axis
        } else if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(-Vector3.forward);
        }
        
    }
}
