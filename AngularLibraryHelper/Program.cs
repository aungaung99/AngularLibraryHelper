using System.Text;

class GeneratePublicApi
{
    static void Main(string[] args)
    {
        // Set the directory path of your Angular library project
        Console.Write("Enter the file path: ");
        string? projectDir = Console.ReadLine();

        // Create a StringBuilder to build the public-api.ts content
        StringBuilder content = new StringBuilder();

        // Iterate through the directories and subdirectories
        IterateThroughDirectories(projectDir ?? "", content, "");

        // Write the content to the public-api.ts file
        string publicApiPath = Path.Combine(projectDir ?? "", "index.ts");
        File.WriteAllText(publicApiPath, content.ToString());

        Console.WriteLine($"Generated index.ts file at {publicApiPath} use in public-api.ts");
    }

    static void IterateThroughDirectories(string dir, StringBuilder content, string prefix)
    {
        // Get the directories and files in the current directory
        string[] dirs = Directory.GetDirectories(dir);
        string[] files = Directory.GetFiles(dir, "*.ts");

        // Iterate through the files
        foreach (string file in files)
        {
            string fileName = Path.GetFileNameWithoutExtension(file);
            content.AppendLine($"export * from './{prefix}{fileName}';");
        }

        // Iterate through the subdirectories
        foreach (string subDir in dirs)
        {
            string subDirName = Path.GetFileName(subDir);
            IterateThroughDirectories(subDir, content, prefix + subDirName + "/");
        }
    }
}