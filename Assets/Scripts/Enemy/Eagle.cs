using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(SpriteRenderer))]
public class Eagle : MonoBehaviour
{
    public Sprite Sprite1;
    public Sprite Sprite2;

    [SerializeField]
    private Transform[] routes;

    private int nextRoute;
    private float tParam;

    private Vector2 eaglePosition;
    private float speedModifier;

    private bool coroutineAllowed;

    public SpriteRenderer SpriteRenderer { get; private set; }

    public AudioSource AudioSource { get; private set; }

    void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        AudioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        nextRoute = 0;
        tParam = 0f;
        speedModifier = 0.7f;
        coroutineAllowed = true;
    }

    void Update()
    {
        if (coroutineAllowed)
            StartCoroutine(GoByTheRoute(nextRoute));
        switch (nextRoute)
        {
            case 0:
            case 1:
                SpriteRenderer.sprite = Sprite1;
                SpriteRenderer.flipX = nextRoute == 1;
                break;
            case 2:
            case 3:
                SpriteRenderer.sprite = Sprite2;
                SpriteRenderer.flipX = nextRoute == 3;
                break;
        }
    }

    private IEnumerator GoByTheRoute(int routNumber)
    {
        coroutineAllowed = false;
        Vector2 p0 = routes[routNumber].GetChild(0).position;
        Vector2 p1 = routes[routNumber].GetChild(1).position;
        Vector2 p2 = routes[routNumber].GetChild(2).position;
        Vector2 p3 = routes[routNumber].GetChild(3).position;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;

            eaglePosition = Mathf.Pow(1 - tParam, 3) * p0 +
                3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                Mathf.Pow(tParam, 3) * p3;

            transform.position = eaglePosition;
            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;
        nextRoute = DrawnNextPosition(nextRoute);
        
        if (nextRoute == 2 || nextRoute == 3)
            AudioSource.Play();

        coroutineAllowed = true;
    }

    // Next Pos Index:
    // 1 = Direita para Esquerda (Por Cima)
    // 2 = Esquerda para Direita (Por Cima)
    // 3 = Direita para Esquerda (Por Baixo)
    // 4 = Esquerda para Direita (Por Baixo)
    int DrawnNextPosition(int actualPos)
    {
        int value = Random.Range(0, 10);
        // Left Side
        if (actualPos == 0 || actualPos == 2)
            return value >= 8 ? 3 : 1;
        return value >= 8 ? 2 : 0;
    }

    /// <summary>
    /// On Collision Enter with Player, Reduce Stamina to 0
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        var pl = collision.GetComponent<Beaver>();
        if (pl != null)
            pl.Stun(1.5f);
    }
}
