using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace CodeGeneration.Examples {
public static class CodeGenerationInitializer {

    [MenuItem("Plugins/Code Generation/Generate Color Palette")]
    public static void Run_ColorPaletteGeneration()
    {
        string text = ColorPaletteGenerator.Generate();
        string path = RequestFilePath(
            "Generated.ColorPalette",
            "cs",
            @"/Plugins/CodeGeneration");

        if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(text))
            return;

        WriteToFile(path, text);
    }

    [CanBeNull]
    private static string RequestFilePath([NotNull] string fileName, [NotNull] string fileExtension, [NotNull] string defaultRelativePath)
    {
        string path = EditorUtility.SaveFilePanel(
            $"Select where to generate create new {fileName}.{fileExtension} file",
            Application.dataPath + defaultRelativePath,
            fileName,
            fileExtension);

        // If cancelled
        if (path == null)
            return null;

        // Making path relative to Unity's root folder
        path = path.Remove(0, Application.dataPath.Length - "Assets".Length);

        bool doGenerateUniquePass =
            File.Exists(path) &&
            EditorUtility.DisplayDialog(
                "File with the same name already exists",
                "Do you want to create file with different name?",
                "Yes",
                "Cancel");

        if (doGenerateUniquePass)
            path = AssetDatabase.GenerateUniqueAssetPath(path);

        if (File.Exists(path)) {
            Debug.LogWarning("Can't create a file: file with same name already exists!");
            return null;
        }

        return path;
    }

    private static void WriteToFile([NotNull] string path, [NotNull] string text)
    {
        Debug.Log("Started writing to: " + path);
        File.WriteAllText(path, text);
        AssetDatabase.Refresh();
    }

}
}
