using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public string keyId = "BasementKey";

    public void Interact()
    {
        if (Inventory.Instance != null) Inventory.Instance.AddItem(keyId);
        Destroy(gameObject);
    }
}