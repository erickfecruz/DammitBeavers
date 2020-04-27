using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricada : MonoBehaviour
{
    [SerializeField]
    public int vidaInicial = 100;
    [SerializeField]
    public int vidaAtual;

    GameObject rachadura;
    GameObject vazamento;

    public float waterMedio = 0.04f;
    public float waterGrande = 0.1f;

    GameObject HUDControler;

    // Start is called before the first frame update
    void Start() {
        vidaAtual = vidaInicial;

        rachadura = transform.GetChild(0).gameObject;
        vazamento = transform.GetChild(1).gameObject;

        HUDControler = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    void Update() {
        if (vidaAtual < 0)
            vidaAtual = 0;

        if (vidaAtual > 50) {
            rachadura.SetActive(false);
            vazamento.SetActive(false);
        } else if (vidaAtual > 0) {
            rachadura.SetActive(true);
            vazamento.SetActive(false);
            HUDControler.GetComponent<HUDManager>().currentAgua = HUDControler.GetComponent<HUDManager>().currentAgua + (waterMedio * Time.deltaTime);
        } else {
            rachadura.SetActive(true);
            vazamento.SetActive(true);
            HUDControler.GetComponent<HUDManager>().currentAgua = HUDControler.GetComponent<HUDManager>().currentAgua + (waterGrande * Time.deltaTime);
        }

        if (vidaAtual > vidaInicial)
            vidaAtual = vidaInicial;
    }
}
