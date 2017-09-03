using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake
{
	public class GameBoard
	{
		Rectangle rect;
		Texture2D texture;

		public GameBoard(Rectangle rect, GraphicsDevice graphics)
		{
			this.rect = rect;
			texture = new Texture2D(graphics, rect.Width, rect.Height);
			Color[] arr = new Color[rect.Width * rect.Height];
			for (int i = 0; i < arr.Length; i++) arr[i] = new Color(0, 0, 0, 30);
			texture.SetData(arr);
		}

		public bool CheckOutOfBounds(Rectangle other)
		{
			if (other.Intersects(rect))
				return false;
			else
				return true;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(texture, rect, Color.White);
		}
	}
}
