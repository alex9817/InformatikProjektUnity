using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LebensKit : MonoBehaviour {

    public int lebensPunkte = 3;
    LebenSpieler lebenspieler;

	// Use this for initialization
	void Start () {
        lebenspieler = GameObject.FindGameObjectWithTag("Player").GetComponent<LebenSpieler>();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            lebenspieler.LebenHinzufügen(lebensPunkte);
            Destroy(gameObject);
        }
    }
}
