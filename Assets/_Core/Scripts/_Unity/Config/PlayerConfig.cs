using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "HollowWatch/Player Config")]
public class PlayerConfig : ScriptableObject
{
    [Header("Movement")]
    public float regularSpeed = 4.0f;
    public float slowSpeed = 2.0f;

    [Header("Noise Intensity")]
    public float noiseWalkRegular = 30.0f;
    public float noiseWalkSlow = 5.0f;
    public float noiseRun = 70.0f;
    public float noiseDoorSlow = 20.0f;
    public float noiseDoorFast = 55.0f;
    public float noiseDecoyImpact = 90.0f;
    public float noiseExitHiding = 25.0f;

    [Header("Scent Trail")]
    public float scentdepositInterval = 0.5f;
    public float scentdepositIntervalSlow = 1.5f;

    [Header("Inventory")]
    public int maxDecoys = 2;

    [Header("Footstep Throttle")]
    public float footstepEmitInterval = 0.25f;
}
