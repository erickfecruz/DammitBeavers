using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour { 

    private float timerFim;
    private float tempoDecorrido = 0f;

    private bool fim = false;

    // Update is called once per frame
    void Update() {
        tempoDecorrido += Time.deltaTime;
        if (tempoDecorrido >= timerFim)
            fim = false;
    }

    public void IniciarContagem(float cont) {
        tempoDecorrido = 0f;
        timerFim = cont;
    }

    public bool TimerEnd() {
        
        if (fim) {
            fim = false;
            return true;
        } else {
            return false;
        }
    }
}   
