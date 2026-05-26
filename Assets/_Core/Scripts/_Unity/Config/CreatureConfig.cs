using UnityEngine;

[CreateAssetMenu(fileName = "CreatureConfig", menuName = "HollowWatch/Creature Config")]
public class CreatureConfig : ScriptableObject
{
    [Header("Speed")]
    public float PatrolSpeed = 2.5f;
    public float AlertSpeed = 3.5f;
    public float ChaseSpeed = 5.5f;

    [Header("Detection Thresholds")]
    public float AlertThreshold = 35.0f;
    public float AlertSoundRadius = 12.0f;
    public float AlertDuration = 12.0f;
    public float ChaseLostTimer = 8.0f;
    public float HearingRadiusMultiplier = 0.15f;

    [Header("Navigation")]
    public float WaypointReachedThreshold = 0.5f;
}
