using CodeGeneration;

namespace CodeGeneration.Examples {
public static class CodeBlocksGenerator {

    public static string Generate()
    {
        var b = CodeBuilder.CreateDefaultBuilder();

        b.Header(nameof(CodeBlocksGenerator)).Br();

        b.Public.Namespace.Name("Generated").Body(() => {



        });

        return b.ToString();

    }

}
}
