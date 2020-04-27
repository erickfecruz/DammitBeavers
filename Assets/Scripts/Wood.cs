using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour
{
    private GameObject barrege;

    private void Start()
    {
        StartCoroutine(Co());
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Barrage"))
        {
            barrege = other.gameObject;
        }
    }


    IEnumerator Co()
    {
        yield return new WaitForSeconds(0.2f);
        barrege.GetComponent<Barricada>().vidaAtual += 50;
        Destroy(this.gameObject);
    }
}
