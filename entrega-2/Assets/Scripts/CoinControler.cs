using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinControler : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D collision)
   {
    Debug.Log("moneda");
    gameObject.SetActive(false);

    if(SceneManager.GetActiveScene().name == "Nivel1" ){
        SceneManager.LoadScene("Nivel2");
    } else {
        SceneManager.LoadScene("Nivel1");
    }
   }
}