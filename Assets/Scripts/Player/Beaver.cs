using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class Beaver : MonoBehaviour
{
    private const float StaminaDrain = 40;

    public UserInput input;

    public float Speed = 10;
    public float RunningSpeed = 15;
    public float MaxStamina = 100;
    public float StaminaRecovery = 10;

    [HideInInspector]
    public int Id = -1;

    private float _stamina;
    public float Stamina
    {
        get => _stamina;
        set => _stamina = Mathf.Clamp(value, 0, MaxStamina);
    }

    public float ActualSpeed => input.IsRunning && Stamina > 0 ? RunningSpeed : Speed;

    public bool IsBreaking => input.IsBreaking && Physics2D.OverlapCircleAll(transform.position, 20).Any(e => e.GetComponent<Arvore>() != null);

    public bool Stunned { get; private set; }

    private Rigidbody2D rb;
    public Animator Animator { get; private set; }
    public SpriteRenderer SpriteRenderer { get; private set; }
    public Collider2D Collider2D { get; private set; }
    public Animation StunAnimation;

    void Awake()
    {
        if (input == null)
            input = new VoidInput();
        rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Collider2D = GetComponent<Collider2D>();
        Stamina = MaxStamina;
        StunAnimation.transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        input.Update();
        Animator.SetFloat("Horizontal", input.Direction.x);
        Animator.SetFloat("Vertical", input.Direction.y);
        Animator.SetBool("Moving", input.Direction.magnitude > 0.01f && !Stunned);
        //Animator.SetBool("Repair", IsBreaking);
        SpriteRenderer.flipX = input.Direction.x > 0;
        if (Stunned)
            return;
        Stamina += StaminaRecovery * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (IsBreaking || Stunned)
            return;
        if (input.IsRunning)
            Stamina -= StaminaDrain * Time.fixedDeltaTime;
        rb.MovePosition((Vector2)transform.position + input.Direction * ActualSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var pl = other.gameObject.GetComponent<Beaver>();
        if (pl != null)
            Physics2D.IgnoreCollision(pl.Collider2D, Collider2D);
    }

    public void Stun(float duration)
    {
        if (!Stunned)
            StartCoroutine(CoStun(duration));
    }

    IEnumerator CoStun(float duration)
    {
        Stunned = true;
        Stamina = 0;
        StunAnimation.transform.localScale = Vector3.one;
        StunAnimation.Play();
        yield return new WaitForSeconds(duration);
        StunAnimation.Stop();
        StunAnimation.transform.localScale = Vector3.zero;
        Stunned = false;
    }

}
