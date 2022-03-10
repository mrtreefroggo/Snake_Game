using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Snake_Game.Interfaces;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Snake_Game.Scripts
{
    public class Sprite : IDraw
    {
        public Vector2 Position;
        private Texture2D sprite;
        private Rectangle location;

        public Guid ID { get; private set; } = Guid.NewGuid();

        public Sprite(Vector2 pos, Texture2D _sprite, Rectangle _location)
        {
            Position = pos;
            sprite = _sprite;
            location = _location;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, Position, location, Color.White);
        }

        public bool CompareSprites(Sprite obj)
        {
            if (obj.ID.Equals(this.ID) == true) return true;
            return false;
        }
    }
}
