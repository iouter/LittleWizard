# LittleWizard

A custom character mod for Slay the Spire 2.

## License

### Source Code (outside the special folders)
All source code located outside the `LittleWizard/`, `materials/`, `scenes/`, and `shaders/` directories is licensed under the **MIT License** – see the [LICENSE](./LICENSE) file for details.

### Assets and Protected Content
The following directories and their entire contents are **NOT** covered by the MIT License. They are **All Rights Reserved** unless otherwise stated in a separate license file inside those directories:

- `LittleWizard/`
- `materials/`
- `scenes/`
- `shaders/`

You may not copy, modify, distribute, reverse‑engineer, or use any file from these directories without explicit permission from the copyright holder(s).

---

**Summary**: You are free to use, modify, and distribute the code outside the four listed folders under the terms of the MIT license, but the content inside those folders is proprietary and not open source.

## Development

**Prerequisites:**
- Install [.NET SDK 9](https://dotnet.microsoft.com/download/dotnet/9.0)
- Install [Godot Engine 4.5.1 Mono (.NET)](https://godotengine.org/download/archive/4.5.1-stable/)
- Install export templates in Godot (Editor → Manage Export Templates → Download and Install)

**Getting Started:**

1. Clone the repository:
   ```bash
   git clone <repository-url>
   cd LittleWizard
   ```

2. Restore NuGet packages:
   ```bash
   dotnet restore
   ```

3. Install Spine-Godot plugin:
   - Download the plugin from [spine-godot-extension-4.2-4.5.1-stable.zip](https://spine-godot.s3.eu-central-1.amazonaws.com/4.2/4.5.1-stable/spine-godot-extension-4.2-4.5.1-stable.zip)
   - Extract the ZIP file and locate the `bin` folder inside
   - Copy the entire `bin` folder directly to the project root directory (same level as `project.godot`)

4. Create configuration file:
   - Create a file named `Directory.Build.props` in the project root directory
   - Add the following content (replace with your actual paths):
     ```xml
     <Project>
         <PropertyGroup>
             <GodotPath>Your Godot installation directory</GodotPath>
             <SteamLibraryPath>Your Steam library directory containing Slay the Spire 2</SteamLibraryPath>
         </PropertyGroup>
     </Project>
     ```

5. Code Formatting:
   - This project uses [CSharpier](https://csharpier.com/) for code formatting
   - Install CSharpier (recommended as a VS Code or Rider plugin)
   - Code is automatically formatted on build, or you can run the format command manually

6. Start Developing:
   - Use `dotnet build` to compile only the DLL files
   - Use `dotnet release` to not only compile DLLs, but also export PCK files and copy JSON files, PDB files (if DebugMode is enabled) to the Slay the Spire 2 mod directory

7. Versioning:
   - The version number is automatically generated from Git tags.
   - If the current commit is exactly on a tag (e.g., `v1.3.5`) and the working tree is clean (no uncommitted changes), the version will be the short form `v1.3.5`.
   - Otherwise (extra commits, dirty working tree, or no tag), the version will be the full `git describe --long --dirty` output, e.g., `v1.3.5-2-g123abcd` or `v1.3.5-0-g70ca280-dirty`.
   - The version is written into `LittleWizard.json` and also used as `Version` and `FileVersion` in the assembly.

8. Extended Reading:
   - **How to Enable Debugger**: https://github.com/Alchyr/ModTemplate-StS2/wiki/Testing-and-Debugging#attaching-a-debugger
   - **Slay the Spire 2 Mod Template Setup Guide**: https://github.com/Alchyr/ModTemplate-StS2/wiki
   - **BaseLib Wiki**: https://alchyr.github.io/BaseLib-Wiki/

## Content

- Adds a new playable character: **Little Wizard**

## Requirements

- BaseLib

## Credits

- Built with the StS2 Mod Template
