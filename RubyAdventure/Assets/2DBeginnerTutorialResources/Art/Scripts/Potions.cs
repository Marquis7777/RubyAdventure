using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potions : MonoBehaviour
{
    public float speedBoostDuration = 5.0f;

    public int maxSpeed = 6;
    private void OnTriggerEnter2D (Collider2D collider2D)
    {
         
            RubyController player = collider2D.GetComponent<RubyController>();
            if (player != null)
            {
                player.SpeedBoost = true;
                Destroy(gameObject);
            }
    }
}
