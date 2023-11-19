using ImGuiNET;

namespace EmberEngine.GUI
{
    public enum MenuBarType
    {
        MainMenuBar,
        WindowMenuBar
    }

    public class MenuBar
    {
        private bool show = true;
        private List<Menu> menus;
        public MenuBarType type;

        public MenuBar(MenuBarType type)
        {
            menus = new List<Menu>();

            this.type = type;
        }

        public void AddMenu(Menu menu)
        {
            menus.Add(menu);
        }

        public void Show()
        {
            show = true;
        }

        public void Hide()
        {
            show = false;
        }

        public void Render()
        {
            if (show)
            {
                switch(type)
                {
                    case MenuBarType.MainMenuBar:
                        if (ImGui.BeginMainMenuBar())
                        {
                            foreach (Menu menu in menus)
                            {
                                if (ImGui.BeginMenu(menu.label))
                                {
                                    foreach (MenuItem menuItem in menu.menuItems)
                                    {
                                        if (ImGui.MenuItem(menuItem.label))
                                        {
                                            menuItem.Click();
                                        }
                                    }
                                }
                            }
                        }

                        break;
                    case MenuBarType.WindowMenuBar:
                        if (ImGui.BeginMenuBar())
                        {
                            foreach (Menu menu in menus)
                            {
                                if (ImGui.Begin(menu.label))
                                {
                                    foreach (MenuItem menuItem in menu.menuItems)
                                    {
                                        if (ImGui.MenuItem(menuItem.label))
                                        {
                                            menuItem.Click();
                                        }
                                    }
                                }
                            }
                        }

                        break;
                }
            }

        }
    }
}
