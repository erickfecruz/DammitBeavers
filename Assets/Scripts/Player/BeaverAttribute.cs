using UnityEngine;

[CreateAssetMenu(fileName = "Beaver Attribute", menuName = "Beaver Attribute")]
public class BeaverAttribute : ScriptableObject
{
    public float Speed = 30;
    public float RunningSpeed = 30;
    public float Force = 5;
    public float MaxStamina = 100;
    public float StaminaRecovery = 10;
}
