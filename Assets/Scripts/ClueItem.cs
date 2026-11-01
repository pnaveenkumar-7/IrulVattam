using UnityEngine;

public class ClueItem : MonoBehaviour
{
    public string clueName = "Clue";
    [TextArea] public string description = "";
    public string truthId = "";      // e.g. "T1". Leave empty for normal clues.
    public bool collected = false;

    public void Interact()
    {
        if (collected) return;
        collected = true;
        Debug.Log("Inspected: " + clueName + " - " + description);

        if (GameState.Instance != null)
            GameState.Instance.AddTruth(truthId);
    }
}