## CLI Install

Pull down the repository and use the dotnet cli to publish

```bash
// Linux distro
dotnet publish -r linux-x64 -c Release --self-contained true --output /path/to/.ts-node-project

// Windows 10
dotnet publish -r win-x64 -c Release --self-contained true --output /path/to/.ts-node-project
```

Next add the published directory path to the PATH environment variable

Linux
```bash
PATH=$PATH:/path/to/bin
export PATH

```

Windows
```powershell
$Env:PATH += ";C:\path\to\bin"
```

## Usage

To create a project

```shell
ts-node-project create <name-of-project>
```