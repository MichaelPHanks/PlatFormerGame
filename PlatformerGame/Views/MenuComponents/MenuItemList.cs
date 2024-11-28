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
        private Vector2 topLeftPosition;
        private GraphicsDeviceManager graphicsDeviceManager;
        private SpriteBatch spriteBatch;

        public MenuItemList(List<string> items, SpriteFont defaultFont, SpriteFont hoveredFont, string selectedItem, GraphicsDeviceManager graphics, SpriteBatch spriteBatch, Vector2 topLeftPosition) 
        {

            this.defaultFont = defaultFont;
            this.hoveredFont = hoveredFont;
            this.menuItems = new List<MenuItem>();
            this.selectedItem = selectedItem;
            this.selectedItemIndex = 0;
            this.graphicsDeviceManager = graphics;
            this.spriteBatch = spriteBatch;
            this.topLeftPosition = topLeftPosition;
            this.createMenuItems(items);

        }

        /// <summary>
        /// Factory-ish pattern to create the menu items, purely just based on a list of strings, each string being what 
        /// an item should be displayed as.
        /// </summary>
        /// <param name="menuStrings"></param>
        private void createMenuItems(List<string> menuStrings)
        {
            this.menuItems.Clear();
            float bottom = this.topLeftPosition.Y;
            // Iterate through each of the menuStrings and create a menuItem, add it to the menuItems list.
            foreach (string menuString in menuStrings)
            {

                float scale = graphicsDeviceManager.PreferredBackBufferWidth / 1920f;
                Vector2 stringSize = menuString == selectedItem ? this.hoveredFont.MeasureString(menuString) * scale : this.defaultFont.MeasureString(menuString) * scale;
                
                MenuItem tempMenuItem = new MenuItem(menuString, new Rectangle((int)graphicsDeviceManager.PreferredBackBufferWidth / 2 - (int)stringSize.X / 2, (int)bottom, (int)stringSize.X, (int)stringSize.Y), this.graphicsDeviceManager, menuString == selectedItem ? this.hoveredFont : this.defaultFont, this.spriteBatch, null, menuString == selectedItem, this.hoveredFont);
                this.menuItems.Add(tempMenuItem);
                bottom += stringSize.Y;
            }
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
            float bottom = 0;
            foreach (var item in menuItems)
            {
                if (bottom != 0)
                { item.setBottom((int)bottom); }
                item.draw();
                bottom = item.getStringSize();
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


        public void registerOnClick(string item, MenuItem.OnClick onClick)
        {
            foreach (MenuItem menuItem in menuItems)
            {
                if (menuItem.text == item)
                {
                    menuItem.registerOnClick(onClick);
                    break;
                }

            }
        }
    }
}
