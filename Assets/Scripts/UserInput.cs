using UnityEngine;

public abstract class UserInput : ScriptableObject
{
    public abstract Vector2 Direction { get; }
    public abstract bool IsBreaking { get; }
    public abstract bool IsRunning { get; }
    public abstract bool OnGrab { get; }
    public abstract void Update();
}

public class VoidInput : UserInput
{
    public override Vector2 Direction => Vector2.zero;
    public override bool IsBreaking => false;
    public override bool IsRunning => false;
    public override bool OnGrab => false;
    public override void Update() {  }
}
