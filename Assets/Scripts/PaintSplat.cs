using UnityEngine;

public class PaintSplat : MonoBehaviour {

    private float timeCreated;
    private readonly float DURATION = 10.0f;


    void Start() {
        this.timeCreated = Time.time;
    }

    void Update() {
        // Remove this paint splat after its duration
        if (Time.time - DURATION >= timeCreated) {
            Destroy(gameObject);
        }
    }
}
