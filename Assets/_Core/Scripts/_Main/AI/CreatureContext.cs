using System;
using System.Numerics;

public class CreatureContext
{
    public Vector3 CreaturePosition;
    public int CurrentWaypointIndex;
    public float AlertTimer;

    public bool HasPendingSound;
    public float PendingSoundIntensity;
    public Vector3 PendingSoundOrigin;

    public float AlertThreshold;
    public float AlertDuration;
    public float WaypointReachedThreshold;

    public Vector3[] PatrolWaypoints;

    public Action<Vector3> SetDestination;
    public Action StopMovement;
    public Action<CreatureState, CreatureState> OnStateChanged;

    public CreatureContext()
    {
        PatrolWaypoints = Array.Empty<Vector3>();
    }

    public bool ConsumePendingSound(out float intensity, out Vector3 origin)
    {
        intensity = PendingSoundIntensity;
        origin = PendingSoundOrigin;

        if (!HasPendingSound)
        {
            return false;
        }

        HasPendingSound = false;
        return true;
    }
}
