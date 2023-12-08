using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    GameObject player;
    public float velocidadeMovimento;
    void Start(){
        player = GameObject.FindWithTag("Player"); 
    }

    void Update(){
        Movimentacao();
    }
    


    void Movimentacao() {
        Vector3 pos = new Vector3 (player.transform.position.x, player.transform.position.y, transform.position.z);
        Vector3 novaPosicao = Vector3.Lerp(transform.position, pos, velocidadeMovimento * Time.deltaTime);
        transform.position = novaPosicao;
    }
}
