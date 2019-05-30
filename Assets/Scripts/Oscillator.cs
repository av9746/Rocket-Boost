using UnityEngine;

[DisallowMultipleComponent] //only one oscillator script can be aded on the component

public class Oscillator : MonoBehaviour {
    
    [SerializeField] private Vector3 movementVector3 = new Vector3(10f, 10f, 10f);
    [Range(0, 1)] [SerializeField] private float movementFactor;
    [SerializeField] private float period = 2f;

    private Vector3 startingPos;
    private Vector3 offset;
      
    
    // Start is called before the first frame update
    void Start() {

        startingPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update() {
        offset = movementVector3 * movementFactor;
        transform.position = startingPos + offset;

        // protect dividing with zero
        if (period <= Mathf.Epsilon || period == null) {
            period = 2;
        }
        float cycles = Time.time / period; // continuasly grow in 10s t5 cycles happened

        const float tau = Mathf.PI * 2; //we want whole cycle which is 2PI
        float rawSinWave = Mathf.Sin(cycles * tau);// goes from -1 to + 1 

        movementFactor = rawSinWave / 2f + 0.5f; // so it goes from 0 to 1
    }
}
