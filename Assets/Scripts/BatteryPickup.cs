   using UnityEngine;

   public class BatteryPickup : MonoBehaviour
   {
       public float amount = 40f;

       public void Interact()
       {
           Flashlight f = FindFirstObjectByType<Flashlight>();
           if (f != null) f.AddBattery(amount);
           Destroy(gameObject);
       }
   }