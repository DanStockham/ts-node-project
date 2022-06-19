using System;
using System.CommandLine;
using System.CommandLine.NamingConventionBinder;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TypescriptNodeScaffolder
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var root = new RootCommand();

            var createCommand = new Command("create")
            {
                new Argument<string>("projectName", "The name of the generated project")
            };

            createCommand.Handler = CommandHandler.Create<string>((projectName) =>
            {
                Console.WriteLine($"The name of the project: {projectName}");
                var workingDirectory = Environment.CurrentDirectory;

                bool exists = Directory.Exists(Path.Combine(workingDirectory, projectName));
                var invalid = Regex.Match(projectName, @"([A-Z\s])");
                
                if (exists)
                {
                    Console.WriteLine($"There is already a directory with the name '{projectName}'. Please create with a different project name.");
                    return;
                }

                if (invalid.Success)
                {
                    Console.WriteLine($"The project name must be all lowercase and no spaces. Please try again with a valid project name");
                    return;
                }
                
                GenerateProject(projectName);
            });

            root.AddCommand(createCommand);

            await root.InvokeAsync(args);
        }

        static void GenerateProject(string projectName)
        {
            string templatePath = Path.Join(AppDomain.CurrentDomain.BaseDirectory, "Templates");
            var templateSubDirectories = Directory.GetDirectories(templatePath);
            var templateFilePaths = Directory.GetFiles(templatePath, "*", SearchOption.AllDirectories);
            string callingDirectoryPath = Path.Join(Environment.CurrentDirectory, projectName);

            Directory.CreateDirectory(callingDirectoryPath);

            foreach (var dir in templateSubDirectories)
            {
                string directoryName = dir.Substring(templatePath.Length + 1);
                string path = Path.Combine(callingDirectoryPath, directoryName);
                Directory.CreateDirectory(path);
            }

            foreach (var path in templateFilePaths)
            {
                try
                {
                    string fileName = path.Substring(templatePath.Length + 1);
                    File.Copy(path, Path.Combine(callingDirectoryPath, fileName));
                    Console.WriteLine($"written {fileName}");
                }
                catch (IOException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}