using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isLocked = false;
    public string requiredKeyId = "";      // e.g. "BasementKey". Leave empty if it needs no key.
    public float openAngle = 90f;
    public float speed = 3f;

    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        closedRotation = transform.localRotation;
        openRotation = closedRotation * Quaternion.Euler(0f, openAngle, 0f);
    }

    void Update()
    {
        Quaternion target = isOpen ? openRotation : closedRotation;
        transform.localRotation = Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * speed);
    }

    public void Interact()
    {
        if (isLocked)
        {
            if (!string.IsNullOrEmpty(requiredKeyId) && Inventory.Instance != null && Inventory.Instance.HasItem(requiredKeyId))
            {
                isLocked = false;
                Debug.Log("Unlocked with " + requiredKeyId);
            }
            else
            {
                Debug.Log("The door is locked.");
                return;
            }
        }
        isOpen = !isOpen;
    }

    public void Unlock() { isLocked = false; }
    public void Lock() { isLocked = true; isOpen = false; }
}