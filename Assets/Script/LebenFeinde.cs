using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LebenFeinde : MonoBehaviour
{


    public float leben = 1;
    void ApplyDamage(float schaden)
    {
        leben -= schaden;
        if (leben <= 0)
            Destroy(gameObject);
    }
}
