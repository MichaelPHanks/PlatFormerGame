using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Runtime.CompilerServices;

namespace PlatformerGameClient.Rendering
{
    public class RenderMenuItem : Renderer
    {

        SpriteFont font;
        string text;
        float y;
        float x;
        Color color;
        SpriteBatch spriteBatch;
        GraphicsDeviceManager m_graphics;

        public RenderMenuItem(SpriteFont font, string text, float y, float x, Color color, SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            this.font = font;
            this.text = text;
            this.y = y;
            this.x = x;
            this.color = color;
            this.spriteBatch = spriteBatch;
            m_graphics = graphics;
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            this.renderMenuItem(this.font, this.text, this.y, this.color, this.spriteBatch, this.m_graphics);
            
        }




        public virtual float renderMenuItem(SpriteFont font, string text, float y, Color color, SpriteBatch m_spriteBatch, GraphicsDeviceManager m_graphics)
        {

            float scale = m_graphics.PreferredBackBufferWidth / 1920f;
            Vector2 stringSize = font.MeasureString(text) * scale;
            m_spriteBatch.DrawString(
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
