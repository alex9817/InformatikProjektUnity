using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpielerController : MonoBehaviour {

    /// <summary>
    /// Name des Inputs für die erste Taste zum Schießen
    /// </summary>
    public const string INPUT_SCHIESSEN_1 = "Schiessen1";

    /// <summary>
    /// Name des Inputs zum Springen
    /// </summary>
    public const string INPUT_SPRINGEN = "Springen";

    public float maxSpeed = 4;
    public float jumpForce = 550;
    public Transform bodenCheck;
    public LayerMask lmBoden;

    /// <summary>
    /// Speichert den Status, ob der Spieler in die Richtung schaut, in die er sich bewegt.
    /// </summary>
    [HideInInspector]
    public bool spielerSchautKorrekt = true;
    public GameObject laserVorlage;
    public Transform spawnPoint;
    public float laserGeschwindigkeit = 500;


    private Rigidbody2D rb2d;
    private Animator anim;

    private bool spielerIstAufBoden = false;
    private bool spielerSollSpringen = false;
    private bool spielerSollSchiessen = false;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown(INPUT_SPRINGEN) && spielerIstAufBoden)
        {
            spielerSollSpringen = true;
        }

        if (Input.GetButtonDown(INPUT_SCHIESSEN_1) && !spielerSollSchiessen)
        {
            spielerSollSchiessen = true;
        }
	}


    private void FixedUpdate()
    {
        float hor = Input.GetAxis("Horizontal");
        anim.SetFloat("Geschwindigkeit", Mathf.Abs(hor));
        rb2d.velocity = new Vector2(hor * maxSpeed, rb2d.velocity.y);
        spielerIstAufBoden = Physics2D.OverlapCircle(bodenCheck.position, 0.15F, lmBoden);

        anim.SetBool("Bodenkontakt", spielerIstAufBoden);

        if ((hor > 0 && !spielerSchautKorrekt) || (hor < 0 && spielerSchautKorrekt))
        {
            SpielerUmdrehen();
        }
            
        if (spielerSollSpringen)
        {
            rb2d.AddForce(new Vector2(0, jumpForce));
            spielerSollSpringen = false;
        }

        if (spielerSollSchiessen)
        {
            anim.SetTrigger("Attack");
            GameObject Laser = (GameObject) Instantiate(laserVorlage, spawnPoint.position, Quaternion.identity);

            if (spielerSchautKorrekt)
                Laser.GetComponent<Rigidbody2D>().AddForce(Vector3.right * laserGeschwindigkeit);
            else
                Laser.GetComponent<Rigidbody2D>().AddForce(Vector3.left * laserGeschwindigkeit);

            spielerSollSchiessen = false;
        }
    }
    
    public void SpielerUmdrehen() {
        spielerSchautKorrekt = !spielerSchautKorrekt;
        Vector3 myScale = transform.localScale;
        myScale.x *= -1;
        transform.localScale = myScale;
    }
}
