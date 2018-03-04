using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LebenSpieler : MonoBehaviour {

    //public float startLeben = 5;
    public int maxLebensPunkte = 17;

    public Image lebenGui;
    public Text endText;


    //private float leben;
    //private float maxLeben = 5;
    private int lebensPunkte;

    private Animator anim;
    private SpielerController spielerController;
    private bool Tot = false;
    private bool DamageAble = true;
	
    
    
    
    // Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        spielerController = GetComponent<SpielerController>();

        lebensPunkte = maxLebensPunkte;
        //leben = PlayerPrefs.GetFloat("Leben");
        //lebensPunkte = PlayerPrefs.GetInt("LebensPunkte");

        endText.text = "";
        UpdateView();
	}
	
	
    
    
    
    
    // Wird aufgerufen, wenn von irgendwoher Schaden auf den Spieler gemacht wird
	void ApplyDamage (int schaden) {
        if (DamageAble)
        {
            lebensPunkte -= schaden;

            //leben = Mathf.Max(0, leben);
            if (!Tot)
            {
                if (lebensPunkte == 0)
                {
                    Tot = true;
                    Sterben();
                }
                else
                {
                    SchadenBekommen();
                }

                DamageAble = false;
                Invoke("ResetDamageAble", 1);
            }
        }

	}


    public void LebenHinzufügen(int neueLebenspunkte)
    {
        //leben += extraLeben;

        lebensPunkte += neueLebenspunkte;
        UpdateView();
    }


    void ResetDamageAble()
    {
        DamageAble = true;
    }



    void Sterben()
    {
        anim.SetBool("Sterben", true);
        spielerController.enabled = false;
        lebensPunkte--;
        UpdateView();

        if (lebensPunkte <= 0)
        {
            endText.text = "Game Over";
            Invoke("SpielStarten", 3);
        }
        else
        {
            Invoke("Neustarten", 1);
        }
    }


    void SpielStarten()
    {
        Application.LoadLevel(0);
    }


    void Neustarten()
    {
        Tot = false;
        //leben = startLeben;
        anim.SetBool("Sterben", false);
        spielerController.enabled = true;

        if (!spielerController.spielerSchautKorrekt)
        {
            spielerController.SpielerUmdrehen ();
        }
        //Level neu genierieren und Spieler zurücksetzen
    }

    void SchadenBekommen()
    {
        anim.SetTrigger("Damage");
        UpdateView();
    }


    private void OnDestroy()
    {
        //PlayerPrefs.SetFloat("Leben", leben);
        PlayerPrefs.SetInt("LebensPunkte", lebensPunkte);
    }

    void UpdateView()
    {
        float hp = (lebensPunkte * 1f) / maxLebensPunkte;
        lebenGui.fillAmount = hp;
    }
}
