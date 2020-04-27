using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.Input;

public class GamepadInput : UserInput
{
    public int id = -1;
    private Vector2 direction;
    private bool isBreaking;
    private bool isRunning;
    private bool onGrab;

    public override Vector2 Direction => direction;
    public override bool IsBreaking => isBreaking;
    public override bool IsRunning => isRunning;
    public override bool OnGrab => onGrab;

    public override void Update()
    {
        if (id == -1)
            return;
        var gp = Gamepad.all.First(c => c.id == id);
        if (gp == null)
            return;
        direction = gp.leftStick.ReadValue();
        isBreaking = gp.buttonWest.isPressed;
        isRunning = gp.buttonEast.isPressed;
        onGrab = gp.buttonSouth.wasPressedThisFrame;
    }
}
