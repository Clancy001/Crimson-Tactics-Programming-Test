// ObstacleEditorWindow.cs (place in an Editor folder)
using UnityEditor;
using UnityEngine;

public class ObstacleEditorWindow : EditorWindow {
    ObstacleData data;

    [MenuItem("Tools/Obstacle Editor")]
    public static void ShowWindow() {
        GetWindow<ObstacleEditorWindow>("Obstacle Editor");
    }

    void OnGUI() {
        // Select the ObstacleData asset to edit
        data = (ObstacleData)EditorGUILayout.ObjectField("Data", data, typeof(ObstacleData), false);
        if (data == null) return;
        // Draw a 10x10 grid of toggle buttons:contentReference[oaicite:9]{index=9}.
        for (int y = 0; y < 10; y++) {
            EditorGUILayout.BeginHorizontal();
            for (int x = 0; x < 10; x++) {
                int index = y * 10 + x;
                data.obstacles[index] = GUILayout.Toggle(data.obstacles[index], ""); // No label, just toggle box
            }
            EditorGUILayout.EndHorizontal();
        }
        // Save button to persist changes
        if (GUILayout.Button("Save Obstacles")) {
            EditorUtility.SetDirty(data);
            AssetDatabase.SaveAssets();
        }
    }
}
