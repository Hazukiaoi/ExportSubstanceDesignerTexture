using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class OutputTextures : EditorWindow {

    Texture tex;

    [MenuItem("SDTools/SD2PNG")]
    static public void Main()
    {
        OutputTextures windows = GetWindow<OutputTextures>();
        windows.Show();
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        tex = (Texture)EditorGUILayout.ObjectField(tex, typeof(Texture), false);
        GUILayout.EndHorizontal();

        GUILayout.Space(20);

        if (GUILayout.Button("Get"))
        {
            RenderTexture ResultTexture = new RenderTexture(tex.width, tex.height, 24);

            Graphics.Blit(tex, ResultTexture);

            Texture2D frame = new Texture2D(ResultTexture.width, ResultTexture.height);
            frame.ReadPixels(new Rect(0, 0, ResultTexture.width, ResultTexture.height), 0, 0, false);
            frame.Apply();
            byte[] bytes = frame.EncodeToPNG();

            System.IO.File.WriteAllBytes("Assets/Temp/Save.png", bytes);
            AssetDatabase.Refresh();
        }     
    }
}
