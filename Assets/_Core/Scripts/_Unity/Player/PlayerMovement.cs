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
        if (moveInput.sqrMagnitude < 0.01f)
        {
            return;
        }

        float speed = isSlow ? config.slowSpeed : config.regularSpeed;
        Vector3 direction = new Vector3(moveInput.x, 0.0f, moveInput.y).normalized;

        cc.Move(speed * Time.deltaTime * direction);
        transform.forward = Vector3.Lerp(transform.forward, direction, 0.2f);

        float intensity = isSlow ? config.noiseWalkSlow : config.noiseWalkRegular;
        OnMoved?.Invoke(intensity, transform.position);
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
