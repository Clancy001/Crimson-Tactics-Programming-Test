// EnemyAI.cs
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour, IUnitAI {
    public float moveSpeed = 2f;
    Tile currentTile;
    bool moving = false;

    void Start() {
        UpdateCurrentTile();
    }

    void UpdateCurrentTile() {
        int x = Mathf.RoundToInt(transform.position.x);
        int y = Mathf.RoundToInt(transform.position.z);
        currentTile = GridManager.gridTiles[x, y];
    }

    // Called from PlayerController after player finishes moving
    public void TakeTurn() {
        if (moving) return;
        // Find player's Tile
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerController pc = player.GetComponent<PlayerController>();
        Tile playerTile = GridManager.gridTiles[
            Mathf.RoundToInt(player.transform.position.x),
            Mathf.RoundToInt(player.transform.position.z)
        ];

        // Get all adjacent (4-way) tiles next to player that aren't obstacles
        List<Tile> targets = new List<Tile>();
        Vector2Int[] dirs = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };
        foreach (var d in dirs) {
            int nx = playerTile.x + d.x, ny = playerTile.y + d.y;
            if (nx >= 0 && nx < 10 && ny >= 0 && ny < 10) {
                Tile t = GridManager.gridTiles[nx, ny];
                if (t != null && !t.isObstacle) targets.Add(t);
            }
        }

        // Find shortest path to any of these target tiles
        List<Tile> bestPath = null;
        foreach (Tile target in targets) {
            List<Tile> path = PlayerController.FindPath(currentTile, target);
            if (path != null && (bestPath == null || path.Count < bestPath.Count)) {
                bestPath = path;
            }
        }
        if (bestPath != null && bestPath.Count > 0) {
            StartCoroutine(MoveAlongPath(bestPath));
        }
    }

    IEnumerator MoveAlongPath(List<Tile> path) {
        moving = true;
        // Move enemy along the path
        for (int i = 0; i < path.Count; i++) {
            Vector3 dest = new Vector3(path[i].x, 0, path[i].y);
            while (Vector3.Distance(transform.position, dest) > 0.01f) {
                transform.position = Vector3.MoveTowards(transform.position, dest, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }
        UpdateCurrentTile();
        moving = false;
    }
}
