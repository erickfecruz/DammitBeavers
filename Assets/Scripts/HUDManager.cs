using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField]
    public RectTransform transformSeta;
    [SerializeField]
    public Slider aguaSlider;
    [SerializeField]
    float timeOfTravel = 500f; //time after object reach a target place 
    [SerializeField]
    float currentTime = 0; // actual floting time 
    float normalizedValue;
    [SerializeField]
    public float maxAgua = 150f;
    public float currentAgua = 0f;


    // Start is called before the first frame update
    void Start()
    {
        currentAgua = 0f;
        aguaSlider.value = calculaSliderAgua();
        //Por enquanto começa aqui no Start
        StartCoroutine(LerpSims()); //Pode ser deletado
    }

    IEnumerator LerpSims()
    {
        while (currentTime <= timeOfTravel)
        {
            currentTime += Time.deltaTime;
            normalizedValue = currentTime / timeOfTravel; // we normalize our time 

            transformSeta.anchoredPosition = Vector3.Lerp(transformSeta.pivot, new Vector2(790, 0), normalizedValue);
            yield return null;
        }

        if (currentTime >= timeOfTravel)
        {
            SceneManager.LoadScene("Win");
        }
    }

    public float calculaSliderAgua()
    {
        return currentAgua / maxAgua;
    }

    private void Update()
    {
        aguaSlider.value = calculaSliderAgua();
        if (currentAgua >= maxAgua)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void IniciaContagem()
    {
        StartCoroutine(LerpSims());
    }

    //Ainda não há nenhuma chamada para a água aumentar
    public void aumentaQuantidadeAgua(float quantidade)
    {
        currentAgua += quantidade;
    }
}
