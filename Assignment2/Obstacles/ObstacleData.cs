// ObstacleData.cs
using UnityEngine;

[CreateAssetMenu(fileName = "ObstacleData", menuName = "Grid/ObstacleData")]
public class ObstacleData : ScriptableObject {
    public bool[] obstacles = new bool[100];  // Flattened 10x10 grid
}
