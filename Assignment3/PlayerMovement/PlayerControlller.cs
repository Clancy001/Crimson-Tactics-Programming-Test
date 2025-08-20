// PlayerController.cs
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
    public float moveSpeed = 3f;
    bool canMove = true;
    GridManager gridMgr;
    Tile currentTile;

    void Start() {
        gridMgr = FindObjectOfType<GridManager>();
        UpdateCurrentTile();
    }

    void UpdateCurrentTile() {
        // Assume player is placed exactly at integer tile coords
        int x = Mathf.RoundToInt(transform.position.x);
        int y = Mathf.RoundToInt(transform.position.z);
        currentTile = GridManager.gridTiles[x, y];
    }

    void Update() {
        // Handle mouse click for movement
        if (canMove && Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit)) {
                Tile targetTile = hit.transform.GetComponent<Tile>();
                if (targetTile != null && !targetTile.isObstacle) {
                    // Compute path (breadth-first search / A*)
                    List<Tile> path = FindPath(currentTile, targetTile);
                    if (path != null && path.Count > 0) {
                        StartCoroutine(MoveAlongPath(path));
                    }
                }
            }
        }
    }

    IEnumerator MoveAlongPath(List<Tile> path) {
        canMove = false;
        // Skip the first element if it's the current tile
        for (int i = 0; i < path.Count; i++) {
            Vector3 dest = new Vector3(path[i].x, 0, path[i].y);
            // Move until close to next tile center
            while (Vector3.Distance(transform.position, dest) > 0.01f) {
                transform.position = Vector3.MoveTowards(transform.position, dest, moveSpeed * Time.deltaTime);
                yield return null;  // resume next frame:contentReference[oaicite:15]{index=15}
            }
        }
        UpdateCurrentTile();
        canMove = true;
    }

    // BFS-based pathfinding on the grid (could use A* with a heuristic for better performance)
    public static List<Tile> FindPath(Tile start, Tile goal) {
        Queue<Tile> queue = new Queue<Tile>();
        Dictionary<Tile, Tile> parent = new Dictionary<Tile, Tile>();
        queue.Enqueue(start);
        parent[start] = null;

        while (queue.Count > 0) {
            Tile current = queue.Dequeue();
            if (current == goal) break;

            // Explore 4 neighbors (N,E,S,W)
            Vector2Int[] deltas = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };
            foreach (var d in deltas) {
                int nx = current.x + d.x, ny = current.y + d.y;
                if (nx >= 0 && nx < 10 && ny >= 0 && ny < 10) {
                    Tile neighbour = GridManager.gridTiles[nx, ny];
                    if (neighbour != null && !neighbour.isObstacle && !parent.ContainsKey(neighbour)) {
                        parent[neighbour] = current;
                        queue.Enqueue(neighbour);
                    }
                }
            }
        }

        // Reconstruct path if goal was reached
        if (!parent.ContainsKey(goal)) return null;
        List<Tile> path = new List<Tile>();
        for (Tile t = goal; t != null; t = parent[t]) {
            path.Add(t);
        }
        path.Reverse();
        return path;
    }
}
