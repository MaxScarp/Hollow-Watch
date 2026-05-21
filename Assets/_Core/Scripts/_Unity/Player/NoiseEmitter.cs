using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class NoiseEmitter : MonoBehaviour
{
    [SerializeField] private PlayerConfig config;

    [Header("Debug")]
    [SerializeField] private bool logEmissions = false;

    private PlayerMovement movement;
    private float nextFootstepTime;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        movement.OnMoved += Movement_OnMoved;
    }

    private void OnDisable()
    {
        movement.OnMoved -= Movement_OnMoved;
    }

    private void Movement_OnMoved(float intensity, Vector3 position)
    {
        if (Time.time < nextFootstepTime)
        {
            return;
        }

        nextFootstepTime = Time.time + config.footstepEmitInterval;
        Emit(intensity, position, SoundSourceType.Footstep);
    }

    private void Emit(float intensity, Vector3 position, SoundSourceType source)
    {
        GameEventBus.Publish(new SoundStimulusEvent(position.x, position.y, position.z, intensity, source));

#if UNITY_EDITOR
        if (logEmissions)
        {
            Debug.Log($"[NoiseEmitter] {source} | intensity: {intensity} | pos: {position}");
        }
#endif
    }

    public void EmitAction(float intensity, Vector3 position)
    {
        Emit(intensity, position, SoundSourceType.Action);
    }

    public void EmitDecoy(Vector3 impactPosition)
    {
        Emit(config.noiseDecoyImpact, impactPosition, SoundSourceType.Decoy);
    }
}
