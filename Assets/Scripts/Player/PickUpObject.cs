using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpObject : MonoBehaviour
{
    [SerializeField]
    private bool pickUpObject = false;
    [SerializeField]
    private bool holdButton;
    [SerializeField]
    private bool holdButtonPickUp;

    private GameObject objectHold; //Madeiras que irá pegar
    private GameObject getInstantiete; //Para chamar função que vai instanciar as madeiras gerados para pegar
    private GameObject sourceDestroy; //Arvore
    private Beaver beaver;
    public UserInput input => beaver.input;

    public GameObject barHold;
    public bool player2;

    public GameObject fumaca;
    private Animator animator;
    private void Awake()
    {
        beaver = GetComponent<Beaver>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (player2)
            barHold = GameObject.FindGameObjectWithTag("BarPlayer2");
    }
    void Update()
    {
        if (input.IsBreaking)
        {
            if (holdButton)
                Action(true);
            else
                Action(false);

           
        }
        else
        {
            Action(false);
        }


        if (input.OnGrab)
        {
            if (holdButtonPickUp)
            {
                objectHold.transform.SetParent(this.transform);
                objectHold.transform.position = this.transform.transform.position;
            }
            else
            {
                objectHold.transform.SetParent(null);
                objectHold.GetComponent<Wood>().enabled = true;
                animator.SetBool("Repair", true);
            }
        } 
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Action"))
        {
            holdButton = true; //Permite que quando apertar a tecla execute o que deve ser feito
            getInstantiete = coll.gameObject;
            sourceDestroy = coll.gameObject;
            //getInstantiete.GetComponent<Arvore>().enabled = true;
        }

        if (coll.gameObject.CompareTag("ObjectHold"))
        {
            holdButtonPickUp = true; //Permite que quando apertar a tecla execute o que deve ser feito
            objectHold = coll.gameObject;
        }

        if (coll.gameObject.CompareTag("DownObj"))
        {
            holdButtonPickUp = false;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Action"))
        {
            holdButton = false;
            // if (getInstantiete != null)
            // getInstantiete.GetComponent<Arvore>().enabled = false;
            sourceDestroy = null;
        }

        if (coll.gameObject.CompareTag("ObjectHold"))
        {
            holdButtonPickUp = false; //Permite que quando apertar a tecla execute o que deve ser feito

        }

        if (coll.gameObject.CompareTag("DownObj"))
        {
            animator.SetBool("Repair", false);
        }

    }

    //Ação de gerar o objeto
    void Action(bool hold) {
        if (getInstantiete != null)
            getInstantiete.GetComponent<Arvore>().prepararMaterial(hold);
        
        fumaca.SetActive(hold);
    }

    public void Destroytree()
    {
        holdButton = false;
        //sourceDestroy.SetActive(false);
        Destroy(sourceDestroy);
        sourceDestroy = null;
    }

  /*  IEnumerator Co()
    {
        yield return new WaitForSeconds(0.2f);
        fumaca.SetActive(false);
    }*/
}
