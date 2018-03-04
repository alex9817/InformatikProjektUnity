using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchadenBekommen : MonoBehaviour
{

    public int schaden = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
            collision.collider.SendMessage("ApplyDamage", schaden);
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
            collider.SendMessage("ApplyDamage", schaden);

    }
}
