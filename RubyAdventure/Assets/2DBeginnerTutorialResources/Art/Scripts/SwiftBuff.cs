using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Potions/SwiftPotion")]
public class SwiftBuff : MonoBehaviour
{
    public float amount;

    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();

       // if (controller != null)
       // {
           // if (controller.speed < controller.maxSpeed)
         //   {
          //      controller.ChangeSpeed(3);
          //      Destroy(gameObject);

         //   }
       // }

    }
}
