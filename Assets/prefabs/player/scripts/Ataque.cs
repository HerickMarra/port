using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ataque : MonoBehaviour
{

    public GameObject ataquedano;
    public float tempoAtaque, tempoAtividade;
    void Start(){
        ataquedano.SetActive(false);
    }

    void Update(){
        
    }


    public void ataque(){ 
            StartCoroutine(ataque(tempoAtividade, ataquedano));
    }



    IEnumerator ataque(float temp, GameObject obj)
    {
        ataquedano.SetActive(false);
        ataquedano.SetActive(true);
        yield return new WaitForSeconds(temp);
        ataquedano.SetActive(false);
    }
}
