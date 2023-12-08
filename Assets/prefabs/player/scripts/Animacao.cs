using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animacao : MonoBehaviour
{
    public Animator animator;
    public Controles controles;
    public Medidores medidores;
    void Start()
    {
        animator = transform.Find("sprite").GetComponent<Animator>();
        controles = GetComponent<Controles>();
        medidores = GetComponent<Medidores>();
    }

    // Update is called once per frame
    void Update(){
        bool parado = verificarEstadoParado();
        animator.SetBool("parado", parado);
        controles.parado = parado;
        
        bool correndo = verificarEstadoCorrendo();
        animator.SetBool("correndo", correndo);
        controles.correndo = correndo;
        controles.correndo = correndo;
        verificarEstadoNoChao();
    }




    public bool  verificarEstadoParado(){
        if(controles.direcao != 0){
            return false;
        }
            
        return true;
    }

    public bool verificarEstadoCorrendo()
    {
        if (controles.direcao == 0)
        {
            return false;
        }

        return true;
    }


    public void verificarEstadoNoChao(){
        animator.SetBool("noChao", medidores.noChao);
    }


    public void setPulando(bool pulando){
        animator.SetBool("pulando", pulando);
        controles.pulando = pulando;
    }


    public void setSegurandoParede(bool segurandoParede){
        animator.SetBool("segurandoParede", segurandoParede);
        controles.segurandoParede = segurandoParede;
    }


    public void setAtacar(string anim){
        animator.SetTrigger(anim);
        
    }


    public void setTrigger(string anim)
    {
        animator.SetTrigger(anim);
    }

}
