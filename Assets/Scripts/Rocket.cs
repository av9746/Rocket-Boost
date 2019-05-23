using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class Rocket : MonoBehaviour {
    private Rigidbody rigidBody;
    private AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start() {
        rigidBody = gameObject.GetComponent<Rigidbody>();
        audioSource = gameObject.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput() {
        if (Input.GetKey(KeyCode.Space)) { //can thrust while rotating
            rigidBody.AddRelativeForce(Vector3.up);
            print("thrusting");
            if (!audioSource.isPlaying) {
                audioSource.Play();           
            }

        } else {
            audioSource.Stop();
        }
        
        if (Input.GetKey(KeyCode.D)) { // can rotate only one way simuntaniously
            transform.Rotate(Vector3.forward); //forward means z axis
        } else if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(-Vector3.forward);
        }
        
    }
    
    
}
