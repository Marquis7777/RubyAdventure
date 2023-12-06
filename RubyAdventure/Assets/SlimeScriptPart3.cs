using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScriptPart3 : MonoBehaviour

{ void OnCollisionEnter2D(Collision2D other)
    {
    RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-3);
        }
    }
}