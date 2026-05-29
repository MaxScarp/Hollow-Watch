using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public event Action<Vector2> OnMoveInput;
    public event Action<bool> OnSlowWalkChanged;
    public event Action<bool> OnRunChanged;
    public event Action OnInteractPressed;
    public event Action OnThrowDecoyPressed;

    private PlayerInputActions actions;

    private Action<InputAction.CallbackContext> onMove;
    private Action<InputAction.CallbackContext> onMoveCanceled;
    private Action<InputAction.CallbackContext> onSlowStart;
    private Action<InputAction.CallbackContext> onSlowStop;
    private Action<InputAction.CallbackContext> onRunStart;
    private Action<InputAction.CallbackContext> onRunStop;
    private Action<InputAction.CallbackContext> onInteract;
    private Action<InputAction.CallbackContext> onThrow;

    private void Awake()
    {
        actions = new();

        onMove = ctx => OnMoveInput?.Invoke(ctx.ReadValue<Vector2>());
        onMoveCanceled = ctx => OnMoveInput?.Invoke(Vector2.zero);
        onSlowStart = ctx => OnSlowWalkChanged?.Invoke(true);
        onSlowStop = ctx => OnSlowWalkChanged?.Invoke(false);
        onRunStart = ctx => OnRunChanged?.Invoke(true);
        onRunStop = ctx => OnRunChanged?.Invoke(false);
        onInteract = ctx => OnInteractPressed?.Invoke();
        onThrow = ctx => OnThrowDecoyPressed?.Invoke();
    }

    private void OnEnable()
    {
        actions.Player.Enable();
        actions.Player.Move.performed += onMove;
        actions.Player.Move.canceled += onMoveCanceled;
        actions.Player.SlowWalk.performed += onSlowStart;
        actions.Player.SlowWalk.canceled += onSlowStop;
        actions.Player.Run.performed += onRunStart;
        actions.Player.Run.canceled += onRunStop;
        actions.Player.Interact.performed += onInteract;
        actions.Player.ThrowDecoy.performed += onThrow;
    }

    private void OnDisable()
    {
        actions.Player.Move.performed -= onMove;
        actions.Player.Move.canceled -= onMoveCanceled;
        actions.Player.SlowWalk.performed -= onSlowStart;
        actions.Player.SlowWalk.canceled -= onSlowStop;
        actions.Player.Run.performed -= onRunStart;
        actions.Player.Run.canceled -= onRunStop;
        actions.Player.Interact.performed -= onInteract;
        actions.Player.ThrowDecoy.performed -= onThrow;
        actions.Player.Disable();
    }
}
