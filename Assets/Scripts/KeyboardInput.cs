using UnityEngine;

[CreateAssetMenu(fileName = "Keyboard Input", menuName = "Keyboard Input")]
public class KeyboardInput : UserInput
{
    public KeyCode Up = KeyCode.W;
    public KeyCode Down = KeyCode.S;
    public KeyCode Left = KeyCode.A;
    public KeyCode Right = KeyCode.D;

    public KeyCode Grab = KeyCode.P;
    public KeyCode Run = KeyCode.LeftShift;
    public KeyCode Break = KeyCode.O;

    private Vector2 direction;
    private bool isRunning;
    private bool isBreaking;
    private bool onGrab;

    public override Vector2 Direction => direction;
    public override bool IsBreaking => isBreaking;
    public override bool IsRunning => isRunning;
    public override bool OnGrab => onGrab;

    public override void Update()
    {
        direction = Vector2.zero;
        if (Input.GetKey(Right))
            direction += Vector2.right;
        if (Input.GetKey(Left))
            direction += Vector2.left;
        if (Input.GetKey(Up))
            direction += Vector2.up;
        if (Input.GetKey(Down))
            direction += Vector2.down;
        isBreaking = Input.GetKey(Break);
        isRunning = Input.GetKey(Run);
        onGrab = Input.GetKeyDown(Grab);
    }

}
