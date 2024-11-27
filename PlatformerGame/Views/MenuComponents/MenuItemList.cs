using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformerGameClient.Views.MenuComponents
{
    public class MenuItemList : MenuObject
    {

        private List<MenuItem> menuItems;
        private string selectedItem;
        private int selectedItemIndex;

        public MenuItemList(List<MenuItem> menuItems, SpriteFont defaultFont, SpriteFont hoveredFont, string selectedItem) 
        {

            this.defaultFont = defaultFont;
            this.hoveredFont = hoveredFont;
            this.menuItems = menuItems;
            this.selectedItem = selectedItem;
            this.selectedItemIndex = 0;
        }


        public void setSelectedItem(string selectedItem, int index)
        {
            this.selectedItem = selectedItem;
            this.selectedItemIndex = index;
            this.setMenuItemsSizes();
        }





        public override void processInput(GameTime gameTime)
        {
            /*int index = 0;
            foreach (MenuItem item in menuItems)
            {
                if (item.isHoveredOver())
                {
                    this.setSelectedItem(item.text, index);
                }
                index++;

            }*/

            foreach (MenuItem item in menuItems)
            {
                item.processInput(gameTime);
            }

        }

        public override string isHoveredOver()
        {
            string tempText = "";
            foreach (MenuItem item in menuItems)
            {
                tempText = item.isHoveredOver();
                if (tempText != "")
                {
                    return tempText;
                }
            }
            return "";
        }

        private void setMenuItemsSizes()
        {
            float bottom = 0;
            int iteration = 0;
            foreach (MenuItem item in menuItems)
            {
                if (item.text == this.selectedItem)
                {
                    item.setIsSelected(true);
                    item.changeFont(this.hoveredFont);
                    
                }
                else
                {
                    item.setIsSelected(false);
                    item.changeFont(this.defaultFont);
                }
                if (iteration != 0)
                {
                    item.setBottom(bottom);
                }
                iteration++;
                bottom = item.getStringSize();
            }

        }



        public override void draw()
        {
            foreach (var item in menuItems)
            {
                item.draw();
            }
        }

        /// <summary>
        /// This is notified (from the View itself) that we are changing the selection to something different.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public override void selectionChanged(string selection)
        {
            this.setSelectedItem(selection, 0);
            
        }
    }
}
