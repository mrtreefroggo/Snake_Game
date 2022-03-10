using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Snake_Game.Interfaces;
using System;
using System.Diagnostics;

namespace Snake_Game.Scripts
{
    public class Food : IDraw, IUpdate
    {
        Vector2 Position;
        Sprite sprite;
        Snake snake;

        public Food(Snake _snake)
        {
            snake = _snake;
            randomPos();
            sprite = new Sprite(Position, Textures.Sheet, new Rectangle(64, 0, 32, 32));
        }

        private void randomPos()
        {
            Random rnd = new Random();
            Position = new Vector2(GameSettings.TileSize*rnd.Next(0, (GameSettings.ScreenWidth/GameSettings.TileSize)), GameSettings.TileSize * rnd.Next(0, (GameSettings.ScreenHeight / GameSettings.TileSize)));
        }

        public void EatFood()
        {
            randomPos();
            sprite.Position = Position;
            snake.AddSegment();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            if (snake.Position == Position)
            {
                EatFood();
            }
        }
    }
}
