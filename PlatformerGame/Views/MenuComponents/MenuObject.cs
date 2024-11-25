using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformerGameClient.Views.MenuComponents
{
    public abstract class MenuObject
    {

        protected GraphicsDeviceManager m_graphics;
        protected SpriteFont defaultFont;
        protected SpriteFont hoveredFont;



        public abstract void draw();


        public abstract void selectionChanged();

        public abstract void processInput(GameTime gameTime);
    }
}
