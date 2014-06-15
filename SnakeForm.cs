using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Snake
{
    public enum Direction
    {
        Down,
        Left,
        Right,
        Up
    }
    public partial class SnakeForm : Form
    {
        private int _score = 0;
        private bool _gameover = false;
        private Direction _direction = Direction.Down;

        private List<SnakePart> _snake = new List<SnakePart>();
        private SnakePart _food;

        private Timer _gameLoop = new Timer();
        private Timer _snakeLoop = new Timer();

        private float _snakeRate = 2.0f;
        public SnakeForm()
        {
            InitializeComponent();

            _gameLoop.Tick += update;
            _snakeLoop.Tick += updateSnake;

            _gameLoop.Interval = 1000 / 60;
            _snakeLoop.Interval = (int)(1000 / _snakeRate);

            _gameLoop.Start();
            _snakeLoop.Start();

            startGame();
        }

        private void startGame()
        {
            _gameover = false;
            _snake.Clear();
            _score = 0;
            _snakeRate = 2.0f;

            _snakeLoop.Interval = (int)(1000 / _snakeRate);

            SnakePart head = new SnakePart(10, 8);
            _snake.Add(head);

            generateFood();
        }

        private void gameover()
        {
            _gameover = true;
        }

        private void update(object sender, EventArgs e)
        {
            if (_gameover)
            {
                if (Input.Press(Keys.Enter))
                {
                    startGame();
                }
            }
            else
            {
                if (Input.Press(Keys.Left))
                {
                    if (_snake.Count < 2 || _snake[0].X == _snake[1].X)
                    {
                        _direction = Direction.Left;
                    }
                }
                else if (Input.Press(Keys.Right))
                {
                    if (_snake.Count < 2 || _snake[0].X == _snake[1].X)
                    {
                        _direction = Direction.Right;
                    }
                }
                else if (Input.Press(Keys.Up))
                {
                    if (_snake.Count < 2 || _snake[0].Y == _snake[1].Y)
                    {
                        _direction = Direction.Up;
                    }
                }
                else if (Input.Press(Keys.Down))
                {
                    if (_snake.Count < 2 || _snake[0].Y == _snake[1].Y)
                    {
                        _direction = Direction.Down;
                    }
                }
            }
            pbCanvas.Invalidate();
        }

        private void updateSnake(object sender, EventArgs e)
        {
            if (!_gameover)
            {
                for (int i = _snake.Count - 1; i >= 0; i--)
                {
                    if (i == 0)
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
                            default:
                                break;
                        }

                        // Check for out of bounds
                        SnakePart head = _snake[0];
                        if (head.X >= 40 || head.X < 0 || head.Y >= 30 || head.Y < 0)
                        {
                            gameover();
                        }

                        // Check for collision with body
                        for (int j = 1; j < _snake.Count; j++)
                        {
                            if (head.X == _snake[j].X && head.Y == _snake[j].Y)
                            {
                                gameover();
                            }
                        }

                        // Check for collision with food
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
                    else
                    {
                        _snake[i].X = _snake[i - 1].X;
                        _snake[i].Y = _snake[i - 1].Y;
                    }
                }
            }
        }

        private void draw(Graphics canvas)
        {
            if (_gameover)
            {
                SizeF size = canvas.MeasureString("Game Over", Font);
                canvas.DrawString("Game Over", Font, Brushes.White, new PointF(320 - size.Width / 2, 160));
                size = canvas.MeasureString("Final Score " + _score.ToString(), Font);
                canvas.DrawString("Final Score " + _score.ToString(), Font, Brushes.White, new PointF(320 - size.Width / 2, 180));
                size = canvas.MeasureString("Press Enter to Start a New Game", Font);
                canvas.DrawString("Press Enter to Start a New Game", Font, Brushes.White, new PointF(320 - size.Width / 2, 200));
            }else{
                canvas.DrawString("Score " + _score.ToString(), Font, Brushes.White, new PointF(4, 4));
                
                for (int i = 0; i < _snake.Count; i++)
                {
                    Color snakeColor = (i == 0) ? Color.Red : Color.Black;
                    SnakePart currentPart = _snake[i];
                    canvas.FillRectangle(new SolidBrush(snakeColor), new Rectangle(currentPart.X * 16, currentPart.Y * 16, 16, 16));
                }

                canvas.FillRectangle(new SolidBrush(Color.Orange), new Rectangle(_food.X * 16, _food.Y * 16, 16, 16));
            }
        }

        private void generateFood()
        {
            Random rnd = new Random();
            _food = new SnakePart(rnd.Next(0, 40), rnd.Next(0, 30));
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
            draw(e.Graphics);
        }
    }
}
