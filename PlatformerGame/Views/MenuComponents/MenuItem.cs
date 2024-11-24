


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PlatformerGameClient.Rendering;

namespace PlatformerGameClient.Views.MenuComponents
{
    public class MenuItem
    {

        public string text;
        public Rectangle area;
        public float x;
        public GraphicsDeviceManager m_graphics;
        public SpriteFont font;
        public SpriteBatch spriteBatch;

        public delegate void OnClick(GameTime gameTime);

        private OnClick onClick;

        private bool isSelected = false;

        private float y = 0f;
        public MenuItem(string text, Rectangle area, GraphicsDeviceManager m_graphics, SpriteFont font, SpriteBatch spriteBatch, OnClick onClickDelgate, bool isSelected)
        {
            this.text = text;
            this.area = area;
            this.m_graphics = m_graphics;
            this.font = font;
            this.spriteBatch = spriteBatch;
            onClick = onClickDelgate;
            y = this.area.Y;
            this.isSelected = isSelected;

        }


        public void setArea(Rectangle area)
        {
            this.area = area;
        }

        public void draw()
        {
            float scale = m_graphics.PreferredBackBufferWidth / 1920f;
            Vector2 stringSize = font.MeasureString(text) * scale;
            spriteBatch.DrawString(
                           font,
                           text,
                           new Vector2(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2, y),
                           isSelected ? Color.Blue : Color.Black,
                           0,
                           Vector2.Zero,
                           scale,
                           SpriteEffects.None,
                           0);
            area = new Rectangle(m_graphics.PreferredBackBufferWidth / 2 - (int)stringSize.X / 2, (int)y, (int)stringSize.X, (int)stringSize.Y);

            /*return y + stringSize.Y;*/

        }

        public float getStringSize()
        {
            float scale = m_graphics.PreferredBackBufferWidth / 1920f;
            Vector2 stringSize = font.MeasureString(text) * scale;
            return y + stringSize.Y;
        }
        public void setBottom(float bottom)
        {
            this.y = bottom;    
        }
        public bool getIsSelected()
        {
            return isSelected;
        }

        public void setIsSelected(bool isSelected)
        {
            this.isSelected = isSelected;
        }

        public void changeFont(SpriteFont font)
        {
            this.font = font;
        }

        public bool isHoveredOver()
        {
            return area.Contains(Mouse.GetState().Position);
        }


        public void processInput(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && isSelected)
            {
                Clicked(gameTime);
            }

            else if (Mouse.GetState().LeftButton == ButtonState.Pressed && isHoveredOver())
            {
                Clicked(gameTime);
            }
        }


        private void Clicked(GameTime gameTime)
        {
            // Insert some stuff here

            // Call the callback function, which is determined by view class using this item.
            if (onClick != null)
            {
                onClick(gameTime);
            }
        }


    }
}
