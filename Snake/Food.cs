using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake
{
	public class Food
	{
		Texture2D texture;
		Vector2 position;
		Rectangle rect;
		public static int tileSize = 5;

		public Food(Texture2D texture)
		{
			this.texture = texture;
			NewPos();
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(texture, position, Color.White);
		}

		public void NewPos()
		{
			Random r = new Random();
			position = new Vector2((r.Next() % Game1.windowWidth / Player.tileSize) * Player.tileSize + tileSize, (r.Next() % ((Game1.windowHeight - Game1.gameYOffset) / Player.tileSize)) * Player.tileSize + tileSize + Game1.gameYOffset);
			rect = new Rectangle((int)position.X, (int)position.Y, tileSize, tileSize);
		}

		public Rectangle getRect()
		{
			return rect;
		}

	}
}
