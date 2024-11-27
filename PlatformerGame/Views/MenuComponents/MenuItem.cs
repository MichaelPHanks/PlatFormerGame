


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PlatformerGameClient.Rendering;

namespace PlatformerGameClient.Views.MenuComponents
{
    public class MenuItem : MenuObject
    {

        public string text;
        public Rectangle area;
        public float x;
        public GraphicsDeviceManager m_graphics;
        public SpriteFont font;
        public SpriteFont baseFont;
        public SpriteBatch spriteBatch;

        public delegate void OnClick(GameTime gameTime);
        public delegate void OnHover(GameTime gameTime, MenuItem menuItem);

        private OnClick onClick;


        private OnHover onHover;

        private SpriteFont m_selectedFont;

        private bool isSelected = false;

        private float y = 0f;
        public MenuItem(string text, Rectangle area, GraphicsDeviceManager m_graphics, SpriteFont font, SpriteBatch spriteBatch, OnClick onClickDelgate, bool isSelected, SpriteFont selectedFont)
        {
            this.text = text;
            this.area = area;
            this.m_graphics = m_graphics;
            this.font = font;
            this.spriteBatch = spriteBatch;
            onClick = onClickDelgate;
            y = this.area.Y;
            this.isSelected = isSelected;
            m_selectedFont = selectedFont;
            baseFont = font;
            
        }


        public void setArea(Rectangle area)
        {
            this.area = area;
        }

        public override void draw()
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

        public override string isHoveredOver()
        {
            return area.Contains(Mouse.GetState().Position) ? this.text : "";
        }


        public override void processInput(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && isSelected)
            {
                Clicked(gameTime);
            }

            else if (Mouse.GetState().LeftButton == ButtonState.Pressed && isSelected)
            {
                Clicked(gameTime);
            }

            else if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed && isSelected)
            {
                Clicked(gameTime);
            }


            /*setIsSelected(isHoveredOver());


            if (isSelected)
            {
                Hovered(gameTime);
            }
            */
            
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

        /// <summary>
        /// This will sometimes not happen, especially for menu items in a menu item list, as that list will handle the font changes.
        /// </summary>
        /// <param name="gameTime"></param>
        private void Hovered(GameTime gameTime)
        {
            if (onHover != null)
            {
                onHover(gameTime, this);
            }
        }

        public void registerHover(OnHover onHover)
        {
            this.onHover = onHover;
        }

        
        /// <summary>
        /// We don't need to do anything quite yet.
        /// </summary>
        public override void selectionChanged(string selection)
        {
            if (selection == this.text)
            {
                this.font = m_selectedFont;
            }
            else
            {
                this.font = baseFont;
            }
        }
    }
}
