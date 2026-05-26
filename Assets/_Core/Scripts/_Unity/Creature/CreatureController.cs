using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CreatureController : MonoBehaviour
{
    [SerializeField] private CreatureConfig config;
    [SerializeField] private Transform[] waypointTransforms;

    private NavMeshAgent agent;
    private CreatureStateMachine fsm;
    private CreatureContext ctx;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        ctx = BuildContenxt();
        fsm = new();
    }

    private void Start()
    {
        agent.speed = config.PatrolSpeed;

        GameEventBus.Subscribe<SoundStimulusEvent>(OnSoundStimulus);

        fsm.Initialize(ctx);
    }

    private void OnDestroy()
    {
        GameEventBus.Unsubscribe<SoundStimulusEvent>(OnSoundStimulus);
    }

    private void Update()
    {
        ctx.CreaturePosition = Vector3Utils.ToCore(transform.position);

        fsm.Tick(ctx, Time.deltaTime);

        agent.speed = fsm.CreatureState switch
        {
            CreatureState.Patrol => config.PatrolSpeed,
            CreatureState.Alert => config.AlertSpeed,
            CreatureState.Chase => config.ChaseSpeed,
            _ => config.PatrolSpeed
        };
    }

    private CreatureContext BuildContenxt()
    {
        return new()
        {
            AlertThreshold = config.AlertThreshold,
            AlertDuration = config.AlertDuration,
            WaypointReachedThreshold = config.WaypointReachedThreshold,

            SetDestination = pos => agent.SetDestination(Vector3Utils.ToUnity(pos)),
            StopMovement = () => agent.ResetPath(),

            OnStateChanged = (next, prev) => GameEventBus.Publish(new AIStateChangedEvent(next, prev)),

            PatrolWaypoints = Array.ConvertAll(waypointTransforms, t => Vector3Utils.ToCore(t.position))
        };
    }

    private void OnSoundStimulus(SoundStimulusEvent e)
    {
        float distance = Vector3.Distance(transform.position, Vector3Utils.ToUnity(e.Position));
        float hearingRadius = e.Intensity * config.HearingRadiusMultiplier;

        if (distance > hearingRadius)
        {
            return;
        }

        if (distance > config.AlertSoundRadius)
        {
            return;
        }

        ctx.HasPendingSound = true;
        ctx.PendingSoundIntensity = e.Intensity;
        ctx.PendingSoundOrigin = e.Position;
    }
}
