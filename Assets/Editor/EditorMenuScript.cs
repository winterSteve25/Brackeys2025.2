using System.IO;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class EditorMenuScript
    {
        [MenuItem("Assets/Export Sliced Sprites")]
        private static void ExportSlicedSprites()
        {
            Object selected = Selection.activeObject;
            if (selected == null)
            {
                Debug.LogError("No sprite sheet selected.");
                return;
            }

            string path = AssetDatabase.GetAssetPath(selected);
            Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);

            if (texture == null)
            {
                Debug.LogError("Selected asset is not a Texture2D.");
                return;
            }

            // Load all sub-assets (the sliced sprites)
            Object[] sprites = AssetDatabase.LoadAllAssetRepresentationsAtPath(path);
            if (sprites == null || sprites.Length == 0)
            {
                Debug.LogError("No sliced sprites found in this texture. Make sure it is sliced in the Sprite Editor.");
                return;
            }

            string exportPath = EditorUtility.OpenFolderPanel("Export Sprites To Folder", Application.dataPath, "");
            if (string.IsNullOrEmpty(exportPath))
                return;

            foreach (var obj in sprites)
            {
                Sprite sprite = obj as Sprite;
                if (sprite == null) continue;

                Texture2D newTex = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
                Color[] pixels = sprite.texture.GetPixels(
                    (int)sprite.rect.x,
                    (int)sprite.rect.y,
                    (int)sprite.rect.width,
                    (int)sprite.rect.height);
                newTex.SetPixels(pixels);
                newTex.Apply();

                byte[] pngData = newTex.EncodeToPNG();
                string filePath = Path.Combine(exportPath, sprite.name + ".png");
                File.WriteAllBytes(filePath, pngData);

                Object.DestroyImmediate(newTex);
            }

            AssetDatabase.Refresh();
            Debug.Log($"Exported {sprites.Length} sprites to {exportPath}");
        }

        [MenuItem("Assets/Export Sliced Sprites", true)]
        private static bool ValidateExportSlicedSprites()
        {
            return Selection.activeObject is Texture2D;
        }
    }
}