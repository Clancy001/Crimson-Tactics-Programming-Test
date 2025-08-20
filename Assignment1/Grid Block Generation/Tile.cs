// Tile.cs
using UnityEngine;

// Attach this script to each cube to hold its grid info
public class Tile : MonoBehaviour {
    // Grid coordinates for pathfinding and UI
    public int x;
    public int y;
    public bool isObstacle = false;  // Default: not an obstacle
}
