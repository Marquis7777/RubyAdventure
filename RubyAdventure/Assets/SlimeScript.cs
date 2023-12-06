using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScript : MonoBehaviour

{
    public Transform player; // Reference to the player's Transform
    public float moveSpeed = 3f; // Speed at which the enemy follows the player

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-3);
        }
    }
    void Update()
    {
        if (player != null)
        {
            // Calculate the direction from the enemy to the player
            Vector3 direction = player.position - transform.position;

            // Normalize the direction to get a unit vector
            direction.Normalize();

            // Move the enemy towards the player
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }
}
