using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmberEngine.GUI
{
    public class Menu
    {
        public string label;
        public List<MenuItem> menuItems;

        public Menu(string label)
        {
            this.label = label;
            menuItems = new List<MenuItem>();
        }

        public void AddMenuItem(MenuItem item)
        {
            menuItems.Add(item);
        }
    }
}
