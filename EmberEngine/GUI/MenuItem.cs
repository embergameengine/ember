using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmberEngine.GUI
{
    public class MenuItem
    {
        public string label;
        public event Action OnClick;

        public void Click()
        {
            OnClick?.Invoke();
        }

        public MenuItem(string label)
        {
            this.label = label;
        }
    }
}
