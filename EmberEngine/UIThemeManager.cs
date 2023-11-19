using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EmberEngine
{
    public static class UIThemeManager
    {
        public static void SetLightStyle()
        {
            ImGuiStylePtr style = ImGui.GetStyle();

            style.FrameRounding = 2.0f;
            style.WindowPadding = new Vector2(4.0f, 3.0f);
            style.FramePadding = new Vector2(4.0f, 4.0f);
            style.ItemSpacing = new Vector2(4.0f, 3.0f);
            style.IndentSpacing = 12;
            style.ScrollbarSize = 12;
            style.GrabMinSize = 9;
            style.WindowBorderSize = 0.0f;
            style.ChildBorderSize = 0.0f;
            style.PopupBorderSize = 0.0f;
            style.FrameBorderSize = 0.0f;
            style.TabBorderSize = 0.0f;
            style.WindowRounding = 0.0f;
            style.ChildRounding = 0.0f;
            style.FrameRounding = 0.0f;
            style.PopupRounding = 0.0f;
            style.GrabRounding = 2.0f;
            style.ScrollbarRounding = 12.0f;
            style.TabRounding = 0.0f;

            style.Colors[(int)ImGuiCol.Text] = new Vector4(0.15f, 0.15f, 0.15f, 1.00f);
            style.Colors[(int)ImGuiCol.TextDisabled] = new Vector4(0.60f, 0.60f, 0.60f, 1.00f);
            style.Colors[(int)ImGuiCol.WindowBg] = new Vector4(0.87f, 0.87f, 0.87f, 1.00f);
            style.Colors[(int)ImGuiCol.ChildBg] = new Vector4(0.87f, 0.87f, 0.87f, 1.00f);
            style.Colors[(int)ImGuiCol.PopupBg] = new Vector4(0.87f, 0.87f, 0.87f, 1.00f);
            style.Colors[(int)ImGuiCol.Border] = new Vector4(0.89f, 0.89f, 0.89f, 1.00f);
            style.Colors[(int)ImGuiCol.BorderShadow] = new Vector4(0.00f, 0.00f, 0.00f, 0.00f);
            style.Colors[(int)ImGuiCol.FrameBg] = new Vector4(0.93f, 0.93f, 0.93f, 1.00f);
            style.Colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(1.00f, 0.69f, 0.07f, 0.69f);
            style.Colors[(int)ImGuiCol.FrameBgActive] = new Vector4(1.00f, 0.82f, 0.46f, 0.69f);
            style.Colors[(int)ImGuiCol.TitleBg] = new Vector4(1.00f, 1.00f, 1.00f, 1.00f);
            style.Colors[(int)ImGuiCol.TitleBgActive] = new Vector4(1.00f, 1.00f, 1.00f, 1.00f);
            style.Colors[(int)ImGuiCol.TitleBgCollapsed] = new Vector4(1.00f, 1.00f, 1.00f, 1.00f);
            style.Colors[(int)ImGuiCol.MenuBarBg] = new Vector4(1.00f, 1.00f, 1.00f, 1.00f);
            style.Colors[(int)ImGuiCol.ScrollbarBg] = new Vector4(0.87f, 0.87f, 0.87f, 1.00f);
            style.Colors[(int)ImGuiCol.ScrollbarGrab] = new Vector4(1.00f, 1.00f, 1.00f, 1.00f);
            style.Colors[(int)ImGuiCol.ScrollbarGrabHovered] = new Vector4(1.00f, 0.69f, 0.07f, 0.69f);
            style.Colors[(int)ImGuiCol.ScrollbarGrabActive] = new Vector4(1.00f, 0.82f, 0.46f, 0.69f);
            style.Colors[(int)ImGuiCol.CheckMark] = new Vector4(0.01f, 0.01f, 0.01f, 0.63f);
            style.Colors[(int)ImGuiCol.SliderGrab] = new Vector4(1.00f, 0.69f, 0.07f, 0.69f);
            style.Colors[(int)ImGuiCol.SliderGrabActive] = new Vector4(1.00f, 0.82f, 0.46f, 0.69f);
            style.Colors[(int)ImGuiCol.Button] = new Vector4(0.83f, 0.83f, 0.83f, 1.00f);
            style.Colors[(int)ImGuiCol.ButtonHovered] = new Vector4(1.00f, 0.69f, 0.07f, 0.69f);
            style.Colors[(int)ImGuiCol.ButtonActive] = new Vector4(1.00f, 0.82f, 0.46f, 0.69f);
            style.Colors[(int)ImGuiCol.Header] = new Vector4(0.67f, 0.67f, 0.67f, 1.00f);
            style.Colors[(int)ImGuiCol.HeaderHovered] = new Vector4(1.00f, 0.69f, 0.07f, 1.00f);
            style.Colors[(int)ImGuiCol.HeaderActive] = new Vector4(1.00f, 0.82f, 0.46f, 0.69f);
            style.Colors[(int)ImGuiCol.Separator] = new Vector4(1.00f, 1.00f, 1.00f, 1.00f);
            style.Colors[(int)ImGuiCol.SeparatorHovered] = new Vector4(1.00f, 0.69f, 0.07f, 1.00f);
            style.Colors[(int)ImGuiCol.SeparatorActive] = new Vector4(1.00f, 0.82f, 0.46f, 0.69f);
            style.Colors[(int)ImGuiCol.ResizeGrip] = new Vector4(1.00f, 1.00f, 1.00f, 1.00f);
            style.Colors[(int)ImGuiCol.ResizeGripHovered] = new Vector4(1.00f, 0.69f, 0.07f, 0.69f);
            style.Colors[(int)ImGuiCol.ResizeGripActive] = new Vector4(1.00f, 0.82f, 0.46f, 0.69f);
            style.Colors[(int)ImGuiCol.Tab] = new Vector4(0.51f, 0.51f, 0.51f, 1.00f);
            style.Colors[(int)ImGuiCol.TabHovered] = new Vector4(0.78f, 0.78f, 0.78f, 1.00f);
            style.Colors[(int)ImGuiCol.TabActive] = new Vector4(0.69f, 0.69f, 0.69f, 1.00f);
            style.Colors[(int)ImGuiCol.TabUnfocused] = new Vector4(0.07f, 0.10f, 0.15f, 0.97f);
            style.Colors[(int)ImGuiCol.TabUnfocusedActive] = new Vector4(0.14f, 0.26f, 0.42f, 1.00f);
            style.Colors[(int)ImGuiCol.DockingPreview] = new Vector4(1.00f, 0.70f, 0.00f, 0.78f);
            style.Colors[(int)ImGuiCol.DockingEmptyBg] = new Vector4(0.16f, 0.16f, 0.16f, 1.00f);
            style.Colors[(int)ImGuiCol.PlotLines] = new Vector4(0.61f, 0.61f, 0.61f, 1.00f);
            style.Colors[(int)ImGuiCol.PlotLinesHovered] = new Vector4(1.00f, 0.43f, 0.35f, 1.00f);
            style.Colors[(int)ImGuiCol.PlotHistogram] = new Vector4(0.90f, 0.70f, 0.00f, 1.00f);
            style.Colors[(int)ImGuiCol.PlotHistogramHovered] = new Vector4(1.00f, 0.60f, 0.00f, 1.00f);
            style.Colors[(int)ImGuiCol.TextSelectedBg] = new Vector4(0.97f, 0.97f, 0.97f, 0.35f);
            style.Colors[(int)ImGuiCol.DragDropTarget] = new Vector4(1.00f, 1.00f, 0.00f, 0.90f);
            style.Colors[(int)ImGuiCol.NavHighlight] = new Vector4(0.26f, 0.59f, 0.98f, 1.00f);
            style.Colors[(int)ImGuiCol.NavWindowingHighlight] = new Vector4(1.00f, 1.00f, 1.00f, 0.70f);
            style.Colors[(int)ImGuiCol.NavWindowingDimBg] = new Vector4(0.80f, 0.80f, 0.80f, 0.20f);
            style.Colors[(int)ImGuiCol.ModalWindowDimBg] = new Vector4(0.80f, 0.80f, 0.80f, 0.35f);
        }

        public static void SetDarkStyle()
        {
            ImGuiStylePtr style = ImGui.GetStyle();

            style.FrameRounding = 2.0f;
            style.WindowPadding = new Vector2(4.0f, 3.0f);
            style.FramePadding = new Vector2(4.0f, 4.0f);
            style.ItemSpacing = new Vector2(4.0f, 3.0f);
            style.IndentSpacing = 12;
            style.ScrollbarSize = 12;
            style.GrabMinSize = 9;
            style.WindowBorderSize = 0.0f;
            style.ChildBorderSize = 0.0f;
            style.PopupBorderSize = 0.0f;
            style.FrameBorderSize = 0.0f;
            style.TabBorderSize = 0.0f;
            style.WindowRounding = 0.0f;
            style.ChildRounding = 0.0f;
            style.FrameRounding = 0.0f;
            style.PopupRounding = 0.0f;
            style.GrabRounding = 2.0f;
            style.ScrollbarRounding = 12.0f;
            style.TabRounding = 0.0f;

            style.Colors[(int)ImGuiCol.Text] = new Vector4(0.82f, 0.82f, 0.82f, 1.00f);
            style.Colors[(int)ImGuiCol.TextDisabled] = new Vector4(0.60f, 0.60f, 0.60f, 1.00f);
            style.Colors[(int)ImGuiCol.WindowBg] = new Vector4(0.13f, 0.14f, 0.16f, 1.00f);
            style.Colors[(int)ImGuiCol.ChildBg] = new Vector4(0.17f, 0.18f, 0.20f, 1.00f);
            style.Colors[(int)ImGuiCol.PopupBg] = new Vector4(0.22f, 0.24f, 0.25f, 1.00f);
            style.Colors[(int)ImGuiCol.Border] = new Vector4(0.16f, 0.17f, 0.18f, 1.00f);
            style.Colors[(int)ImGuiCol.BorderShadow] = new Vector4(0.16f, 0.17f, 0.18f, 1.00f);
            style.Colors[(int)ImGuiCol.FrameBg] = new Vector4(0.14f, 0.15f, 0.16f, 1.00f);
            style.Colors[(int)ImGuiCol.FrameBgHovered] = new Vector4(0.84f, 0.34f, 0.17f, 1.00f);
            style.Colors[(int)ImGuiCol.FrameBgActive] = new Vector4(0.59f, 0.24f, 0.12f, 1.00f);
            style.Colors[(int)ImGuiCol.TitleBg] = new Vector4(0.13f, 0.14f, 0.16f, 1.00f);
            style.Colors[(int)ImGuiCol.TitleBgActive] = new Vector4(0.13f, 0.14f, 0.16f, 1.00f);
            style.Colors[(int)ImGuiCol.TitleBgCollapsed] = new Vector4(0.13f, 0.14f, 0.16f, 1.00f);
            style.Colors[(int)ImGuiCol.MenuBarBg] = new Vector4(0.13f, 0.14f, 0.16f, 1.00f);
            style.Colors[(int)ImGuiCol.ScrollbarBg] = new Vector4(0.13f, 0.14f, 0.16f, 1.00f);
            style.Colors[(int)ImGuiCol.ScrollbarGrab] = new Vector4(0.51f, 0.51f, 0.51f, 1.00f);
            style.Colors[(int)ImGuiCol.ScrollbarGrabHovered] = new Vector4(0.75f, 0.30f, 0.15f, 1.00f);
            style.Colors[(int)ImGuiCol.ScrollbarGrabActive] = new Vector4(0.51f, 0.51f, 0.51f, 1.00f);
            style.Colors[(int)ImGuiCol.CheckMark] = new Vector4(0.90f, 0.90f, 0.90f, 0.50f);
            style.Colors[(int)ImGuiCol.SliderGrab] = new Vector4(1.00f, 1.00f, 1.00f, 0.30f);
            style.Colors[(int)ImGuiCol.SliderGrabActive] = new Vector4(0.51f, 0.51f, 0.51f, 1.00f);
            style.Colors[(int)ImGuiCol.Button] = new Vector4(0.19f, 0.20f, 0.22f, 1.00f);
            style.Colors[(int)ImGuiCol.ButtonHovered] = new Vector4(0.84f, 0.34f, 0.17f, 1.00f);
            style.Colors[(int)ImGuiCol.ButtonActive] = new Vector4(0.59f, 0.24f, 0.12f, 1.00f);
            style.Colors[(int)ImGuiCol.Header] = new Vector4(0.22f, 0.23f, 0.25f, 1.00f);
            style.Colors[(int)ImGuiCol.HeaderHovered] = new Vector4(0.84f, 0.34f, 0.17f, 1.00f);
            style.Colors[(int)ImGuiCol.HeaderActive] = new Vector4(0.59f, 0.24f, 0.12f, 1.00f);
            style.Colors[(int)ImGuiCol.Separator] = new Vector4(0.17f, 0.18f, 0.20f, 1.00f);
            style.Colors[(int)ImGuiCol.SeparatorHovered] = new Vector4(0.75f, 0.30f, 0.15f, 1.00f);
            style.Colors[(int)ImGuiCol.SeparatorActive] = new Vector4(0.59f, 0.24f, 0.12f, 1.00f);
            style.Colors[(int)ImGuiCol.ResizeGrip] = new Vector4(0.84f, 0.34f, 0.17f, 0.14f);
            style.Colors[(int)ImGuiCol.ResizeGripHovered] = new Vector4(0.84f, 0.34f, 0.17f, 1.00f);
            style.Colors[(int)ImGuiCol.ResizeGripActive] = new Vector4(0.59f, 0.24f, 0.12f, 1.00f);
            style.Colors[(int)ImGuiCol.Tab] = new Vector4(0.16f, 0.16f, 0.16f, 1.00f);
            style.Colors[(int)ImGuiCol.TabHovered] = new Vector4(0.84f, 0.34f, 0.17f, 1.00f);
            style.Colors[(int)ImGuiCol.TabActive] = new Vector4(0.68f, 0.28f, 0.14f, 1.00f);
            style.Colors[(int)ImGuiCol.TabUnfocused] = new Vector4(0.13f, 0.14f, 0.16f, 1.00f);
            style.Colors[(int)ImGuiCol.TabUnfocusedActive] = new Vector4(0.17f, 0.18f, 0.20f, 1.00f);
            style.Colors[(int)ImGuiCol.DockingPreview] = new Vector4(0.19f, 0.20f, 0.22f, 1.00f);
            style.Colors[(int)ImGuiCol.DockingEmptyBg] = new Vector4(0.00f, 0.00f, 0.00f, 1.00f);
            style.Colors[(int)ImGuiCol.PlotLines] = new Vector4(1.00f, 1.00f, 1.00f, 1.00f);
            style.Colors[(int)ImGuiCol.PlotLinesHovered] = new Vector4(0.90f, 0.70f, 0.00f, 1.00f);
            style.Colors[(int)ImGuiCol.PlotHistogram] = new Vector4(0.90f, 0.70f, 0.00f, 1.00f);
            style.Colors[(int)ImGuiCol.PlotHistogramHovered] = new Vector4(1.00f, 0.60f, 0.00f, 1.00f);
            style.Colors[(int)ImGuiCol.TextSelectedBg] = new Vector4(0.75f, 0.30f, 0.15f, 1.00f);
            style.Colors[(int)ImGuiCol.DragDropTarget] = new Vector4(0.75f, 0.30f, 0.15f, 1.00f);
            style.Colors[(int)ImGuiCol.NavHighlight] = new Vector4(0.75f, 0.30f, 0.15f, 1.00f);
            style.Colors[(int)ImGuiCol.NavWindowingHighlight] = new Vector4(1.00f, 1.00f, 1.00f, 0.70f);
            style.Colors[(int)ImGuiCol.NavWindowingDimBg] = new Vector4(0.80f, 0.80f, 0.80f, 0.20f);
            style.Colors[(int)ImGuiCol.ModalWindowDimBg] = new Vector4(0.20f, 0.20f, 0.20f, 0.35f);
        }
    }
}
