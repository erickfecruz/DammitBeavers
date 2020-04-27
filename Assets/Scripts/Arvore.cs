using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arvore : MonoBehaviour
{
    [SerializeField]
    public int quantidadeMaterialInicial;
    [SerializeField]
    public int quantidadeMaterialAtual;
    [SerializeField]
    public GameObject material;
    [SerializeField]
    public float tempoDePreparo = 10f;
    [SerializeField]
    public float velocidade = 0f;
    [SerializeField]
    public bool preparandoMaterial;
    [SerializeField]
    public float tempoPreparando;

    [SerializeField]
    private GameObject playerPickUp;
    // Start is called before the first frame update
    void Start()
    {
        quantidadeMaterialAtual = quantidadeMaterialInicial;
    }

    // Update is called once per frame
    void Update()
    {
        if (preparandoMaterial)
        {
            tempoPreparando += Time.deltaTime * velocidade;
            playerPickUp.GetComponent<PickUpObject>().barHold.GetComponent<Slider>().value = tempoPreparando;
        }

        if(tempoPreparando >= tempoDePreparo)
        {
            Instantiate(material, new Vector3(transform.position.x, transform.position.y, material.transform.position.z), new Quaternion());
            tempoPreparando = 0f;
            quantidadeMaterialAtual--;
            playerPickUp.GetComponent<PickUpObject>().barHold.GetComponent<Slider>().value = tempoPreparando;
        }

        if (quantidadeMaterialAtual <= 0)
        {
            preparandoMaterial = false;
            playerPickUp.GetComponent<PickUpObject>().Destroytree();
            //Destroy(this);
        }
    }

    public void prepararMaterial(bool hold)
    {
        preparandoMaterial = hold;

        if(!hold)
        {
            tempoPreparando = 0f;
            playerPickUp.GetComponent<PickUpObject>().barHold.GetComponent<Slider>().value = tempoPreparando;
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;

        if (tag.CompareTo("PlayerP") == 0)
        {
            velocidade = 1f;
            playerPickUp = collision.gameObject;
        }
        else if (tag.CompareTo("PlayerM") == 0)
        {
            velocidade = 1.5f;
            playerPickUp = collision.gameObject;
        }
        else if (tag.CompareTo("PlayerG") == 0)
        {
            velocidade = 3f;
            playerPickUp = collision.gameObject;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;

        if (tag.CompareTo("PlayerP") == 0)
        {
            velocidade = 1f;
        }
        else if (tag.CompareTo("PlayerM") == 0)
        {
            velocidade = 1.5f;
        }
        else if (tag.CompareTo("PlayerG") == 0)
        {
            velocidade = 3f;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        preparandoMaterial = false;
    }

}