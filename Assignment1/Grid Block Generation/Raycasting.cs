// GridManager.cs (continued)
using UnityEngine.UI;

public class GridManager : MonoBehaviour {
    public Text hoverText;  // Assign a UI Text in the Inspector

    void Update() {
        // Cast a ray from the camera through the mouse position:contentReference[oaicite:5]{index=5}.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit)) {
            // Check if we hit a Tile
            Tile tile = hit.transform.GetComponent<Tile>();
            if (tile != null) {
                // Display the tile's grid coordinates in the UI Text:contentReference[oaicite:6]{index=6}.
                hoverText.text = $"Tile: ({tile.x}, {tile.y})";
            }
        }
    }
}
