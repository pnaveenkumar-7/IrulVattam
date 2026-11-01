using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }
    private HashSet<string> items = new HashSet<string>();

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void AddItem(string id)
    {
        if (items.Add(id)) Debug.Log("Picked up: " + id);
    }

    public bool HasItem(string id) => items.Contains(id);
}