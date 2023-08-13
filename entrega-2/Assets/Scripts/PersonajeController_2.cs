using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeController_2 : MonoBehaviour
{
   public float velMovement = 5f; // Velocidad de movimiento del personaje
    public float fuerzaJump = 7f; //Fuerza dle salto dle personaje 
    public bool enElsuelo = false; //Indicador si el personaje est en el suelo
    private bool mirandoDerecha = true;//Indica el sentido donde mira el personaje
    private Transform controladorSuelo;
    private Vector3 dimensionesCaja;
    public bool sePuedeMover = true;
    private float velActual;
    private float jumpActual;
    private float velocidadEscalar = 5f;
    private BoxCollider2D boxCollider;
    private float gravedadInicial;
    private bool escalando;
    private Vector2 input;


    public Vector2 velocidadRebote;


    private Rigidbody2D rb;
    private Animator animator;

    void Awake()
    {
        sePuedeMover = true;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        gravedadInicial = rb.gravityScale;

    }


    void FixedUpdate()

    {

        if(sePuedeMover)
        {
            //movimiento horizonal
            float movimientoH = Input.GetAxis("Horizontal");
            input.x = movimientoH;
            input.y = Input.GetAxis("Vertical");

            //sentido personaje
            if(movimientoH > 0 && !mirandoDerecha){
                Girar();
            }

            else if(movimientoH < 0 && mirandoDerecha){
                Girar();
            }


            rb.velocity = new Vector2(movimientoH * velMovement, rb.velocity.y);

            

            animator.SetFloat("Horizontal", Mathf.Abs(movimientoH));
            animator.SetBool("enSuelo",enElsuelo);
            animator.SetFloat("VelocidadY", rb.velocity.y);
            animator.SetBool("estaEscalando", escalando);
            Escalar();
            //Salto
            if (Input.GetKey(KeyCode.UpArrow) && enElsuelo &&  escalando == false)
            {
                //rb.AddForce(new Vector2(0f, fuerzaJump));
                rb.velocity = new Vector2(rb.velocity.x, +fuerzaJump);
                enElsuelo = false;
                //AudioManager.Instance.PlaySFX("jump");
            }



        
        }
         
    }       

    private void Escalar(){
         Debug.Log(input.y);
       if ((boxCollider.IsTouchingLayers(LayerMask.GetMask("escalera"))))
       { 
        Debug.Log("toco la escalera");
        rb.velocity = new Vector2(rb.velocity.x, input.y * velocidadEscalar);
        rb.gravityScale = 0;
        escalando = true;
        }
        else {
             rb.gravityScale = gravedadInicial;
             escalando = false;
             }

        
}

        private void Girar()
        {
            mirandoDerecha = !mirandoDerecha;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
            Escalar();
        }

        public void Rebote(Vector2 puntoGolpe, float direccion){
            rb.velocity = new Vector2(velocidadRebote.x * puntoGolpe.x * direccion, velocidadRebote.y);
        }

        public void superVelocidad(float velM, float tiempoPoder){
            StartCoroutine(velocidad(velM,tiempoPoder));
        }

        public void superSalto(float fuerzaJ, float tiempoPoder){
            StartCoroutine(salto(fuerzaJ,tiempoPoder));
        }

        private IEnumerator velocidad(float velM, float tiempoPoder){
            velActual = velMovement;
            velMovement = velM;
            yield return new WaitForSeconds(tiempoPoder);
            velMovement = velActual;
        }

        private IEnumerator salto(float fuerzaJ, float tiempoPoder){
            jumpActual = fuerzaJump;
            fuerzaJump = fuerzaJ;
            yield return new WaitForSeconds(tiempoPoder);
            fuerzaJump = jumpActual;
        }
}
