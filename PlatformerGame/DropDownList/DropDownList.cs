using PlatformerGameClient.DropDownList.ObserverPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformerGameClient.DropDownList
{
    public class DropDownList : Subject
    {
        public string selectedItem;
        public List<string> items;
        public DropDownList()
        {
            items = new List<string>();
            selectedItem = string.Empty;
        }

        public void addToList(string item)
        {
            if (!items.Contains(item))
            {
                items.Add(item);
            }
        }

        public void removeFromList(string item)
        {
            if (items.Contains(item))
            {
                items.Remove(item);
            }

        }

        public void setSelectedItem(int index)
        {
            try
            {
                selectedItem = items[index];
            }
            catch
            {
                Console.WriteLine("Out of bounds selection");
            }
        }
    }
}
