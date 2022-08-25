using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Graffiti.CodeGeneration.Examples {
public static class CodeGenerationInitializer {

    [MenuItem("Plugins/Code Generation/Generate Color Palette")]
    public static void Run_ColorPaletteGeneration()
    {
        string text = ColorPaletteGenerator.Generate();
        string path = EditorUtility.SaveFilePanel(
            "Select a folder to generate new ColorPalette.cs file",
            Application.dataPath + @"\Plugins\code-generation\CodeGeneration.Examples\Colors Generation",
            "Partial.Generated.ColorPalette",
            "cs");

        // Making path relative to Unity's root folder
        path = path.Remove(0, Application.dataPath.Length - "Assets".Length);

        // If there is a file with the same name, change current path to unique
        path = AssetDatabase.GenerateUniqueAssetPath(path);

        if (File.Exists(path)) {
            Debug.LogWarning("Can't generate a file: file with same name already exists!");
            return;
        }

        WriteToFile(path, text);
    }

    static void WriteToFile(string path, string text)
    {
        Debug.Log("Started writing to: " + path);
        File.WriteAllText(path, text);
        AssetDatabase.Refresh();
    }

}
}
