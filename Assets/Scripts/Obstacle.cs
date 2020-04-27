using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Obstacle : MonoBehaviour
{
    public Barrage Target;

    public int Damage = 5;

    public float Velocity = 10;

    public GameObject Explosion;

    public Collider2D Collider2D { get; private set; }

    void Awake()
    {
        Collider2D = GetComponent<Collider2D>();
    }

    private void Start()
    {
        StartCoroutine(Co());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        /* if (!Target.gameObject.Equals(other.gameObject))
         {
             Physics2D.IgnoreCollision(Collider2D, other.collider);
             return;
         }
         Target.Health -= Damage;
         if (Explosion != null)
             Instantiate(Explosion, transform.position, Quaternion.identity);
         Destroy(gameObject);*/

        if (other.gameObject.CompareTag("Barrage"))
        {
            other.gameObject.GetComponent<Barricada>().vidaAtual = other.gameObject.GetComponent<Barricada>().vidaAtual - (int)Damage;
            //Target.Health -= Damage;
            Destroy(gameObject);
        }
    }

    IEnumerator Co() {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }


}