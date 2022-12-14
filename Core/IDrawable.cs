using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace JadeFables.Core
{
    public interface IDrawable
    {
        void Draw(SpriteBatch spritebatch);
    }
}