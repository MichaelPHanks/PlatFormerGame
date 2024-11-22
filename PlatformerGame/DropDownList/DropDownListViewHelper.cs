using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerGameClient.DropDownList.ObserverPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformerGameClient.DropDownList
{
    public class DropDownListViewHelper : Observer
    {
        private SpriteBatch spriteBatch;
        private GraphicsDeviceManager graphics;
        string selectedItem;
        List<string> items;
        List<Rectangle> rects;
        float x;
        float y;

        bool isOpen;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="x">How far we are from left to right in terms of the size of the screen for x: Ex: if x = 1/4, we are 1/4 of the way from 0 to the width of the screen.</param>
        /// <param name="y">How far we are from left to right in terms of the size of the screen for y: Ex: if y = 1/4, we are 1/4 of the way from 0 to the height of the screen.</param>
        public DropDownListViewHelper(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, float x, float y) 
        {
            this.graphics = graphics;
            this.spriteBatch = spriteBatch;
            isOpen = false;
            this.x = x;
            this.y = y;
            rects = new List<Rectangle>();
            selectedItem = "";
        }

        public void draw()
        {
            // Draw the selected item



            if (isOpen)
            {

            }
        }


        public void processInput()
        {
            


        }

        public void updateView(DropDownList dropDownList)
        {
            this.selectedItem = dropDownList.selectedItem;
            this.items = dropDownList.items;

        }

        /*private void initializeRectangles()
        {
            foreach (string  item in this.items) 
            {
                this.rects.Add(new Rectangle())
            }
        }*/

        






    }
}
