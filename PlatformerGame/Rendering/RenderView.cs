using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PlatformerGameClient.Rendering
{
    public abstract class RenderView : Renderer
    {

        public abstract void renderMenuItem(SpriteFont font, string text, float y, Color color);
    }
}
