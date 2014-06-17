/******************************************************************************
 * Filename:     SnakeGame.cs
 * Author:       platobeing<platobeing(c)gmail.com>
 * Created Date: 2014-06-15
 * Description:  
 * ****************************************************************************
 * Winter is coming.
 * ***************************************************************************/


namespace Snake
{
    #region using directives
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms; 
    #endregion

    public class SnakeGame
    {
        #region private fields
        private const int BOARD_WIDTH = 320;
        private const int BOARD_HEIGHT = 240;

        private int _score;
        private bool _gameover;
        private Direction _direction = Direction.Down;

        private readonly List<SnakePart> _snake = new List<SnakePart>();
        private SnakePart _food;

        private readonly Timer _gameLoop = new Timer();
        private readonly Timer _snakeLoop = new Timer();

        private float _snakeRate = 2.0f;

        private readonly Control _canvas; 
        #endregion

        #region constructor
        public SnakeGame(Control canvas)
        {
            _canvas = canvas;
            _canvas.Width = BOARD_WIDTH;
            _canvas.Height = BOARD_HEIGHT;

            _gameLoop.Tick += update;
            _snakeLoop.Tick += updateSnake;

            _gameLoop.Interval = 1000 / 60;
            _snakeLoop.Interval = (int)(1000 / _snakeRate);

            _gameLoop.Start();
            _snakeLoop.Start();

            StartGame();
        } 
        #endregion

        #region public methods
        /// <summary>
        /// Start a new game.
        /// </summary>
        public void StartGame()
        {
            _gameover = false;
            _snake.Clear();
            _score = 0;
            _snakeRate = 2.0f;
            _direction = Direction.Down;

            _snakeLoop.Interval = (int)(1000 / _snakeRate);

            SnakePart head = new SnakePart(10, 8);
            _snake.Add(head);

            generateFood();
        }

        /// <summary>
        /// Draw elements of the game.
        /// </summary>
        /// <param name="canvas">The canvas to be drawed.</param>
        public void Draw(Graphics canvas)
        {
            if (_gameover)
            {
                // draw final score
                drawFinalScore(canvas);
            }
            else
            {
                // update score
                drawScore(canvas);
                // draw snake parts
                drawSnakeParts(canvas);
                // draw food
                drawFood(canvas);
            }
        }
        #endregion

        #region private methods
        private void drawFood(Graphics canvas)
        {
            canvas.FillRectangle(new SolidBrush(Color.Orange), new Rectangle(_food.X * 16, _food.Y * 16, 16, 16));
        }

        private void drawSnakeParts(Graphics canvas)
        {
            for (int i = 0; i < _snake.Count; i++)
            {
                Color snakeColor = (i == 0) ? Color.Red : Color.Black;
                SnakePart currentPart = _snake[i];
                canvas.FillRectangle(new SolidBrush(snakeColor), new Rectangle(currentPart.X * 16, currentPart.Y * 16, 16, 16));
            }
        }

        private void drawScore(Graphics canvas)
        {
            canvas.DrawString("Score " + _score, _canvas.Font, Brushes.White, new PointF(4, 4));
        }

        private void drawFinalScore(Graphics canvas)
        {
            string text = "Game Over";
            SizeF size = canvas.MeasureString(text, _canvas.Font);
            canvas.DrawString(text, _canvas.Font, Brushes.White, new PointF(BOARD_WIDTH / 2 - size.Width / 2, 100));
            text = "Final Score " + _score;
            size = canvas.MeasureString(text, _canvas.Font);
            canvas.DrawString(text, _canvas.Font, Brushes.White,
                new PointF(BOARD_WIDTH / 2 - size.Width / 2, 120));
            text = "Press Enter to Start a New Game";
            size = canvas.MeasureString(text, _canvas.Font);
            canvas.DrawString(text, _canvas.Font, Brushes.White,
                new PointF(BOARD_WIDTH / 2 - size.Width / 2, 140));
        }

        private void gameover()
        {
            _gameover = true;
        }

        private void update(object sender, EventArgs e)
        {
            if (_gameover)
            {
                if (Input.Pressed(Keys.Enter))
                {
                    StartGame();
                }
            }
            else
            {
                updateDirection();
            }
            _canvas.Invalidate();
        }

        private void updateDirection()
        {
            if (Input.Pressed(Keys.Left))
            {
                if (_snake.Count < 2 || _snake[0].X == _snake[1].X)
                {
                    _direction = Direction.Left;
                }
            }
            else if (Input.Pressed(Keys.Right))
            {
                if (_snake.Count < 2 || _snake[0].X == _snake[1].X)
                {
                    _direction = Direction.Right;
                }
            }
            else if (Input.Pressed(Keys.Up))
            {
                if (_snake.Count < 2 || _snake[0].Y == _snake[1].Y)
                {
                    _direction = Direction.Up;
                }
            }
            else if (Input.Pressed(Keys.Down))
            {
                if (_snake.Count < 2 || _snake[0].Y == _snake[1].Y)
                {
                    _direction = Direction.Down;
                }
            }
        }

        private void updateSnake(object sender, EventArgs e)
        {
            if (_gameover) return;

            for (int i = _snake.Count - 1; i >= 0; i--)
            {
                if (i == 0) // snake head
                {
                    switch (_direction)
                    {
                        case Direction.Down:
                            _snake[0].Y++;
                            break;
                        case Direction.Left:
                            _snake[0].X--;
                            break;
                        case Direction.Right:
                            _snake[0].X++;
                            break;
                        case Direction.Up:
                            _snake[0].Y--;
                            break;
                    }

                    SnakePart head = _snake[0];
                    // Check for out of bounds
                    checkForBounds(head);

                    // Check for collision with body
                    checkForCollisionWithBody(head);

                    // Check for collision with food
                    checkForCollisionWithFood(head);
                }
                else
                {
                    _snake[i].X = _snake[i - 1].X;
                    _snake[i].Y = _snake[i - 1].Y;
                }
            }
        }

        private void checkForCollisionWithFood(SnakePart head)
        {
            if (head.X == _food.X && head.Y == _food.Y)
            {
                SnakePart part = new SnakePart(_snake[_snake.Count - 1].X, _snake[_snake.Count - 1].Y);
                _snake.Add(part);
                generateFood();
                _score++;
                if (_snakeRate < 30)
                {
                    _snakeRate += 0.5f;
                    _snakeLoop.Interval = (int)(1000 / _snakeRate);
                }
            }
        }

        private void checkForCollisionWithBody(SnakePart head)
        {
            for (int j = 1; j < _snake.Count; j++)
            {
                if (head.X == _snake[j].X && head.Y == _snake[j].Y)
                {
                    gameover();
                }
            }
        }

        private void checkForBounds(SnakePart head)
        {
            if (head.X >= BOARD_WIDTH / 16 || head.X < 0 || head.Y >= BOARD_HEIGHT / 16 || head.Y < 0)
            {
                gameover();
            }
        }

        private void generateFood()
        {
            Random rnd = new Random();
            _food = new SnakePart(rnd.Next(0, 20), rnd.Next(0, 15));

            while (foodIsInvalid())
            {
                _food = new SnakePart(rnd.Next(0, 20), rnd.Next(0, 15));
            }
        }

        private bool foodIsInvalid()
        {
            return _snake.Any(snakePart => snakePart.X == _food.X && snakePart.Y == _food.Y);
        } 
        #endregion
    }
}
