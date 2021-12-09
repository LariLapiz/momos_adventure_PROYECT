using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMomo : MonoBehaviour
{
    public static PlayerMomo objeto;

    public int vidas = 3;
    public bool SobrePiso = false;//isGrounded
    public bool moviendo = false;//isMoving
    public bool inmune = false;


    //Datos flotantes  del moviento del personaje
    public float velocidad = 5f;
    public float FuersaSalto = 3f;
    public float movHorizon;

    // Variables de Inmunidad
    public float tiempoInmuniCnt = 0f;
    public float tiempoInmunidad = 0.05f;

    public LayerMask PisoLayer;
    public Vector3 pisoOFSED;
    public float radio = 0.3f;
    public float pisoRadioDist = 0.5f;

    private Rigidbody2D rigidBodyMomo;
    private Animator animMomo;
    private SpriteRenderer sprMomo;

    private void Awake()
    {
        objeto = this;
    }

    // Start is called before the first frame update
     void Start()
     {
        rigidBodyMomo = GetComponent<Rigidbody2D>();
        //deberia de ser <Animator>
        animMomo = GetComponent<Animator>();
        sprMomo = GetComponent<SpriteRenderer>();
     }

     //Para detectar movimiento y caminata
     void Update()
     {
        movHorizon = Input.GetAxisRaw("Horizontal");
        moviendo = (movHorizon != 0f);
        SobrePiso = Physics2D.CircleCast(transform.position + pisoOFSED, radio, Vector3.down, pisoRadioDist, PisoLayer);
        if (Input.GetKeyDown(KeyCode.Space))
            saltar();

        animMomo.SetBool("moviendo", moviendo);
        animMomo.SetBool("SobrePiso", SobrePiso);
        voltear(movHorizon);

     }

    private void FixedUpdate()
    {
        rigidBodyMomo.velocity = new Vector2(movHorizon * velocidad, rigidBodyMomo.velocity.y);
    }

    //para detectar salto
    public void saltar()
    {
        if (!SobrePiso) return;

        rigidBodyMomo.velocity = Vector2.up * FuersaSalto;
    }

    private void voltear(float _xValue)
    {
        Vector3 laEscala = transform.localScale;

        if (_xValue < 0)
            laEscala.x = Mathf.Abs(laEscala.x) * -1;
        else
            if (_xValue > 0)
            laEscala.x = Mathf.Abs(laEscala.x);
        transform.localScale = laEscala;

        //esta función se ejecutara en  Update

    }


    private void OnDestroy()
    {
        objeto = null; 
    }



}
