// GridManager.cs
using UnityEngine;

public class GridManager : MonoBehaviour {
    public GameObject tilePrefab;            // Assign a cube prefab with the Tile script
    public static Tile[,] gridTiles;         // Static array for easy access

    void Start() {
        int width = 10, height = 10;
        gridTiles = new Tile[width, height];
        // Create the grid using loops and Instantiate in rows and columns:contentReference[oaicite:3]{index=3}.
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                // Instantiate a cube and set its parent for organization
                Vector3 pos = new Vector3(x, 0, y);
                GameObject tileObj = Instantiate(tilePrefab, pos, Quaternion.identity, this.transform);
                tileObj.name = $"Tile_{x}_{y}";
                // Store the Tile component and its coords
                Tile tile = tileObj.GetComponent<Tile>();
                tile.x = x;
                tile.y = y;
                gridTiles[x, y] = tile;
            }
        }
    }
}
