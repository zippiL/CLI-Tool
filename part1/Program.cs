
using System.CommandLine;

var bundleOutputOption = new Option<FileInfo>("--output", "File path name");
var bundleLanguageOption = new Option<string>("--language", "language option") { IsRequired = true };
var bundleNoteOption = new Option<bool>("--note", "note option");
var bundleSortOption = new Option<bool>("--sort", "sort option");
var bundleRemoveOption = new Option<bool>("--remove-empty-lines", "remove-empty-lines option");
var bundleAuthorOption = new Option<string>("--author", "author option");

bundleOutputOption.AddAlias("-o");
bundleLanguageOption.AddAlias("-l");
bundleNoteOption.AddAlias("-n");
bundleSortOption.AddAlias("-s");
bundleRemoveOption.AddAlias("-r");
bundleAuthorOption.AddAlias("-a");

var bundleCommand = new Command("bundle", "Bundle code files to single file");
bundleCommand.AddOption(bundleOutputOption);
bundleCommand.AddOption(bundleLanguageOption);
bundleCommand.AddOption(bundleNoteOption);
bundleCommand.AddOption(bundleSortOption);
bundleCommand.AddOption(bundleRemoveOption);
bundleCommand.AddOption(bundleAuthorOption);

string projectPath = AppContext.BaseDirectory;

bundleCommand.SetHandler((output, language, note, sort, remove, author) =>
{
    try
    {
        using (File.Create(output.FullName))
        { }
        using (StreamWriter writer = new StreamWriter(output.FullName))
        {
            if (note)
                writer.WriteLine("//" + projectPath);
            if (author != null)
                writer.WriteLine("//" + author);
            string[] files = new string[0];

            if (language != "all")
            {
                string[] languages = language.Split(',');

                foreach (var lang in languages)
                {
                    string currentLanguage = lang.Trim();
                    if (currentLanguage != "all")
                    {
                        files = files.Concat(Directory.GetFiles(Directory.GetCurrentDirectory(), $"*.{currentLanguage}", SearchOption.AllDirectories)).ToArray();
                    }
                }
            }
            else
            {
                files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*", SearchOption.AllDirectories);

            }
            files = files.Where(file => !file.Contains("obj")).ToArray();
            files = files.Where(file => !file.Contains("bin")).ToArray();
            files = files.Where(file => !file.Contains(".vs")).ToArray();
            files = files.Where(file => !file.Contains(".sln")).ToArray();
            files = files.Where(file => !file.Contains(".rsp")).ToArray();
            if (sort)
                files = files.OrderBy(file => Path.GetExtension(file)).ToArray();
            else
                files = files.OrderBy(file => Path.GetFileName(file)).ToArray();


            foreach (var filePath in files)
            {
                try
                {
                    string[] lines = File.ReadAllLines(filePath);
                    writer.WriteLine("//---" + Path.GetFileName(filePath) + "---");

                    foreach (var line in lines)
                    {
                        if (remove)
                        {
                            if (!string.IsNullOrWhiteSpace(line))
                                writer.WriteLine(line);
                        }
                        else
                        {
                            writer.WriteLine(line);
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error! An error occurred while copying the file");
                }
            }


        }
        Console.WriteLine("The file was created successfully");
    }
    catch (DirectoryNotFoundException ex)
    {
        Console.WriteLine("Error! The file path is invalid");
    }

}, bundleOutputOption, bundleLanguageOption, bundleNoteOption, bundleSortOption, bundleRemoveOption, bundleAuthorOption);
var rootCommand = new RootCommand("Root Command for  file builder cli");

rootCommand.AddCommand(bundleCommand);

var createRspCommand = new Command("create-rsp", "Create response file");
createRspCommand.SetHandler(() =>
{
    string fileName = "rsp.rsp";
    using (StreamWriter writer = new StreamWriter(fileName))
    {
        writer.WriteLine("bundle ");
        Console.WriteLine("Enter the file name + routing if necessary");
        string wfile = Console.ReadLine();
        if (wfile != "")
            writer.WriteLine("-o " + wfile);
        Console.WriteLine("Enter the name of the file owner");
        string author = Console.ReadLine();
        if (author != "")
            writer.WriteLine("-a " + author);
        Console.WriteLine("Enter the names of the languages you want to " +
            "include separated by a comma if you want to include all the files write all");
        string langouges = Console.ReadLine();
        if (langouges != "")
            writer.WriteLine("-l " + langouges);

        Console.WriteLine("you want note true/false");
        bool note = bool.Parse(Console.ReadLine());
        if (note)
        {
            writer.WriteLine(" -n");
        }
        Console.WriteLine("If you want to sort by file type true/false");
        bool sort = bool.Parse(Console.ReadLine());
        if (sort)
        {
            writer.WriteLine(" -s");
        }
        Console.WriteLine("If you want to remove blank lines? true/false");
        bool remove = bool.Parse(Console.ReadLine());
        if (remove)
        {
            writer.WriteLine(" -r");
        }

    }

});


rootCommand.AddCommand(createRspCommand);

rootCommand.InvokeAsync(args);