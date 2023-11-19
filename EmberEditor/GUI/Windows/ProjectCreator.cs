using ImGuiNET;
using EmberEngine.GUI;
using EmberEngine;
using EmberEngine.Components;
using System.IO.Compression;

namespace AuroraEditor.GUI.Windows
{
    public class ProjectCreator : GUIWindow
    {
        string projectName = "ember3D Game";
        string projectPath = "C:";


        void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
        {
            // Get information about the source directory
            var dir = new DirectoryInfo(sourceDir);

            // Check if the source directory exists
            if (!dir.Exists)
                throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

            // Cache directories before we start copying
            DirectoryInfo[] dirs = dir.GetDirectories();

            // Create the destination directory
            Directory.CreateDirectory(destinationDir);

            // Get the files in the source directory and copy to the destination directory
            foreach (FileInfo file in dir.GetFiles())
            {
                string targetFilePath = Path.Combine(destinationDir, file.Name);
                if (File.Exists(targetFilePath))
                {
                    continue;
                }
                file.CopyTo(targetFilePath);
            }

            // If recursive and copying subdirectories, recursively call this method
            if (recursive)
            {
                foreach (DirectoryInfo subDir in dirs)
                {
                    string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                    CopyDirectory(subDir.FullName, newDestinationDir, true);
                }
            }
        }

        public void CreateProject(string zipPath, string extractPath, string name)
        {
            // Extract the zip file
            ZipFile.ExtractToDirectory(zipPath, extractPath);

            // Get all files in the directory and its subdirectories
            string[] files = Directory.GetFiles(extractPath, "*.*", SearchOption.AllDirectories);

            // Replace the string in each file
            foreach (string file in files)
            {
                string content = File.ReadAllText(file);
                content = content.Replace("ReplaceWithGameName", name);
                File.WriteAllText(file, content);
            }

            // Copy libraries
            CopyDirectory("projLib", extractPath + "\\CSharp\\lib", true);

            // Copy runtimes
            CopyDirectory("runtimes", extractPath + "\\CSharp\\bin\\Debug\\net6.0\\runtimes", true);

            // Copy imgui
            File.Copy(extractPath + "\\CSharp\\bin\\Debug\\net6.0\\runtimes\\win-x64\\native\\cimgui.dll", extractPath + "\\CSharp\\bin\\Debug\\net6.0\\cimgui.dll");

        }

        public ProjectCreator()
        {
            windowName = "project creator";
        }

        public override void RenderContent()
        {
            ImGui.InputText("Name", ref projectName, 128);
            ImGui.InputText("Path", ref projectPath, 128);

            if (ImGui.Button("Create"))
            {
                CreateProject("projectTemplate.zip", projectPath, projectName.Replace(" ", ""));
                EditorManager.projectLocation = projectPath;
                SceneManager.LoadScene("UnloadedScene");
                Close();
            }

            if (ImGui.Button("Play music"))
            {
                AudioManager.PlayFile("Sounds/barriers16bit.wav");
            }
        }
    }
}
