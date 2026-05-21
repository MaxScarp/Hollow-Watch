using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerConfig config;
    [SerializeField] private PlayerInputHandler inputHandler;

    public event Action<float, Vector3> OnMoved;

    private CharacterController cc;
    private Vector2 moveInput;
    private bool isSlow;

    private float verticalVelocity;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        inputHandler.OnMoveInput += InputHandler_OnMoveInput;
        inputHandler.OnSlowWalkChanged += InputHandler_OnSlowWalkChanged;
    }

    private void OnDisable()
    {
        inputHandler.OnMoveInput -= InputHandler_OnMoveInput;
        inputHandler.OnSlowWalkChanged -= InputHandler_OnSlowWalkChanged;
    }

    private void Update()
    {
        if (cc.isGrounded && verticalVelocity < 0.0f)
        {
            verticalVelocity = -2.0f;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        if (moveInput.sqrMagnitude > 0.01f)
        {
            float speed = isSlow ? config.slowSpeed : config.regularSpeed;
            Vector3 direction = new Vector3(moveInput.x, 0.0f, moveInput.y).normalized;

            transform.forward = Vector3.Lerp(transform.forward, direction, 0.2f);

            float intensity = isSlow ? config.noiseWalkSlow : config.noiseWalkRegular;

            Vector3 motion = direction * speed;
            motion.y = verticalVelocity;
            cc.Move(motion * Time.deltaTime);

            OnMoved?.Invoke(intensity, transform.position);
        }
        else
        {
            cc.Move(new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime);
        }
    }

    private void InputHandler_OnSlowWalkChanged(bool isSlow)
    {
        this.isSlow = isSlow;
    }

    private void InputHandler_OnMoveInput(Vector2 moveInput)
    {
        this.moveInput = moveInput;
    }
}
