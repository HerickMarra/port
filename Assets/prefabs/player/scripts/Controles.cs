using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controles : MonoBehaviour
{


    public bool parado,correndo,jump,dash,ataque, ataque2,podAtaque2 = true, pulando,segurandoParede;
    public float direcao;

    public Button btnAtacar, btnDash;

    void Start(){
        podAtaque2 = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void atacando(float temp) {
        if (!ataque){
            StartCoroutine(ataqueTemporizador(temp));
            StartCoroutine(podAtaque2Temporizador(temp - (temp * 0.6f)));
        }else{
            StartCoroutine(ataque2Temporizador(temp));
        }
    }
    IEnumerator ataqueTemporizador(float temp){
        ataque = true;
        //btnAtacar.SetActive(false);
        yield return new WaitForSeconds(temp);
        ataque = false;
    }

    IEnumerator podAtaque2Temporizador(float temp){
        btnAtacar.interactable = false;
        podAtaque2 = false;
        //btnAtacar.SetActive(false);
        yield return new WaitForSeconds(temp);
        podAtaque2 = true;
        btnAtacar.interactable = true;


    }

    IEnumerator ataque2Temporizador(float temp){
        ataque2 = true;
        btnAtacar.interactable = false;

        //btnAtacar.SetActive(false);
        yield return new WaitForSeconds(temp - (temp * 0.65f));
        ataque2 = false;
        btnAtacar.interactable = true;

    }
}
