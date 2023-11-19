using ImGuiNET;
using EmberEngine.GUI;
using EmberEngine;
using EmberEngine.Components;
using System.Numerics;

namespace AuroraEditor.GUI.Windows
{
    public class Output : GUIWindow
    {
        static string outputText = "ember v1.0";

        static void TransitionText(string targetText)
        {
            string oldText = outputText;

            string _targetText = targetText;
            outputText = string.Empty;
            int _currentIndex = 0;

            while (_currentIndex < _targetText.Length)
            {
                outputText += _targetText[_currentIndex];
                _currentIndex++;

                Thread.Sleep(10);
            }
        }

        static string CharArrayToString(char[] array)
        {
            string buf = "";

            foreach (char chr in array)
            {
                buf += chr;
            }

            return buf;
        }

        static void TransitionOut()
        {
            while (true)
            {
                if (outputText.Length == 0)
                {
                    break;
                }

                List<char> text = outputText.ToCharArray().ToList();
                text.RemoveAt(text.Count - 1);
                outputText = CharArrayToString(text.ToArray());

                Thread.Sleep(10);
            }
        }

        public static void SetOutput(string text, int timeMs)
        {
            Thread setOutputThread = new Thread(() =>
            {
                TransitionOut();
                TransitionText(text);
                Thread.Sleep(timeMs);
                TransitionOut();
                TransitionText("ember v1.0");
            });

            setOutputThread.Start();
        }

        public Output()
        {
            windowName = "output";
            fixedPosition = true;
            fixedSize = true;
            flags = ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoResize;


        }

        public override void RenderContent()
        {
            windowPosition = new Vector2(0, ImGui.GetIO().DisplaySize.Y - 25);
            windowSize = new Vector2(ImGui.GetIO().DisplaySize.X, 15);
            ImGui.Text(outputText);
        }
    }
}
