using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuerteCaida : MonoBehaviour
{
    private Vida vida;
    public float nivelPiso = -6f; 
    //public float cantidadDano;
    // Start is called before the first frame update
    void Start()
    {
        vida = GetComponent<Vida>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < nivelPiso){
            vida.TomarVida(vida.maximoVida);
        } 
    }
}
