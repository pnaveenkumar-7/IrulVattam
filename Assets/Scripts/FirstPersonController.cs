using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    public Transform cameraTransform;
    public float walkSpeed = 3.5f;
    public float mouseSensitivity = 2f;
    public float interactDistance = 3f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private float pitch;
    private float yVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Look();
        Move();
        if (Input.GetKeyDown(KeyCode.E)) TryInteract();
    }

    void Look()
    {
        float mx = Input.GetAxis("Mouse X") * mouseSensitivity;
        float my = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(0f, mx, 0f);
        pitch = Mathf.Clamp(pitch - my, -80f, 80f);
        cameraTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    void Move()
    {
        Vector3 input = transform.right * Input.GetAxis("Horizontal")
                      + transform.forward * Input.GetAxis("Vertical");
        if (input.magnitude > 1f) input.Normalize();

        if (controller.isGrounded && yVelocity < 0f) yVelocity = -2f;
        yVelocity += gravity * Time.deltaTime;

        Vector3 move = input * walkSpeed + Vector3.up * yVelocity;
        controller.Move(move * Time.deltaTime);
    }

    void TryInteract()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            ClueItem clue = hit.collider.GetComponent<ClueItem>();
            if (clue != null) clue.Interact();
        }
    }
}