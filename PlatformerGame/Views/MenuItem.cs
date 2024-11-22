


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerGameClient.Rendering;

namespace PlatformerGameClient.Views
{
    public class MenuItem
    {

        public string text;
        public Rectangle area;
        public float x;
        public GraphicsDeviceManager m_graphics;
        public SpriteFont font;
        public SpriteBatch spriteBatch;
        public MenuItem(string text, Rectangle area, GraphicsDeviceManager m_graphics, SpriteFont font, SpriteBatch spriteBatch)
        {
            this.text = text;
            this.area = area;
            this.m_graphics = m_graphics;
            this.font = font;
            this.spriteBatch = spriteBatch;
        }

        public float draw(SpriteBatch spriteBatch, bool isSelected, float y)
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
            this.area = new Rectangle((int)m_graphics.PreferredBackBufferWidth / 2 - (int)stringSize.X / 2, (int)y, (int)stringSize.X, (int)stringSize.Y);

            return y + stringSize.Y;

        }

        public void changeFont(SpriteFont font)
        {
            this.font = font;
        }

        public bool processHover(Point mousePosition)
        {
            if ( this.area.Contains(mousePosition))
            {
                return true;
            }
            return false;
        }
    }
}
