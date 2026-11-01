using System.Collections.Generic;
using UnityEngine;

public class EvidenceBoard : MonoBehaviour
{
    public static EvidenceBoard Instance { get; private set; }

    [System.Serializable]
    public class Entry
    {
        public string title;
        public string description;
        public string truthId;
    }

    public KeyCode toggleKey = KeyCode.Tab;

    private List<Entry> entries = new List<Entry>();
    private bool isOpen = false;
    private Vector2 scroll;
    private FirstPersonController player;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        player = FindFirstObjectByType<FirstPersonController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey)) SetOpen(!isOpen);
    }

    void SetOpen(bool open)
    {
        isOpen = open;
        Cursor.lockState = open ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = open;
        if (player != null) player.enabled = !open;   // freeze movement and look while open
    }

    public void AddEntry(string title, string description, string truthId)
    {
        foreach (var e in entries) if (e.title == title) return;   // no duplicates
        entries.Add(new Entry { title = title, description = description, truthId = truthId });
    }

    void OnGUI()
    {
        if (!isOpen) return;

        float w = Screen.width * 0.6f;
        float h = Screen.height * 0.7f;
        Rect box = new Rect((Screen.width - w) / 2f, (Screen.height - h) / 2f, w, h);

        GUI.Box(box, "EVIDENCE BOARD  (Tab to close)");
        GUILayout.BeginArea(new Rect(box.x + 15, box.y + 30, box.width - 30, box.height - 45));
        scroll = GUILayout.BeginScrollView(scroll);

        if (entries.Count == 0) GUILayout.Label("Nothing collected yet.");

        foreach (var e in entries)
        {
            GUILayout.Label("<b>" + e.title + "</b>", new GUIStyle(GUI.skin.label) { richText = true });
            if (!string.IsNullOrEmpty(e.description)) GUILayout.Label(e.description);
            GUILayout.Space(8);
        }

        GUILayout.EndScrollView();
        GUILayout.EndArea();
    }
}