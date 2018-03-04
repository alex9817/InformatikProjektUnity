using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSchaden : MonoBehaviour {

    public float Schaden = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            collision.SendMessage("ApplyDamage", Schaden, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }
    }

}
