using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using Rampart.Interfaces;
using Rampart.Actors;
using Rampart.HelperClasses;
using Rampart.Common;

namespace Rampart
{
    public class GameForm : Form
    {
        private const int FRAMES_PER_SECOND = 30;
        private const int FORM_WIDTH = 720;
        private const int FORM_HEIGHT = 480;
        private const int CELL_SIZE = 20;

        private System.Windows.Forms.Timer _timer;
        private List<IGameObject> _actors;
        private GameGrid _grid;
        private XBox _player1Crosshair;
        private Point _player1CrosshairGridLoc;

        public GameForm()
        {
            InitializeComponents();
            StartGame();
        }

        private void InitializeComponents()
        {
            // Form setup
            this.Width = FORM_WIDTH;
            this.Height = FORM_HEIGHT;

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque | ControlStyles.DoubleBuffer, true);
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.Text = "Rampart";
            this.BackColor = Color.Black;
            this.MaximizeBox = false;
            this.DoubleBuffered = true;
            this.KeyDown += GameForm_OnKeyDown;
            this.KeyUp += GameForm_OnKeyUp;

            // Timer setup
            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 1000 / FRAMES_PER_SECOND;
            _timer.Tick += Timer_Tick;
        }

        private void StartGame()
        {
            _actors = new List<IGameObject>();

            _grid = new GameGrid(FORM_WIDTH / CELL_SIZE, FORM_HEIGHT / CELL_SIZE, ClientRectangle);
            _grid.Visible = false;
            _grid.Pen = new Pen(Color.FromArgb(0x30, Color.White), 1f);

            _player1Crosshair = new XBox();
            _player1CrosshairGridLoc = new Point(0, 0);
            _player1Crosshair.Pen = new Pen(Color.GreenYellow, 1.0f);
            _player1Crosshair.Width = (int)_grid.CellWidth;
            _player1Crosshair.Height = (int)_grid.CellHeight;
            _player1Crosshair.SetPosition(_grid.GetCellLocation(_player1CrosshairGridLoc));

            for (int i = 0; i < 5; i++)
            {
                PlayerCannon cannon = new PlayerCannon(0);
                var cannonGridLoc = new Point(StaticApp.RandomNum.Next(0, _grid.CellNumHorizontal), StaticApp.RandomNum.Next(0, _grid.CellNumVertical));
                cannon.Pen = new Pen(Color.LawnGreen, 1.0f);
                cannon.Width = (int)_grid.CellWidth;
                cannon.Height = (int)_grid.CellHeight;
                cannon.SetPosition(_grid.GetCellLocation(cannonGridLoc));
                cannon.ObjectToPointAt = _player1Crosshair;
                cannon.CannonAutoSizeLength();

                _actors.Add(cannon);
            }

            _actors.Add(_grid);
            _actors.Add(_player1Crosshair);

            _timer.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            foreach (var actor in _actors)
                actor.Draw(e.Graphics, this.ClientRectangle);
        }

        private void Timer_Tick(object sender, EventArgs args)
        {
            this.Text = DateTime.Now.Ticks.ToString();

            BringOutYourDead();
            MoveActors();

            // Invalidate the form so OnPaint() gets called
            this.Invalidate();
        }

        private void BringOutYourDead()
        {
            _actors.RemoveAll(actor => (actor is IKillable && ((IKillable)actor).IsDead));
        }

        private void MoveActors()
        {
            foreach (var actor in _actors)
                actor.Move();
        }

        private void MoveCrosshair(int player, KeyDirection direction)
        {
            if (player == 0)
            {
                if (direction == KeyDirection.Up && (_player1CrosshairGridLoc.Y > 0))
                    _player1CrosshairGridLoc.Y--;
                else if (direction == KeyDirection.Down && (_player1CrosshairGridLoc.Y < _grid.CellNumVertical - 1))
                    _player1CrosshairGridLoc.Y++;
                else if (direction == KeyDirection.Left && (_player1CrosshairGridLoc.X > 0))
                    _player1CrosshairGridLoc.X--;
                else if (direction == KeyDirection.Right && (_player1CrosshairGridLoc.X < _grid.CellNumHorizontal - 1))
                    _player1CrosshairGridLoc.X++;

                _player1Crosshair.SetPosition(_grid.GetCellLocation(_player1CrosshairGridLoc));
            }
        }

        private void GameForm_OnKeyDown(object sender, KeyEventArgs args)
        {
            Console.WriteLine(string.Format("KeyDown: {0}", args.KeyCode));
            switch (args.KeyCode)
            {
                case Keys.Escape:
                    Exit();
                    break;
                case Keys.G:
                    _grid.Visible = !_grid.Visible;
                    break;
                case Keys.Space:
                    FireCannonBall(0);
                    break;
                case Keys.Up:
                    MoveCrosshair(0, KeyDirection.Up);
                    break;
                case Keys.Down:
                    MoveCrosshair(0, KeyDirection.Down);
                    break;
                case Keys.Left:
                    MoveCrosshair(0, KeyDirection.Left);
                    break;
                case Keys.Right:
                    MoveCrosshair(0, KeyDirection.Right);
                    break;
                default:
                    break;
            }
        }

        private void FireCannonBall(int player)
        {
            var cannons = GetCannonsForPlayer(player, true);
            foreach (var cannon in cannons)
            {
                var ball = new CannonBall(cannon.ID, _grid.CellSize, cannon.RealPosition, _player1Crosshair.RealPosition, cannon.CannonSpeed);
                cannon.HasFired = true;
                _actors.Add(ball);
                break;
            }
        }

        private IEnumerable<PlayerCannon> GetCannonsForPlayer(int player, bool onlyFreeCannons)
        {
            if (onlyFreeCannons)
                return _actors.Where(a => a is PlayerCannon && ((PlayerCannon)a).PlayerOwner == player && ((PlayerCannon)a).HasFired == false).Cast<PlayerCannon>();
            else
                return _actors.Where(a => a is PlayerCannon && ((PlayerCannon)a).PlayerOwner == player).Cast<PlayerCannon>();
        }

        private void GameForm_OnKeyUp(object sender, KeyEventArgs args)
        {
            //Console.WriteLine(string.Format("KeyUp: {0}", args.KeyCode));
            switch (args.KeyCode)
            {
                default:
                    break;
            }
        }

        private void Exit()
        {
            this.Close();
        }

    }
}
