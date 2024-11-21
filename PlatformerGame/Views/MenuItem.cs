


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerGameClient.Rendering;

namespace PlatformerGameClient.Views
{
    public class MenuItem : Renderer
    {

        public string text;
        public Rectangle area;
        public float y;
        public float x;
        public GraphicsDeviceManager m_graphics;
        public SpriteFont font;
        public SpriteBatch spriteBatch;
        public Color color;
        public MenuItem(string text, Rectangle area, float y, float x, GraphicsDeviceManager m_graphics, SpriteFont font, SpriteBatch spriteBatch, Color color)
        {
            this.text = text;
            this.area = area;
            this.y = y;
            this.x = x;
            this.m_graphics = m_graphics;
            this.font = font;
            this.spriteBatch = spriteBatch;
            this.color = color;
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            float scale = m_graphics.PreferredBackBufferWidth / 1920f;
            Vector2 stringSize = font.MeasureString(text) * scale;
            spriteBatch.DrawString(
                           font,
                           text,
                           new Vector2(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2, y),
                           color,
                           0,
                           Vector2.Zero,
                           scale,
                           SpriteEffects.None,
                           0);

            return y + stringSize.Y;

        }
    }
}
