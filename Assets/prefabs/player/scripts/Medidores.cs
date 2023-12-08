using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Medidores : MonoBehaviour
{
    Rigidbody2D rb;
    Controles controles;
    Animacao animacao;
    public bool noChao = false;
    void Start(){
        rb = gameObject.GetComponent<Rigidbody2D>();
        controles = gameObject.GetComponent<Controles>();
        animacao = gameObject.GetComponent<Animacao>();
    }
    // Update is called once per frame
    void Update()
    {

        noChao = VeirificadorChao(
        rayCast2d(transform.position, Vector2.down, Color.red, comprimentoDoRaio),
        rayCast2d(new Vector3(transform.position.x + 0.2f, transform.position.y, transform.position.z), Vector2.down, Color.red, comprimentoDoRaio),
        rayCast2d(new Vector3(transform.position.x - 0.2f, transform.position.y, transform.position.z), Vector2.down, Color.red, comprimentoDoRaio)
        );

        VerificarParede();
        
    }


    public float comprimentoDoRaio, comprimentoRaioParede;
    private RaycastHit2D rayCast2d(Vector3 pos, Vector2 dir, Color cor,float tamanhoRaio){
        RaycastHit2D hit = Physics2D.Raycast(pos, dir, tamanhoRaio, ~LayerMask.GetMask("Player"));

        // Verifica se o raio atingiu algo

        // Desenha a linha do raio no Editor para visualização
        Debug.DrawRay(pos, dir * tamanhoRaio, cor);


        return hit;
    }

    public bool podeSSetPulo = false;
    private bool VeirificadorChao(RaycastHit2D hit0, RaycastHit2D hit1, RaycastHit2D hit2){
        bool chao = (hit0.collider != null || hit1.collider != null || hit2.collider != null);
        if (chao && controles.pulando && podeSSetPulo)
        {
            animacao.setPulando(false);
        }
        return chao;
    }

   void VerificarParede(){
        RaycastHit2D hitParede = rayCast2d(transform.position, transform.rotation.y >= 0 ? Vector2.right : Vector2.left, Color.blue, comprimentoRaioParede);

        if(hitParede.collider != null){
            if (hitParede.collider.tag == "paredeEscalavel"){
                if(controles.direcao != 0) {
                    animacao.setSegurandoParede(true);
                }else{
                    transform.Translate((transform.rotation.y >= 0 ? Vector2.right : Vector2.left)*0.01f);
                    animacao.setSegurandoParede(false);
                }
            }
            else {
                transform.Translate((transform.rotation.y >= 0 ? Vector2.left : Vector2.right)*0.04f);
                animacao.setSegurandoParede(false);
            }
        }else { animacao.setSegurandoParede(false); }

    }
}
