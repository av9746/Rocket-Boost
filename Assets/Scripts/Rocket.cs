using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class Rocket : MonoBehaviour {
    private Rigidbody rigidBody;
    private AudioSource audioSource;

    [SerializeField]private float rcsTHrust = 100;
    [SerializeField]private float mainTHrust = 100;
    
    // Start is called before the first frame update
    void Start() {
        rigidBody = gameObject.GetComponent<Rigidbody>();
        audioSource = gameObject.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        Thrusting();
        Rotate();
    }

    private void OnCollisionEnter(Collision other) {

        switch (other.gameObject.tag) {
            case "Friendly":
                break;
            default:
                Destroy(gameObject);
                break;
        }

    }

    private void Thrusting() {
        if (Input.GetKey(KeyCode.Space)) { //can thrust while rotating
            
            rigidBody.AddRelativeForce(Vector3.up*mainTHrust);
            
            if (!audioSource.isPlaying) { // so it doesn't layer
                audioSource.Play();
            }
        }
        else {
            audioSource.Stop();
        }
    }

    private void Rotate() {

        rigidBody.freezeRotation = true; //take manual control

        var rotationThisFrame = rcsTHrust * Time.deltaTime;
        
        if (Input.GetKey(KeyCode.D)) { // can rotate only one way simuntaniously
            transform.Rotate(Vector3.forward * rotationThisFrame); //forward means z axis
        }
        else if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        rigidBody.freezeRotation = false; // resume physics control of rotation
    }

}
