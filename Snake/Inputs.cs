using Microsoft.Xna.Framework.Input;

namespace Snake
{
	public class Inputs
	{
		KeyboardState keyboardState;
		Player player;

		public Inputs(Player player)
		{
			this.player = player;
		}

		public void CheckUserInput()
		{
			keyboardState = Keyboard.GetState();
			if (!Game1.gameOver)
			{
				if (keyboardState.IsKeyDown(Keys.Left))
					player.ChangeDirection(MoveDirection.LEFT);

				if (keyboardState.IsKeyDown(Keys.Right))
					player.ChangeDirection(MoveDirection.RIGHT);

				if (keyboardState.IsKeyDown(Keys.Down))
					player.ChangeDirection(MoveDirection.DOWN);

				if (keyboardState.IsKeyDown(Keys.Up))
					player.ChangeDirection(MoveDirection.UP);
			}
		}
	}
}