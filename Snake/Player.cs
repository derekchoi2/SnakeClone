using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake
{
	public class Player
	{
		public static int tileSize = 20;
		List<Vector2> positions;
		Texture2D tile;

		MoveDirection currentDirection;
		TimeSpan lastMove;

		double speed;

		Inputs inputs;

		public bool gameOver;

		bool moved = false; //only change direction once per "move". very quick inputs will 
		bool add = false; //whether or not to duplicate last tile when moving

		public Player(Texture2D t, Vector2 startpos, double speed)
		{
			tile = t;
			positions = new List<Vector2>();
			positions.Add(startpos);

			//random starting direction
			Random r = new Random();
			switch (r.Next() % 4)
			{
				case 0: currentDirection = MoveDirection.UP; break;
				case 1: currentDirection = MoveDirection.DOWN; break;
				case 2: currentDirection = MoveDirection.LEFT; break;
				case 3: currentDirection = MoveDirection.RIGHT; break;
			}

			this.speed = speed;
			inputs = new Inputs(this);
			lastMove = TimeSpan.Zero;
			gameOver = false;
		}

		public void Update(GameTime gameTime)
		{
			if (!gameOver)
			{
				inputs.CheckUserInput();
				if (gameTime.TotalGameTime - lastMove > TimeSpan.FromSeconds(1 / speed))
				{
					//move
					Move(gameTime);
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			for (int i = 0; i < positions.Count; i++)
				spriteBatch.Draw(tile, positions[i], Color.White);
		}

		public void ChangeDirection(MoveDirection dir)
		{
			bool opposite = false;
			if (positions.Count > 1) //only prevent opposite movement if more than 1 tile big
				switch (currentDirection)
				{
					case MoveDirection.UP:
						if (dir == MoveDirection.DOWN)
							opposite = true;
						break;
					case MoveDirection.DOWN:
						if (dir == MoveDirection.UP)
							opposite = true;
						break;
					case MoveDirection.LEFT:
						if (dir == MoveDirection.RIGHT)
							opposite = true;
						break;
					case MoveDirection.RIGHT:
						if (dir == MoveDirection.LEFT)
							opposite = true;
						break;
				}
			if (!opposite && moved)
			{
				currentDirection = dir;
				moved = false;
			}
		}

		private void Move(GameTime gameTime)
		{
			lastMove = gameTime.TotalGameTime;
			Vector2 moveVec = new Vector2(0, 0);
			switch (currentDirection)
			{
				case MoveDirection.UP:
					moveVec = new Vector2(0, -tileSize);
					break;
				case MoveDirection.DOWN:
					moveVec = new Vector2(0, tileSize);
					break;
				case MoveDirection.LEFT:
					moveVec = new Vector2(-tileSize, 0);
					break;
				case MoveDirection.RIGHT:
					moveVec = new Vector2(tileSize, 0);
					break;
			}

			Vector2 lastPos = new Vector2(0,0);
			if (add) //save last position
				lastPos = positions[positions.Count - 1];

			for (int i = positions.Count - 1; i >= 0; i--)
			{
				//copy positions from head to tail
				if (i != 0)
					positions[i] = positions[i - 1];
				else //head
					positions[i] += moveVec;
			}

			if (add)
			{
				positions.Add(lastPos);
				add = false;
			}

			moved = true;
		}

		public Rectangle getHead()
		{
			return new Rectangle((int)positions[0].X, (int)positions[0].Y, tileSize, tileSize);
		}

		public void GameOver()
		{
			gameOver = true;
		}

		public bool SelfCollide()
		{
			for (int i = 1; i < positions.Count; i++)
			{
				if (positions[i] == positions[0])
					return true;
			}

			return false;
		}

		public void IncreaseLength()
		{
			add = true;
		}

	}
}
