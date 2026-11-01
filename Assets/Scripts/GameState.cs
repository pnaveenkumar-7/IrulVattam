using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }

    public enum Level { Low, Medium, High }

    private HashSet<string> truthFound = new HashSet<string>();
    public int Truth => truthFound.Count;

    public int trustPoints = 0;     // goes up/down with dialogue choices
    public int echoUses = 0;

    public bool showDebug = true;

    public Level Trust => trustPoints >= 3 ? Level.High : trustPoints >= 1 ? Level.Medium : Level.Low;
    public Level EchoExposure => echoUses >= 5 ? Level.High : Level.Low;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        // Test keys: remove or disable before release
        if (Input.GetKeyDown(KeyCode.F1)) ChangeTrust(1);
        if (Input.GetKeyDown(KeyCode.F2)) ChangeTrust(-1);
        if (Input.GetKeyDown(KeyCode.F3)) UseEcho();
        if (Input.GetKeyDown(KeyCode.F4)) EndingScreen.Show();
    }

    public bool Has(string id) => truthFound.Contains(id);

    public void AddTruth(string id)
    {
        if (string.IsNullOrEmpty(id) || !truthFound.Add(id)) return;
        Debug.Log("Truth " + id + " found (total " + Truth + ")");
    }

    public void ChangeTrust(int amount) { trustPoints += amount; }
    public void UseEcho() { echoUses++; }

    // Ending checks from the design doc
    public bool CanGetSilence()
    {
        return Truth >= 5 && Has("T5") && Has("T7") && Trust != Level.Low;
    }

    public bool CanGetUnremembered()
    {
        return Truth >= 6 && Has("T6") && EchoExposure == Level.High && Trust == Level.High;
    }

    public bool CanGetNewCircle()
    {
        return true; // always available as the fallback
    }

    void OnGUI()
    {
        if (!showDebug) return;
        GUI.Label(new Rect(20, 50, 400, 25), "Truth: " + Truth + "/7   Trust: " + Trust + "   Echo: " + EchoExposure);
        GUI.Label(new Rect(20, 75, 400, 25), "Silence: " + CanGetSilence() + "   Unremembered: " + CanGetUnremembered());
    }
}