# CLI Application

This is a Console Application developed using .NET that provides a Command Line Interface (CLI) for managing code files. The CLI has two main commands: `bundle` and `create-rsp`. The application is designed to work with the System.CommandLine library and enables bundling code files into a single file and creating a response file for easier CLI usage.
- [Usage](#usage)
  - [bundle Command](#bundle-command)
  - [create-rsp Command](#create-rsp-command)
- [Validation and Aliases](#validation-and-aliases)
- [Environment Path Setup](#environment-path-setup)
- [Contributing](#contributing)
## Usage

### bundle Command

The `bundle` command is used to package code files into a single output file. The command provides various options to customize the bundling process.

**Command Syntax:**

```bash
dotnet bundle --language <languages> --output <output_file> [options]
```
**Options:**

- `--language`, `-l` (required): Specify the list of programming languages for the bundle. Use `all` to include all code files in the directory.
- `--output`, `-o`: Specify the name or path of the output file. If only a filename is provided, the output file is saved in the current directory.
- `--note`, `-n`: Include the original file name and path as a comment in the bundle file.
- `--sort`, `-s`: Order files by alphabetical order of filename or by file type. The default order is by the filename.
- `--remove-empty-lines`, `-r`: Remove empty lines from the source code before bundling.
- `--author`, `-a`: Add the author's name as a comment at the beginning of the bundle file.

**Example Usage**

**Bundle all code files:**

```bash
dotnet bundle --language all --output "MyBundle.cs" --note --sort type --remove-empty-lines --author "John Doe"
```

**Bundle only C# files:**
``` bash
dotnet bundle --language csharp --output "CSharpBundle.cs" --author "Jane Smith"
```

### create-rsp Command

The `create-rsp` command simplifies the process of creating long commands by generating a response file containing a complete command with all required options.

**Command Syntax:**

```bash
dotnet create-rsp
```
**How it Works:**
- When the create-rsp command is executed, the application prompts the user to enter the desired values for each option.
- Once all options are provided, the application generates a response file (.rsp) containing the complete command.
- The user can then run the command by typing: ``` bash dotnet @<fileName>.rsp ```

**Example Usage**

```bash
dotnet create-rsp
```
The application will prompt for input, and the resulting response file can be used as follows:

```bash
dotnet @bundleCommand.rsp
```

## Validation and Aliases
- Each option in the CLI commands has a short alias for convenience.
- The application performs validation checks on user inputs to ensure correctness.
- Invalid inputs result in a clear and informative error message.

## Environment Path Setup

To run the CLI application from any directory, add the path to the executable file to the system's `Path` environment variable.

1. Open the Control Panel and navigate to **System** > **Advanced system settings**.
2. Click on **Environment Variables**.
3. Under **System variables**, find and select the `Path` variable, then click **Edit**.
4. Click **New** and add the path to the `publish` folder containing the executable file.
5. Click **OK** to save the changes.

## Contributing

Contributions to this project are welcome! Please follow these guidelines:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature/YourFeature`).
3. Commit your changes (`git commit -m 'Add your feature'`).
4. Push to the branch (`git push origin feature/YourFeature`).
5. Open a Pull Request.


