using UnityEngine;

public class EndingScreen : MonoBehaviour
{
    private static EndingScreen instance;
    private bool visible = false;
    private string result = "";

    void Awake() { instance = this; }

    public static void Show()
    {
        if (instance == null) return;
        instance.visible = true;
        instance.result = "";
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void OnGUI()
    {
        if (!visible) return;
        GameState gs = GameState.Instance;
        if (gs == null) return;

        float w = 520f, h = 320f;
        Rect box = new Rect((Screen.width - w) / 2f, (Screen.height - h) / 2f, w, h);
        GUI.Box(box, "FINAL DECISION");
        GUILayout.BeginArea(new Rect(box.x + 20, box.y + 40, w - 40, h - 60));

        DrawOption("The Silence", gs.CanGetSilence());
        DrawOption("The New Circle", gs.CanGetNewCircle());
        DrawOption("The Unremembered", gs.CanGetUnremembered());

        GUILayout.Space(15);
        if (result != "") GUILayout.Label(result);
        if (GUILayout.Button("Close"))
        {
            visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        GUILayout.EndArea();
    }

    void DrawOption(string name, bool unlocked)
    {
        GUI.enabled = unlocked;
        string label = unlocked ? name : name + "  (locked)";
        if (GUILayout.Button(label, GUILayout.Height(40)))
            result = "Ending chosen: " + name;
        GUI.enabled = true;
        if (!unlocked) GUILayout.Label("Arun doesn't understand how the mechanism works.");
    }
}