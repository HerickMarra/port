using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movimentacao : MonoBehaviour
{

    Controles controles;
    Medidores medidores;
    Animacao animacao;
    Ataque ataque;
    public Rigidbody2D rb;
    public Vector2 velMovimento;
    public float forcaDoPulo, forcaDoPuloParede;

    void Start(){
        controles = gameObject.GetComponent<Controles>();
        gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
        rb = gameObject.GetComponent<Rigidbody2D>();
        medidores = gameObject.GetComponent<Medidores>();
        animacao = gameObject.GetComponent<Animacao>();
        ataque = gameObject.GetComponent<Ataque>();
    }

    void Update(){
        
    }

    private void FixedUpdate(){
        movimentar();
    }



    public void movimentar(){
        if (controles.direcao != 0 && !controles.segurandoParede && !controles.ataque && !controles.dash){
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.velocity = new Vector2(velMovimento.x * controles.direcao, rb.velocity.y);
        }

        if (controles.segurandoParede){
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        }else{rb.constraints = RigidbodyConstraints2D.FreezeRotation;}
    }
    public void Andar(float direcao){
       // if (medidores.noChao){
       // }else{ controles.direcao = 0; }
        controles.direcao = direcao;

        if(direcao > 0) { gameObject.transform.eulerAngles = new Vector3 (0, 0, 0);}
        if(direcao < 0) { gameObject.transform.eulerAngles = new Vector3 (0, 180, 0);}
    }


    public void pular(){

        if (medidores.noChao && !controles.segurandoParede){
            rb.AddForce(Vector3.up * forcaDoPulo, ForceMode2D.Impulse);
            animacao.setPulando(true);
            StartCoroutine(puloTemporizador());
        }

        if (controles.segurandoParede){
            controles.direcao = 0;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
            rb.AddForce((transform.rotation.y >= 0 ? Vector2.left : Vector2.right) * forcaDoPuloParede, ForceMode2D.Impulse);
            rb.AddForce(Vector3.up * forcaDoPulo, ForceMode2D.Impulse);
            animacao.setPulando(true);
            StartCoroutine(puloTemporizador());
        }

        if(controles.ataque){
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        }
    }
    IEnumerator puloTemporizador(){
        medidores.podeSSetPulo = false;
        animacao.animator.SetBool("animPulando", true);
        yield return new WaitForSeconds(0.1f);
        medidores.podeSSetPulo = true;
        animacao.animator.SetBool("animPulando", false);
    }




    public float tempoAtaqueParado, tempoAtaqueCorrendo;
    public float forcaAtaqueParado, forcaAtaqueCorrendo;
    public void atacar(){
        if (controles.ataque2 || !controles.podAtaque2){
            return;
        }


        if (medidores.noChao){
            if (controles.parado & !controles.dash) {
                if (!controles.ataque) {
                    animacao.setAtacar("atacar");
                }else{
                    animacao.setAtacar("atacar2");
                }
                float forca = controles.ataque ? forcaAtaqueParado / 2 : forcaAtaqueParado;
                rb.AddForce((transform.rotation.y >= 0 ? Vector2.right : Vector2.left) * forca, ForceMode2D.Impulse);
                controles.atacando(tempoAtaqueParado);
                ataque.ataque();
            }
            if (controles.correndo | controles.dash) {
                animacao.setAtacar("atacarDash");
                controles.atacando(tempoAtaqueCorrendo);
                rb.AddForce((transform.rotation.y >= 0 ? Vector2.right : Vector2.left) * forcaAtaqueCorrendo, ForceMode2D.Impulse);
                controles.direcao = 0;
                ataque.ataque();
            }

        }

    }


    public float forcaDash,tempoDash;
    public void dash(){
        if (!controles.ataque) {
            controles.dash = true;
            rb.AddForce((transform.rotation.y >= 0 ? Vector2.right : Vector2.left) * forcaDash, ForceMode2D.Impulse);
            StartCoroutine(dashTemporizador());
        }
        
    }


    IEnumerator dashTemporizador(){
        controles.btnDash.interactable = false;
        animacao.setTrigger("dash");
        yield return new WaitForSeconds(0.5f);
        controles.dash = false;
        animacao.animator.SetBool("animPulando", false);

        yield return new WaitForSeconds(tempoDash);
        controles.btnDash.interactable = true;
    }

}
