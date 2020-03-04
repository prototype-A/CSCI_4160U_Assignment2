using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSCharacterController : MonoBehaviour {

    // Camera
    public float mouseSensitivity = 100.0f;
    private float hLook = 0.0f;
    private float vLook = 0.0f;

    // Body
    private CharacterController charController;
    private float walkSpeed = 4.5f;
    private float runSpeed = 8.0f;

    // Firing
    private Transform cameraT;
    private float range = 100.0f;


    void Start() {
        // Lock and hide cursor at center of screen
        Cursor.lockState = CursorLockMode.Locked;

        // Get camera transform
        this.cameraT = transform.Find("Main Camera");

        // Get Character Controller component
        this.charController = GetComponent<CharacterController>();
    }

    void Update() {
        // Camera movement
        float hCameraMovement = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float vCameraMovement = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        hLook += hCameraMovement;
        vLook -= vCameraMovement;
        vLook = Mathf.Clamp(vLook, -75, 75);

        // Player movement
        float hMovement = Input.GetAxis("Horizontal");
        float vMovement = Input.GetAxis("Vertical");
        Vector3 movement = transform.right * hMovement + transform.forward * vMovement;
        if (Input.GetKey(KeyCode.LeftShift)) {
            // Running
            charController.SimpleMove(movement * runSpeed);
        } else {
            // Walking
            charController.SimpleMove(movement * walkSpeed);
        }

        // Fire
        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space)) {
            Shoot();
        }

        // Move player
        transform.localRotation = Quaternion.Euler(vLook, hLook, 0.0f);
    }

    private void Shoot() {
        Debug.Log("Fire!");

        // Detect collisions in only enemy or ground layers
        LayerMask groundMask = LayerMask.GetMask("Ground");
        LayerMask enemyMask = LayerMask.GetMask("Enemies");

        // Detect collision
        RaycastHit hit;
        if (Physics.Raycast(cameraT.position, cameraT.forward, out hit, range, groundMask)) {
            // Hit the ground

        } else if (Physics.Raycast(cameraT.position, cameraT.forward, out hit, range, enemyMask)) {
            // Hit the enemy

        }
    }
}
