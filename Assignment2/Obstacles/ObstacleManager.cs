// ObstacleManager.cs
using UnityEngine;

public class ObstacleManager : MonoBehaviour {
    public ObstacleData obstacleData;      // Assign in Inspector
    public Material redMaterial;          // Assign a red material (or set color in code)

    void Start() {
        if (obstacleData == null) return;
        // Loop through the 10x10 grid
        for (int x = 0; x < 10; x++) {
            for (int y = 0; y < 10; y++) {
                int idx = y * 10 + x;
                if (obstacleData.obstacles[idx]) {
                    // Spawn a red sphere at the tile (x,y)
                    GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    sphere.transform.position = new Vector3(x, 0.5f, y);  // Slightly above ground
                    // Color it red
                    Renderer rend = sphere.GetComponent<Renderer>();
                    if (redMaterial != null) rend.material = redMaterial;
                    else rend.material.color = Color.red;
                }
            }
        }
    }
}
