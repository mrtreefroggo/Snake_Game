using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Snake_Game.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace Snake_Game.Scripts
{
    public class Snake : IUpdate, IDraw
    {
        //Private
        private Queue<Sprite> snakeSegments = new Queue<Sprite>();
        private Vector2 startPosition = new Vector2(GameSettings.ScreenWidth / 2, GameSettings.ScreenHeight / 2);

        static private float speed = 32f;
        private Vector2 direction = new Vector2(1, 0);
        public Sprite snakeHead;
        private float lastUpdate = 0;

        //public
        public Vector2 Position;

        public Snake() 
        {
            Reset();
        }

        public void Reset()
        {
            Position = startPosition;
            snakeSegments.Clear();

            //add head
            Vector2 pos = Position;
            snakeHead = new Sprite(pos, Textures.Sheet, new Rectangle(0, 0, 32, 32));
            snakeSegments.Enqueue(snakeHead);

            AddSegment();
        }

        public void AddSegment()
        {
            Vector2 pos = snakeSegments.Peek().Position;
            Sprite segment = new Sprite(pos, Textures.Sheet, new Rectangle(32, 0, 32, 32));
            snakeSegments.Enqueue(segment);
        }

        public void Update(GameTime gameTime)
        {
            //Update direction
            var keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.W))
            {
                direction = new Vector2(0, -1);
            }
            if (keyboard.IsKeyDown(Keys.S))
            {
                direction = new Vector2(0, 1);
            }

            if (keyboard.IsKeyDown(Keys.A))
            {
                direction = new Vector2(-1, 0);
            }
            if (keyboard.IsKeyDown(Keys.D))
            {
                direction = new Vector2(1, 0);
            }

            if (lastUpdate >= 0.11f)
            {
                //update snake position and check for snake head hitting segments
                Vector2 lastPos = Position;
                foreach (Sprite segment in snakeSegments)
                {
                    Vector2 currPos = segment.Position;
                    segment.Position = lastPos;
                    lastPos = currPos;
                }

                //Update snake head position
                Position += (direction * speed);
                snakeHead.Position = new Vector2((int)Math.Round(Position.X), (int)Math.Round(Position.Y));

                foreach (Sprite segment in snakeSegments)
                {
                    if (snakeHead.CompareSprites(segment) == true)
                    {
                        continue;
                    }
                    if (snakeHead.Position == segment.Position)
                    {
                       Reset();
                       return;
                    }
                }

                lastUpdate -= 0.11f;
            }

            lastUpdate += (float)gameTime.ElapsedGameTime.TotalSeconds;

            //check for wall hits

            if (Position.X >= GameSettings.ScreenWidth || Position.X < 0)
            {
                Reset();
            }

            if (Position.Y >= GameSettings.ScreenHeight || Position.Y < 0)
            {
                Reset();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Sprite segment in snakeSegments) 
            {
                segment.Draw(spriteBatch);
            }
        }
    }
}
