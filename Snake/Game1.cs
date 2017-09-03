using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Snake
{
	public class Game1 : Game
	{
		public static int windowWidth = 800;
		public static int windowHeight = 640;
		public static int gameYOffset = 40;

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		Texture2D playerTexture;
		Texture2D foodTexture;
		SpriteFont font;

		Food food;
		Player player;
		Vector2 playerStart = new Vector2(windowWidth / 2, (windowHeight - gameYOffset) / 2);
		double playerSpeed = 20;
		GameBoard board;

		int score;

		public static bool gameOver = false;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			graphics.PreferredBackBufferWidth = windowWidth;
			graphics.PreferredBackBufferHeight = windowHeight;
		}

		protected override void Initialize()
		{
			board = new GameBoard(new Rectangle(0, gameYOffset, windowWidth, windowHeight - gameYOffset), GraphicsDevice);
			score = 0;
			base.Initialize();
		}

		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);
			playerTexture = Content.Load<Texture2D>("sprites/player");
			foodTexture = Content.Load<Texture2D>("sprites/food");
			font = Content.Load<SpriteFont>("fonts/uiFont");

			player = new Player(playerTexture, playerStart, playerSpeed);
			food = new Food(foodTexture);

		}

		protected override void Update(GameTime gameTime)
		{

			if (Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			if (gameOver)
				if (Keyboard.GetState().IsKeyDown(Keys.Space))
					Reset();

			if (board.CheckOutOfBounds(player.getHead()) || player.SelfCollide())
			{
				GameOver();
			}
			else
			{
				player.Update(gameTime);
				if (food.getRect().Intersects(player.getHead()))
				{
					score++;
					player.IncreaseLength();
					food.NewPos();
				}
			}

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();
			board.Draw(spriteBatch);
			player.Draw(spriteBatch);
			food.Draw(spriteBatch);

			spriteBatch.DrawString(font, "Score: " + score.ToString(), new Vector2(5, 5), Color.Black);

			//if game over draw reset text
			if (gameOver)
			{
				String resetStr = "Press SPACE to Reset";
				Vector2 strSize = font.MeasureString(resetStr);
				spriteBatch.DrawString(font, resetStr, new Vector2((windowWidth - strSize.X) / 2, (windowHeight - strSize.Y) / 2), Color.Black);
			}

			spriteBatch.End();

			base.Draw(gameTime);
		}

		public void GameOver()
		{
			gameOver = true;
			player.GameOver();
		}

		public void Reset()
		{
			score = 0;
			gameOver = false;
			player = new Player(playerTexture, playerStart, playerSpeed);
			food.NewPos();
		}
	}
}
