using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSCharacterController : MonoBehaviour {

    // Camera
    public float mouseSensitivity = 100.0f;
    private float hLook = 0.0f;
    private float vLook = 0.0f;

    // Body
    private CharacterController charController;
    private float moveSpeed = 8.0f;


    void Start() {
        // Lock and hide cursor at center of screen
        Cursor.lockState = CursorLockMode.Locked;

        // Get Character Controller component
        charController = GetComponent<CharacterController>();
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
        charController.SimpleMove(movement * moveSpeed);

        // Move player GameObject
        transform.localRotation = Quaternion.Euler(vLook, hLook, 0.0f);
    }
}
