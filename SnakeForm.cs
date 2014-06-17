/******************************************************************************
 * Filename:     SnakeForm.cs
 * Author:       platobeing<platobeing(c)gmail.com>
 * Created Date: 2014-06-15
 * Description:  This file defines the main form of snake game.
 * ****************************************************************************
 * Winter is coming.
 * ***************************************************************************/

namespace Snake
{
    using System.Windows.Forms;

    public partial class SnakeForm : Form
    {
        private readonly SnakeGame _snakeGame;
        public SnakeForm()
        {
            InitializeComponent();
            _snakeGame = new SnakeGame(pbCanvas);
        }

        private void SnakeForm_KeyDown(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, true);
        }

        private void SnakeForm_KeyUp(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, false);
        }

        private void pbCanvas_Paint(object sender, PaintEventArgs e)
        {
            _snakeGame.Draw(e.Graphics);
        }
    }
}
