using UnityEngine;
using UnityEngine.SceneManagement;


public class Rocket : MonoBehaviour
{

    private Rigidbody rigidBody;
    [SerializeField] private float rcsTHrust = 100f;
    [SerializeField] private float mainTHrust = 100f;
    [SerializeField] private float levelLoad = 1f;

    //Sound
    private AudioSource audioSource;
    [SerializeField] private AudioClip mainEngine;
    [SerializeField] private AudioClip success;
    [SerializeField] private AudioClip explosion;

    //Particles
    [SerializeField] private ParticleSystem successParticles;
    [SerializeField] private ParticleSystem mainEngineParticles;
    [SerializeField] private ParticleSystem explosionParticles;

    [SerializeField] private bool collisionsDisabled = false;



    enum State
    {
        Alive,
        Dying,
        Transcending
    }

    private State state = State.Alive;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            RespondToThrustInput();
            RespondToRotateInput();
        }

        if (Debug.isDebugBuild)
        {
            RespondToDebugKeys();
        }
        
    }

    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            StartSuccessSequence();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionsDisabled = !collisionsDisabled; //toggle
        }
    }


    private void OnCollisionEnter(Collision other) {

        if (state != State.Alive || collisionsDisabled) {return;} //like a guard statement---ignore collisions when dead

        switch (other.gameObject.tag) {
            case "Friendly":
                break;
            case "Finish":
                StartSuccessSequence();
                break;            
            default:                
                StartDeathSequence();
                break;
        }
 
    }

    private void StartSuccessSequence() {
        state = State.Transcending;
        audioSource.PlayOneShot(success);
        successParticles.Play();
        Invoke("LoadNextLevel", levelLoad);
    }

    private void StartDeathSequence() {
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(explosion);
        explosionParticles.Play();
        var currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene); ///for testing levels
        //
        //Invoke("LoadFirstLevel", levelLoad);
    }


    private void LoadNextLevel() {
        SceneManager.LoadScene(1);
    }

    private void LoadFirstLevel() {
        SceneManager.LoadScene(0);
    }

    private void RespondToThrustInput() {
        if (Input.GetKey(KeyCode.Space)) { //can thrust while rotating
            ApplyThrust();
        }
        else {
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
    }

    private void ApplyThrust() {
        rigidBody.AddRelativeForce(Vector3.up*mainTHrust*Time.deltaTime);
        if (!audioSource.isPlaying) { // so it doesn't layer
            audioSource.PlayOneShot(mainEngine);
        }
        mainEngineParticles.Play();
    }

    private void RespondToRotateInput() {

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
