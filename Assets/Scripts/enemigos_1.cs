using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigos_1 : MonoBehaviour
{

    private Rigidbody2D CuerpoRig;

    public float MovHor = 0f;
    public float Velocid = 3f;

    public bool TocaPiso = true;
    public bool TocaPared = false;

    public LayerMask pisoLayer;
    public float frontGrndRayDist = 0.25f;//distancia del rayo.
    public float PisoCheckY = 0.52f;
    public float FrenteCheck = 0.51f;
    public Transform FrenteCheckReference;
    public float DistFrent = 0.001f;

    public int scoreGive = 50;
    public Transform enemySpriteTransform;

    private RaycastHit2D hit;

    // Start is called before the first frame update
    void Start()
    {
        CuerpoRig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Evitar caer en precipicio
        TocaPiso = (Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z),
            Vector3.down, frontGrndRayDist, pisoLayer));
        if (!TocaPiso)
            MovHor = MovHor * -1;
            
        //Choque con pared
        if (Physics2D.Raycast(FrenteCheckReference.position, new Vector3(MovHor, 0, 0), FrenteCheck, pisoLayer))
        {
            MovHor = MovHor * -1;
            enemySpriteTransform.localScale = (MovHor == 1) ? Vector3.one : new Vector3(-1,1,1);
        }

        //Choque con otro enemigo
        hit = Physics2D.Raycast(new Vector3(transform.position.x + MovHor * FrenteCheck, transform.position.y, transform.position.z),
            new Vector3(MovHor, 0, 0), DistFrent);

        if (hit != null)
            if (hit.transform != null)
                if (hit.transform.CompareTag("Enemy1"))
                    MovHor = MovHor * -1;
    }

    private void FixedUpdate()
    {
        CuerpoRig.velocity = new Vector2(MovHor * Velocid, CuerpoRig.velocity.y);

    }
}
