using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpStrength = 5;
    public const float gravity = 9.81f;
    private float airTime = 0;
    [HideInInspector] public bool grounded;
    [SerializeField] private Transform groundCheck;
    [SerializeField] LayerMask groundLayer;

    private CharacterController controller;
    private InputAction moveAction;
    private InputAction jumpAction;

    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    void Update()
    {
        grounded = Physics.CheckSphere(groundCheck.position, 0.04f, groundLayer);
        airTime = grounded ? 0 : airTime + Time.deltaTime;

        Vector2 playerInput = moveAction.ReadValue<Vector2>();
        Vector3 velocity = new(
            x: playerInput.normalized.x * speed * Time.deltaTime,
            y: jumpAction.WasPressedThisFrame() && grounded ? jumpStrength : -gravity * Time.deltaTime * airTime,
            z: playerInput.normalized.y * speed * Time.deltaTime
        );

        controller.Move(velocity);
    }
}
