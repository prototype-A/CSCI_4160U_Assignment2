using UnityEngine;
using TMPro;

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
    public TextMeshProUGUI killCounter;
    private int killCount = 0;
    public GameObject paintSplat1;
    public GameObject paintSplat2;
    private Transform cameraT;
    private float range = 50.0f;
    private int damage = 10;


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
        transform.localRotation = Quaternion.Euler(vLook, hLook, 0.0f);

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
    }

    private void Shoot() {
        // Only hit the ground, buildings, or enemies
        LayerMask enemyMask = LayerMask.GetMask("Enemies");
        LayerMask buildingMask = LayerMask.GetMask("Buildings");
        LayerMask groundMask = LayerMask.GetMask("Ground");
        int splatLayers = ~(1 << buildingMask.value << groundMask.value);

        // Detect collision
        RaycastHit hit;
        if (Physics.Raycast(cameraT.position, cameraT.forward, out hit, range, enemyMask)) {
            // Hit the enemy for damage
            if (hit.collider.GetComponent<EnemyController>().TakeDamage(damage)) {
                // Update kill count when enemy dies
                killCount++;
                killCounter.text = "" + killCount;
            }
        } else if (Physics.Raycast(cameraT.position, cameraT.forward, out hit, range, splatLayers)) {
            // Make a paint splat if hit the terrain/a building
            Instantiate((Random.Range(0, 2) == 0) ? paintSplat1 : paintSplat2,
                hit.point + (0.01f * hit.normal),
                Quaternion.LookRotation(-1 * hit.normal, hit.transform.up));
        }
    }
}
