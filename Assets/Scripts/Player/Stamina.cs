using UnityEngine;
using UnityEngine.Events;

public class Stamina : MonoBehaviour
{
    private float actualStamina;
    private float maxStamina = 100;
    private float minStamina = 0;
    private float increaseRateStamina = 6;
    private bool stun;

    BeaverAnimationTiredEvent beaverAnimationTired = new BeaverAnimationTiredEvent();

    // Start parameters
    void Start()
    {
        actualStamina = 100;
        stun = false;
        EventManager.AddBeaverAnimationTiredInvoker(this);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckStaminaOver();
        IncreaseStamina(increaseRateStamina * Time.fixedDeltaTime);
        CheckPlayerAlive();
    }

    /// <summary>
    /// Check if player lost all his stamina
    /// </summary>
    void CheckStaminaOver()
    {
        if (actualStamina == minStamina)
        {
            stun = true;
            //GetComponent<Movement>().enabled = false;
            beaverAnimationTired.Invoke(0);
        }
    }

    /// <summary>
    /// Check if player can move 
    /// </summary>
    void CheckPlayerAlive()
    {
        if (!stun)
            return;

        if (actualStamina == maxStamina)
        {
            stun = false;
            //GetComponent<Movement>().enabled = true;
            beaverAnimationTired.Invoke(1);
        }
    }

    /// <summary>
    /// Decrease player stamina
    /// </summary>
    /// <param name="ammount"></param>
    public void DecreaseStamina(float ammount)
    {
        actualStamina = actualStamina - ammount;
        if (actualStamina < minStamina)
            actualStamina = minStamina;
    }

    /// <summary>
    /// Increase player stamina
    /// </summary>
    /// <param name="ammount"></param>
    public void IncreaseStamina(float ammount)
    {
        actualStamina = actualStamina + ammount;
        if (actualStamina > maxStamina)
            actualStamina = maxStamina;
    }

    /// <summary>
    /// Listener
    /// </summary>
    /// <param name="listener"></param>
    public void AddBeaverAnimationTiredListener(UnityAction<int> listener)
    {
        beaverAnimationTired.AddListener(listener);
    }

}
