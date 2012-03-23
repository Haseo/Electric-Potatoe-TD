using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace Tetris
{
    class Game
    {
        enum Type_piece
        {
            TypeI = 0,
            TypeO = 1,
            TypeT = 2,
            TypeL = 3,
            TypeJ = 4,
            TypeZ = 5,
            TypeS = 6,
        };

        enum Direction_piece
        {
            Pos1,
            Pos2,
            Pos3,
            Pos4,
        };

        enum Direction
        {
            Down,
            Left,
            Right
        };
        
        enum Flip
        {
            Left,
            Right
        };

        struct piece
        {
            public int x;
            public int y;
            public Tetris_color color;
            public Direction_piece statut;
            public Type_piece type;
            public piece(int x1, int y1, Tetris_color col, Direction_piece stat, Type_piece typ)
            {
                x = x1;
                y = y1;
                color = col;
                statut = stat;
                type = typ;
            }
            public piece(piece copy)
            {
                x = copy.x;
                y = copy.y;
                color = copy.color;
                statut = copy.statut;
                type = copy.type;
            }
        }

        Game1 _origin;
        SpriteFont Font1;
        SpriteFont Font2;
        Texture2D Carre;
        Texture2D Cadre;
        Texture2D _Down;
        Texture2D _Left;
        Texture2D _Right;
        Texture2D _FlipLeft;
        Texture2D _FlipRight;
        Rectangle[] _landscape;
        Rectangle[] _portrait;
        Tetris_color[,] _map;
        Dictionary<Tetris_color, Color> ArrayColor;
        Dictionary<Type_piece, piece[]> all_piece;
        piece[] current;
        Type_piece next_piece;
        Random _random;
        int _temp;
        int _X;
        int _Y;
        int _score;
        int _lvl;
        int _flag;
        bool _end;
        bool _pause;

        public Game(Game1 origin)
         {
             _origin = origin;
             _X = (_origin.graphics.PreferredBackBufferWidth / 2);
             _Y = (_origin.graphics.PreferredBackBufferHeight / 2);
             _map = new Tetris_color[12, 25];
             _random = new Random();
            new KeyValuePair<int, int>(0, 0);
            new Dictionary<KeyValuePair<int, int>, Tetris_color>();
            current = new piece[4];
            init_map();
            init_colors();
            init_list_of_piece();
            _score = 0;
            _lvl = 1;
            _flag = 0;
            _temp = 0;
            _end = false;
            _pause = false;
            choose_next_piece();
            for (int i = 0; i < 4; i++)
                current[i] = new piece(all_piece[next_piece][i]);
            choose_next_piece();
         }

        public void init_map()
        {
            int i = 0;
            while (i < 12)
            {
                int j = 0;
                while (j < 25)
                    _map[i, j++] = Tetris_color.TWhite;
                i++;
            }
        }

        private void init_colors()
        {
            ArrayColor = new Dictionary<Tetris_color, Color>();
            ArrayColor[Tetris_color.TWhite] = Color.White;
            ArrayColor[Tetris_color.TBlack] = Color.Black;
            ArrayColor[Tetris_color.TRed] = Color.Red;
            ArrayColor[Tetris_color.TBlue] = Color.Blue;
            ArrayColor[Tetris_color.TCyan] = Color.Cyan;
            ArrayColor[Tetris_color.TGreen] = Color.Green;
            ArrayColor[Tetris_color.TOrange] = Color.Orange;
            ArrayColor[Tetris_color.TMagenta] = Color.Magenta;
            ArrayColor[Tetris_color.TYellow] = Color.Yellow;
            ArrayColor[Tetris_color.TBrun] = Color.Brown;
        }

        private void init_list_of_piece()
        {
            all_piece = new Dictionary<Type_piece, piece[]>();
            all_piece[Type_piece.TypeI] = new piece[]
            {
                new piece(0, 5, Tetris_color.TRed, Direction_piece.Pos1, Type_piece.TypeI),
                new piece(1, 5, Tetris_color.TRed, Direction_piece.Pos1, Type_piece.TypeI),
                new piece(2, 5, Tetris_color.TRed, Direction_piece.Pos1, Type_piece.TypeI),
                new piece(3, 5, Tetris_color.TRed, Direction_piece.Pos1, Type_piece.TypeI),
            };
            all_piece[Type_piece.TypeO] = new piece[]
            {
                new piece(0, 5, Tetris_color.TBlue, Direction_piece.Pos1, Type_piece.TypeO),
                new piece(0, 6, Tetris_color.TBlue, Direction_piece.Pos1, Type_piece.TypeO),
                new piece(1, 5, Tetris_color.TBlue, Direction_piece.Pos1, Type_piece.TypeO),
                new piece(1, 6, Tetris_color.TBlue, Direction_piece.Pos1, Type_piece.TypeO),
            };
            all_piece[Type_piece.TypeT] = new piece[]
            {
                new piece(0, 4, Tetris_color.TBrun, Direction_piece.Pos1, Type_piece.TypeT),
                new piece(0, 5, Tetris_color.TBrun, Direction_piece.Pos1, Type_piece.TypeT),
                new piece(0, 6, Tetris_color.TBrun, Direction_piece.Pos1, Type_piece.TypeT),
                new piece(1, 5, Tetris_color.TBrun, Direction_piece.Pos1, Type_piece.TypeT),
            };
            all_piece[Type_piece.TypeL] = new piece[]
            {
                new piece(0, 4, Tetris_color.TMagenta, Direction_piece.Pos1, Type_piece.TypeL),
                new piece(0, 5, Tetris_color.TMagenta, Direction_piece.Pos1, Type_piece.TypeL),
                new piece(0, 6, Tetris_color.TMagenta, Direction_piece.Pos1, Type_piece.TypeL),
                new piece(1, 4, Tetris_color.TMagenta, Direction_piece.Pos1, Type_piece.TypeL),
            };
            all_piece[Type_piece.TypeJ] = new piece[]
            {
                new piece(0, 4, Tetris_color.TOrange, Direction_piece.Pos1, Type_piece.TypeJ),
                new piece(0, 5, Tetris_color.TOrange, Direction_piece.Pos1, Type_piece.TypeJ),
                new piece(0, 6, Tetris_color.TOrange, Direction_piece.Pos1, Type_piece.TypeJ),
                new piece(1, 6, Tetris_color.TOrange, Direction_piece.Pos1, Type_piece.TypeJ),
            };
            all_piece[Type_piece.TypeZ] = new piece[]
            {
                new piece(0, 4, Tetris_color.TGreen, Direction_piece.Pos1, Type_piece.TypeZ),
                new piece(0, 5, Tetris_color.TGreen, Direction_piece.Pos1, Type_piece.TypeZ),
                new piece(1, 5, Tetris_color.TGreen, Direction_piece.Pos1, Type_piece.TypeZ),
                new piece(1, 6, Tetris_color.TGreen, Direction_piece.Pos1, Type_piece.TypeZ),
            };
            all_piece[Type_piece.TypeS] = new piece[]
            {
                new piece(1, 5, Tetris_color.TCyan, Direction_piece.Pos1, Type_piece.TypeS),
                new piece(1, 6, Tetris_color.TCyan, Direction_piece.Pos1, Type_piece.TypeS),
                new piece(0, 6, Tetris_color.TCyan, Direction_piece.Pos1, Type_piece.TypeS),
                new piece(0, 7, Tetris_color.TCyan, Direction_piece.Pos1, Type_piece.TypeS),
            };
        }

        private void choose_next_piece()
        {
            int i =  _random.Next(7);

            next_piece = (Type_piece)i;
        }

        private void Pause()
        {
            _pause = true;
        }

        private void Resume()
        {
            _pause = false;
        }

        private void change_piece()
        {
            foreach (piece item in current)
                _map[item.y, item.x] = item.color;
            test_line();
            for (int i = 0; i < 4; i++)
                current[i] = new piece(all_piece[next_piece][i]);
            choose_next_piece();
            if (test_pos() == false)
              end();
        }

        private bool test_pos()
        {
            foreach (piece item in current)
            {
                if (_map[item.y, item.x] != Tetris_color.TWhite)
                    return (false);
            }
            return (true);
        }

        private void end()
        {
            _end = true;
        }

        private void erase_line(int target)
        {
            bool flag = false;

            int i = 0;
            while (i < 4)
            {
                if (current[i].x == target)
                    current[i].x = -1;
                else if (current[i].x < target)
                    current[i].x++;
                i++;
            }
            while (target > 0 && flag == false)
            {
                flag = true;
                int j = 0;
                while (j < 12)
                {
                    _map[j, target] = _map[j, target - 1];
                    if (_map[j, target] != Tetris_color.TWhite)
                        flag = false;
                    j++;
                }
                target--;
            }
            if (target == 0)
            {
                int j = 0;
                while (j < 12)
                    _map[j++, 0] = Tetris_color.TWhite;
            }
        }

        private void test_line()
        {
            int line_check = 0;
            int i = 0;

            while (i < 4)
            {
                int j = 0;
                bool test = true;

                if (current[i].x != -1)
                {
                    while (j < 12)
                        if (_map[j++, current[i].x] == Tetris_color.TWhite)
                            test = false;
                    if (test == true)
                    {
                        erase_line(current[i].x);
                        line_check++;
                    }
                }
                i++;
            }
            if (line_check > 0)
                calc_score(line_check);
        }

        private void calc_score(int line_check)
        {
            switch (line_check)
            {
                case 1:
                    add_score(40 * _lvl);
                    break;
                case 2:
                    add_score(100 * _lvl);
                    break;
                case 3:
                    add_score(300 * _lvl);
                    break;
                default:
                    add_score(1200 * _lvl);
                    break;
            }
        }

        private void add_score(int i)
        {
            _score += i;
        }

        private void change_level()
        {
            _lvl++;
        }

        private void change_level(int lvl)
        {
            _lvl = lvl;
        }

        public void Initialize()
        {

        }

        public void LoadContent()
        {
            Font1 = _origin.Content.Load<SpriteFont>("GameFont1");
            Font2 = _origin.Content.Load<SpriteFont>("GameFont2");
            Carre = _origin.Content.Load<Texture2D>("carre");
            Cadre = _origin.Content.Load<Texture2D>("Cadre");
            _Down = _origin.Content.Load<Texture2D>("Down");
            _Left = _origin.Content.Load<Texture2D>("Left");
            _Right = _origin.Content.Load<Texture2D>("Right");
            _FlipLeft = _origin.Content.Load<Texture2D>("Flip_Left");
            _FlipRight = _origin.Content.Load<Texture2D>("Flip_Right");
            _landscape = new Rectangle[]
             {
                 new Rectangle(0, 0, (_X * 8 / 5), (_Y * 7 / 5)),
                 new Rectangle(0, 0, (_X * 8 / 5), (_Y * 7 / 5)),
                 new Rectangle(0, 0, (_X * 8 / 5), (_Y * 7 / 5)),
                 new Rectangle(0, 0, (_X * 8 / 5), (_Y * 7 / 5)),
                 new Rectangle(0, 0, (_X * 8 / 5), (_Y * 7 / 5)),
                 new Rectangle(0, 0, (_X * 8 / 5), (_Y * 7 / 5)),
                 new Rectangle(0, 0, (_X * 8 / 5), (_Y * 7 / 5)),
                 new Rectangle(0, 0, (_X * 8 / 5), (_Y * 7 / 5)),
             };
            // A Configurer :: 
            _portrait = new Rectangle[]
             {
                 new Rectangle((_Y * 9 / 6) - 5, (_X - (_X * 11 / 12)), (_Y  / 2), (_X / 5)),
                 new Rectangle((_Y * 9 / 6) - 5, (_X - (_X * 8 / 12)), (_Y  / 2), (_X / 5)),
                 new Rectangle((_Y * 9 / 6) - 5, (_X - (_X * 5 / 12)), (_Y  / 2), (_X / 5)),
                 new Rectangle((_Y * 9 / 6) - 5, (_X - (_X * 2 / 12)), 10, 10),
                 new Rectangle((_Y * 9 / 6) - 5, (_X + (_X * 3 / 12)), 10, 10),
                 new Rectangle((_Y * 9 / 6) - 5, (_X + (_X * 4 / 12)), 10, 10),
                 new Rectangle(10, ((_X * 2) - (_X / 5)), (_Y  / 3), (_X / 6)),
                 new Rectangle(20 + (_Y / 3), ((_X * 2) - (_X / 5)), (_Y / 3), (_X / 6)),
                 new Rectangle(30 + (_Y * 2 / 3), ((_X * 2) - (_X / 5)), (_Y / 3), (_X / 6)),
                 new Rectangle(40 + (_Y * 3 / 3), ((_X * 2) - (_X / 5)), (_Y / 3), (_X / 6)),
                 new Rectangle(50 + (_Y * 4 / 3), ((_X * 2) - (_X / 5)), (_Y / 3), (_X / 6)),
             };
        }

        public void UnloadContent()
        {
        }

        private bool move_piece(Direction dir)
        {
            int i = 0;
            if (can_move_piece(dir) == false)
                return (false);
            switch (dir)
            {
                case Direction.Down:
                    while (i < 4)
                        current[i++].x++;
                    break;
                case Direction.Left:
                    while (i < 4)
                        current[i++].y--;
                    break;
                case Direction.Right:
                    while (i < 4)
                        current[i++].y++;
                    break;
            }
            return (true);
        }

        private bool can_move_piece(Direction dir)
        {
            int i = 0;
            switch (dir)
            {
                case Direction.Down:
                    while (i < 4)
                    {
                        if ((current[i].x + 1) >= 25)
                            return (false);
                        if (_map[current[i].y, current[i].x + 1] != Tetris_color.TWhite)
                            return (false);
                        i++;
                    }
                    break;
                case Direction.Left:
                    while (i < 4)
                    {
                        if ((current[i].y - 1) < 0)
                            return (false);
                        if (_map[current[i].y - 1, current[i].x] != Tetris_color.TWhite)
                            return (false);
                        i++;
                    }
                    break;
                case Direction.Right:
                    while (i < 4)
                    {
                        if ((current[i].y + 1) >= 12)
                            return (false);
                        if (_map[current[i].y + 1, current[i].x] != Tetris_color.TWhite)
                            return (false);
                        i++;
                    }
                    break;
            }
            return (true);
        }

        private bool test_pos(int x, int y)
        {
            if (x < 0 || y < 0 ||  x >= 25 || y >= 12)
                return (false);
            if (_map[y, x] != Tetris_color.TWhite)
                return (false);
            return (true);
        }

        private bool flip_pos1()
        {
            bool to_return = false;

            switch (current[0].type)
            {
                case Type_piece.TypeI:
                        if (current[0].statut == Direction_piece.Pos1 || current[0].statut == Direction_piece.Pos3)
                            to_return = true;
                        if (test_pos(current[1].x - 1, current[1].y) == true && test_pos(current[1].x + 1, current[1].y) == true &&
                            test_pos(current[1].x + 2, current[1].y) == true &&
                            (current[0].statut == Direction_piece.Pos2 || current[0].statut == Direction_piece.Pos4))
                        {
                            current[0].y = current[1].y;
                            current[2].y = current[1].y;
                            current[3].y = current[1].y;

                            current[0].x = current[1].x - 1;
                            current[2].x = current[1].x + 1;
                            current[3].x = current[1].x + 2;
                            to_return = true;
                        }
                    break;
                case Type_piece.TypeO:
                    to_return = true;
                    break;
                case Type_piece.TypeT:
                    if (current[0].statut == Direction_piece.Pos1 || current[0].statut == Direction_piece.Pos3)
                        to_return = true;
                    if (test_pos(current[1].x + 1, current[1].y) == true && test_pos(current[1].x - 1, current[1].y) == true &&
                        test_pos(current[1].x, current[1].y + 1) == true &&
                        (current[0].statut == Direction_piece.Pos2 || current[0].statut == Direction_piece.Pos4))
                    {
                        current[0].y = current[1].y;
                        current[2].y = current[1].y;
                        current[3].y = current[1].y + 1;

                        current[0].x = current[1].x + 1;
                        current[2].x = current[1].x - 1;
                        current[3].x = current[1].x;
                        to_return = true;
                    }
                    break;
                case Type_piece.TypeL:
                    break;
                case Type_piece.TypeJ:
                    break;
                case Type_piece.TypeZ:
                    if (current[0].statut == Direction_piece.Pos1 || current[0].statut == Direction_piece.Pos3)
                        to_return = true;
                    if (test_pos(current[1].x + 1, current[1].y) == true && test_pos(current[1].x, current[1].y + 1) == true &&
                        test_pos(current[1].x - 1, current[1].y + 1) == true &&
                        (current[0].statut == Direction_piece.Pos2 || current[0].statut == Direction_piece.Pos4))
                    {
                        current[0].y = current[1].y;
                        current[2].y = current[1].y + 1;
                        current[3].y = current[1].y + 1;

                        current[0].x = current[1].x + 1;
                        current[2].x = current[1].x;
                        current[3].x = current[1].x - 1;
                        to_return = true;
                    }
                    break;
                case Type_piece.TypeS:
                    if (current[0].statut == Direction_piece.Pos1 || current[0].statut == Direction_piece.Pos3)
                        to_return = true;
                    if (test_pos(current[2].x + 1, current[2].y + 1) == true && test_pos(current[2].x, current[2].y + 1) == true &&
                        test_pos(current[2].x - 1, current[2].y) == true &&
                        (current[0].statut == Direction_piece.Pos2 || current[0].statut == Direction_piece.Pos4))
                    {
                        current[0].y = current[2].y + 1;
                        current[1].y = current[2].y + 1;
                        current[3].y = current[2].y;

                        current[0].x = current[2].x + 1;
                        current[1].x = current[2].x;
                        current[3].x = current[2].x - 1;
                        to_return = true;
                    }
                    break;
            }
            if (to_return == true)
                current[0].statut = Direction_piece.Pos1;
            return (to_return);
        }

        private bool flip_pos2()
        {
            bool to_return = false;

            switch (current[0].type)
            {
                case Type_piece.TypeI:
                    if (current[0].statut == Direction_piece.Pos2 || current[0].statut == Direction_piece.Pos4)
                        to_return = true;
                    if (test_pos(current[1].x, current[1].y + 1) == true && test_pos(current[1].x, current[1].y - 1) == true &&
                        test_pos(current[1].x, current[1].y - 2) == true &&
                        (current[0].statut == Direction_piece.Pos1 || current[0].statut == Direction_piece.Pos3))
                    {
                        current[0].y = current[1].y + 1;
                        current[2].y = current[1].y - 1;
                        current[3].y = current[1].y - 2;

                        current[0].x = current[1].x;
                        current[2].x = current[1].x;
                        current[3].x = current[1].x;
                        to_return = true;
                    }
                    break;
                case Type_piece.TypeO:
                    to_return = true;
                    break;
                case Type_piece.TypeT:
                    if (current[0].statut == Direction_piece.Pos2 || current[0].statut == Direction_piece.Pos4)
                        to_return = true;
                    if (test_pos(current[1].x - 1, current[1].y) == true && test_pos(current[1].x + 1, current[1].y) == true &&
                        test_pos(current[1].x, current[1].y - 1) == true &&
                        (current[0].statut == Direction_piece.Pos1 || current[0].statut == Direction_piece.Pos3))
                    {
                        current[0].y = current[1].y;
                        current[2].y = current[1].y;
                        current[3].y = current[1].y - 1;

                        current[0].x = current[1].x - 1;
                        current[2].x = current[1].x + 1;
                        current[3].x = current[1].x;
                        to_return = true;
                    }
                    break;
                case Type_piece.TypeL:
                    break;
                case Type_piece.TypeJ:
                    break;
                case Type_piece.TypeZ:
                    if (current[0].statut == Direction_piece.Pos2 || current[0].statut == Direction_piece.Pos4)
                        to_return = true;
                    if (test_pos(current[1].x, current[1].y - 1) == true && test_pos(current[1].x + 1, current[1].y) == true &&
                        test_pos(current[1].x + 1, current[1].y + 1) == true &&
                        (current[0].statut == Direction_piece.Pos1 || current[0].statut == Direction_piece.Pos3))
                    {
                        current[0].y = current[1].y - 1;
                        current[2].y = current[1].y;
                        current[3].y = current[1].y + 1;

                        current[0].x = current[1].x;
                        current[2].x = current[1].x + 1;
                        current[3].x = current[1].x + 1;
                        to_return = true;
                    }
                    break;
                case Type_piece.TypeS:
                    if (current[0].statut == Direction_piece.Pos2 || current[0].statut == Direction_piece.Pos4)
                        to_return = true;
                    if (test_pos(current[2].x + 1, current[2].y - 1) == true && test_pos(current[2].x + 1, current[2].y) == true &&
                        test_pos(current[2].x, current[2].y + 1) == true &&
                        (current[0].statut == Direction_piece.Pos1 || current[0].statut == Direction_piece.Pos3))
                    {
                        current[0].y = current[2].y - 1;
                        current[1].y = current[2].y;
                        current[3].y = current[2].y + 1;

                        current[0].x = current[2].x + 1;
                        current[1].x = current[2].x + 1;
                        current[3].x = current[2].x;
                        to_return = true;
                    }
                    break;
            }
            if (to_return == true)
                current[0].statut = Direction_piece.Pos2;
            return (to_return);
        }

        private bool flip_pos3()
        {
            bool to_return = false;

            switch (current[0].type)
            {
                case Type_piece.TypeI:
                    if (current[0].statut == Direction_piece.Pos1 || current[0].statut == Direction_piece.Pos3)
                        to_return = true;
                    if (test_pos(current[1].x - 1, current[0].y) == true && test_pos(current[1].x + 1, current[0].y) == true &&
                        test_pos(current[1].x + 2, current[0].y) == true &&
                        (current[0].statut == Direction_piece.Pos2 || current[0].statut == Direction_piece.Pos4))
                    {
                        current[0].y = current[1].y;
                        current[2].y = current[1].y;
                        current[3].y = current[1].y;

                        current[0].x = current[1].x - 1;
                        current[2].x = current[1].x + 1;
                        current[3].x = current[1].x + 2;
                        to_return = true;
                    }
                    break;
                case Type_piece.TypeO:
                    to_return = true;
                    break;
                case Type_piece.TypeT:
                    if (current[0].statut == Direction_piece.Pos1 || current[0].statut == Direction_piece.Pos3)
                        to_return = true;
                    if (test_pos(current[1].x, current[1].y + 1) == true && test_pos(current[1].x, current[1].y - 1) == true &&
                        test_pos(current[1].x - 1, current[1].y) == true &&
                        (current[0].statut == Direction_piece.Pos2 || current[0].statut == Direction_piece.Pos4))
                    {
                        current[0].y = current[1].y + 1;
                        current[2].y = current[1].y - 1;
                        current[3].y = current[1].y;

                        current[0].x = current[1].x;
                        current[2].x = current[1].x;
                        current[3].x = current[1].x - 1;
                        to_return = true;
                    }
                    break;
                case Type_piece.TypeL:
                    break;
                case Type_piece.TypeJ:
                    break;
                case Type_piece.TypeZ:
                    if (current[0].statut == Direction_piece.Pos1 || current[0].statut == Direction_piece.Pos3)
                        to_return = true;
                    if (test_pos(current[1].x + 1, current[1].y) == true && test_pos(current[1].x, current[1].y + 1) == true &&
                        test_pos(current[1].x - 1, current[1].y + 1) == true &&
                        (current[0].statut == Direction_piece.Pos2 || current[0].statut == Direction_piece.Pos4))
                    {
                        current[0].y = current[1].y;
                        current[2].y = current[1].y + 1;
                        current[3].y = current[1].y + 1;

                        current[0].x = current[1].x + 1;
                        current[2].x = current[1].x;
                        current[3].x = current[1].x - 1;
                        to_return = true;
                    }
                    break;
                case Type_piece.TypeS:
                    if (current[0].statut == Direction_piece.Pos1 || current[0].statut == Direction_piece.Pos3)
                        to_return = true;
                    if (test_pos(current[2].x + 1, current[2].y + 1) == true && test_pos(current[2].x, current[2].y + 1) == true &&
                        test_pos(current[2].x - 1, current[2].y) == true &&
                        (current[0].statut == Direction_piece.Pos2 || current[0].statut == Direction_piece.Pos4))
                    {
                        current[0].y = current[2].y + 1;
                        current[1].y = current[2].y + 1;
                        current[3].y = current[2].y;

                        current[0].x = current[2].x + 1;
                        current[1].x = current[2].x;
                        current[3].x = current[2].x - 1;
                        to_return = true;
                    }
                    break;
            }
            if (to_return == true)
                current[0].statut = Direction_piece.Pos3;
            return (to_return);
        }

        private bool flip_pos4()
        {
            bool to_return = false;

            switch (current[0].type)
            {
                case Type_piece.TypeI:
                    if (current[0].statut == Direction_piece.Pos2 || current[0].statut == Direction_piece.Pos4)
                        to_return = true;
                    if (test_pos(current[1].x, current[0].y + 1) == true && test_pos(current[1].x, current[0].y - 1) == true &&
                        test_pos(current[1].x, current[0].y - 2) == true &&
                        (current[0].statut == Direction_piece.Pos1 || current[0].statut == Direction_piece.Pos3))
                    {
                        current[0].y = current[1].y + 1;
                        current[2].y = current[1].y - 1;
                        current[3].y = current[1].y - 2;

                        current[0].x = current[1].x;
                        current[2].x = current[1].x;
                        current[3].x = current[1].x;
                        to_return = true;
                    }
                    break;
                case Type_piece.TypeO:
                    to_return = true;
                    break;
                case Type_piece.TypeT:
                    if (current[0].statut == Direction_piece.Pos2 || current[0].statut == Direction_piece.Pos4)
                        to_return = true;
                    if (test_pos(current[1].x, current[1].y - 1) == true && test_pos(current[1].x, current[1].y + 1) == true &&
                        test_pos(current[1].x + 1, current[1].y) == true &&
                        (current[0].statut == Direction_piece.Pos1 || current[0].statut == Direction_piece.Pos3))
                    {
                        current[0].y = current[1].y - 1;
                        current[2].y = current[1].y + 1;
                        current[3].y = current[1].y;

                        current[0].x = current[1].x;
                        current[2].x = current[1].x;
                        current[3].x = current[1].x + 1;
                        to_return = true;
                    }
                    break;
                case Type_piece.TypeL:
                    break;
                case Type_piece.TypeJ:
                    break;
                case Type_piece.TypeZ:
                    if (current[0].statut == Direction_piece.Pos2 || current[0].statut == Direction_piece.Pos4)
                        to_return = true;
                    if (test_pos(current[1].x, current[1].y - 1) == true && test_pos(current[1].x + 1, current[1].y) == true &&
                        test_pos(current[1].x + 1, current[1].y + 1) == true &&
                        (current[0].statut == Direction_piece.Pos1 || current[0].statut == Direction_piece.Pos3))
                    {
                        current[0].y = current[1].y - 1;
                        current[2].y = current[1].y;
                        current[3].y = current[1].y + 1;

                        current[0].x = current[1].x;
                        current[2].x = current[1].x + 1;
                        current[3].x = current[1].x + 1;
                        to_return = true;
                    }
                    break;
                case Type_piece.TypeS:
                    if (current[0].statut == Direction_piece.Pos2 || current[0].statut == Direction_piece.Pos4)
                        to_return = true;
                    if (test_pos(current[2].x + 1, current[2].y - 1) == true && test_pos(current[2].x + 1, current[2].y) == true &&
                        test_pos(current[2].x, current[2].y + 1) == true &&
                        (current[0].statut == Direction_piece.Pos1 || current[0].statut == Direction_piece.Pos3))
                    {
                        current[0].y = current[2].y - 1;
                        current[1].y = current[2].y;
                        current[3].y = current[2].y + 1;

                        current[0].x = current[2].x + 1;
                        current[1].x = current[2].x + 1;
                        current[3].x = current[2].x;
                        to_return = true;
                    }
                    break;
            }
            if (to_return == true)
                current[0].statut = Direction_piece.Pos4;
            return (to_return);
        }

        private bool flip_piece(Flip dir)
        {
            switch (dir)
            {
                case Flip.Left :
                    switch (current[0].statut)
                    {
                        case Direction_piece.Pos1:
                            return (flip_pos4());
                        case Direction_piece.Pos2:
                            return (flip_pos1());
                        case Direction_piece.Pos3:
                            return (flip_pos2());
                        case Direction_piece.Pos4:
                            return (flip_pos3());
                    }
                    break;
                case Flip.Right:
                    switch (current[0].statut)
                    {
                        case Direction_piece.Pos1:
                            return (flip_pos2());
                        case Direction_piece.Pos2:
                            return (flip_pos3());
                        case Direction_piece.Pos3:
                            return (flip_pos4());
                        case Direction_piece.Pos4:
                            return (flip_pos1());
                    }
                    break;
            }
            return (true);
        }
        
        public void Update(GameTime gameTime, DisplayOrientation orientation)
        {
            if (_end == true)
            {
                switch (orientation)
                {
                    case DisplayOrientation.LandscapeLeft:
                        Update_End(_landscape);
                        break;
                    case DisplayOrientation.LandscapeRight:
                        Update_End(_landscape);
                        break;
                    case DisplayOrientation.Portrait:
                        Update_End(_portrait);
                        break;
                    case DisplayOrientation.Default:
                        Update_End(_portrait);
                        break;
                }
                return;
            }
            switch (orientation)
            {
                case DisplayOrientation.LandscapeLeft:
                    Update_Content(_landscape);
                    break;
                case DisplayOrientation.LandscapeRight:
                    Update_Content(_landscape);
                    break;
                case DisplayOrientation.Portrait:
                    Update_Content(_portrait);
                    break;
                case DisplayOrientation.Default:
                    Update_Content(_portrait);
                    break;
            }
            if (_pause == false)
            {
                if (_flag >= (15 / _lvl))
                {
                    if (move_piece(Direction.Down) == false)
                        change_piece();
                    _flag = 0;
                }
                else
                {
                    _flag++;
                }
            }
        }

        public void Update_Content(Rectangle[] array)
        {
                TouchPanelCapabilities touchCap = TouchPanel.GetCapabilities();
                if (touchCap.IsConnected)
                {
                    TouchCollection touches = TouchPanel.GetState();
                    if (touches.Count >= 1)
                    {
                       if (touches[0].State == TouchLocationState.Pressed || _temp >= 6)
                        {
                            Vector2 PositionTouch = touches[0].Position;

                            if (_pause == false)
                            {
                                if ((PositionTouch.X >= array[6].X && PositionTouch.X <= (array[6].X + array[6].Width)) &&
                                    (PositionTouch.Y >= array[6].Y && PositionTouch.Y <= (array[6].Y + array[6].Height)))
                                {
                                    move_piece(Direction.Left);
                                }
                                if ((PositionTouch.X >= array[7].X && PositionTouch.X <= (array[7].X + array[7].Width)) &&
                                    (PositionTouch.Y >= array[7].Y && PositionTouch.Y <= (array[7].Y + array[7].Height)))
                                {
                                    move_piece(Direction.Down);
                                }
                                if ((PositionTouch.X >= array[8].X && PositionTouch.X <= (array[8].X + array[8].Width)) &&
                                    (PositionTouch.Y >= array[8].Y && PositionTouch.Y <= (array[8].Y + array[8].Height)))
                                {
                                    move_piece(Direction.Right);
                                }
                                if ((PositionTouch.X >= array[9].X && PositionTouch.X <= (array[9].X + array[9].Width)) &&
                                    (PositionTouch.Y >= array[9].Y && PositionTouch.Y <= (array[9].Y + array[9].Height)))
                                {
                                    flip_piece(Flip.Left);
                                }
                                if ((PositionTouch.X >= array[10].X && PositionTouch.X <= (array[10].X + array[10].Width)) &&
                                    (PositionTouch.Y >= array[10].Y && PositionTouch.Y <= (array[10].Y + array[10].Height)))
                                {
                                    flip_piece(Flip.Right);
                                }
                            }
                            if ((PositionTouch.X >= array[0].X && PositionTouch.X <= (array[0].X + array[0].Width)) &&
                                (PositionTouch.Y >= array[0].Y && PositionTouch.Y <= (array[0].Y + array[0].Height)))
                            {
                                _origin.setStatut(Statut.Menu, false);
                            }
                            if ((PositionTouch.X >= array[1].X && PositionTouch.X <= (array[1].X + array[1].Width)) &&
                                (PositionTouch.Y >= array[1].Y && PositionTouch.Y <= (array[1].Y + array[1].Height)))
                            {
                                if (_pause == true)
                                    Resume();
                                else
                                    Pause();
                            }
                            if ((PositionTouch.X >= array[2].X && PositionTouch.X <= (array[2].X + array[2].Width)) &&
                                (PositionTouch.Y >= array[2].Y && PositionTouch.Y <= (array[2].Y + array[2].Height)))
                            {
                                Restart();
                            }
                            _temp = 0;
                        }

                    }
                }
                _temp++;
            }
 
        public void Update_End(Rectangle[] array)
        {
            TouchPanelCapabilities touchCap = TouchPanel.GetCapabilities();

            if (touchCap.IsConnected)
            {
                TouchCollection touches = TouchPanel.GetState();
                if (touches.Count >= 1)
                {
                    /* Gestion Statut
                     * touches[0].State 
                     * TouchLocationState.Invalid
                     * TouchLocationState.Moved
                     * TouchLocationState.Pressed
                     * TouchLocationState.Released
                     */
                    if (touches[0].State == TouchLocationState.Pressed)
                    {
                        Vector2 PositionTouch = touches[0].Position;
                        if ((PositionTouch.X >= array[0].X && PositionTouch.X <= (array[0].X + array[0].Width)) &&
                        (PositionTouch.Y >= array[0].Y && PositionTouch.Y <= (array[0].Y + array[0].Height)))
                        {
                            _origin.setStatut(Statut.Menu, false);
                        }
                        if ((PositionTouch.X >= array[2].X && PositionTouch.X <= (array[2].X + array[2].Width)) &&
                            (PositionTouch.Y >= array[2].Y && PositionTouch.Y <= (array[2].Y + array[2].Height)))
                        {
                            Restart();
                        }

                    }
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, DisplayOrientation orientation)
        {
            switch (orientation)
            {
                case DisplayOrientation.LandscapeLeft:
                    Draw_Content(spriteBatch, _landscape, orientation);
                    break;
                case DisplayOrientation.LandscapeRight:
                    Draw_Content(spriteBatch, _landscape, orientation);
                    break;
                case DisplayOrientation.Portrait:
                    Draw_Content(spriteBatch, _portrait, orientation);
                    break;
                case DisplayOrientation.Default:
                    Draw_Content(spriteBatch, _portrait, orientation);
                    break;
            }
            if (_end == true)
                Draw_Game_over(spriteBatch, orientation);
        }

        private void Draw_Game_over(SpriteBatch spriteBatch, DisplayOrientation orientation)
        {
            if (orientation == DisplayOrientation.LandscapeLeft || orientation == DisplayOrientation.LandscapeRight)
            {
                spriteBatch.DrawString(Font2, "Game Over", new Vector2(_X - 150, _Y - 50), Color.Black);
                spriteBatch.DrawString(Font2, "Score : ", new Vector2(_X - 150, _Y), Color.Black);
                spriteBatch.DrawString(Font2, _score.ToString(), new Vector2(_X - 150, _Y + 50), Color.Black);
            }
            else
            {
                spriteBatch.DrawString(Font2, "Game Over", new Vector2(_Y - 150, _X - 50), Color.Black);
                spriteBatch.DrawString(Font2, "Score :", new Vector2(_Y - 150, _X), Color.Black);
                spriteBatch.DrawString(Font2, _score.ToString(), new Vector2(_Y - 150, _X + 50), Color.Black);
            }
        }

        private void Draw_Content(SpriteBatch spriteBatch, Rectangle[] array, DisplayOrientation orientation)
        {

            int i = 0;
            while (i < 12)
            {
                int j = 0;
                while (j < 25)
                {
                    if (orientation == DisplayOrientation.LandscapeLeft || orientation == DisplayOrientation.LandscapeRight)
                        spriteBatch.Draw(Carre, new Rectangle(j * 28 + 10, i * 28 + 10, 28, 28), ArrayColor[_map[i, j++]]);
                    else
                        spriteBatch.Draw(Carre, new Rectangle(i * 28 + 10, j * 28 + 10, 28, 28), ArrayColor[_map[i, j++]]);
                }
                i++;
            }
            if (_end == false)
                foreach (piece item in current)
                {

                    if (orientation == DisplayOrientation.LandscapeLeft || orientation == DisplayOrientation.LandscapeRight)
                        spriteBatch.Draw(Carre, new Rectangle(item.x * 28 + 10, item.y * 28 + 10, 28, 28), ArrayColor[item.color]);
                    else
                        spriteBatch.Draw(Carre, new Rectangle(item.y * 28 + 10, item.x * 28 + 10, 28, 28), ArrayColor[item.color]);
                }
            spriteBatch.Draw(Cadre, array[0], Color.White);
            spriteBatch.DrawString(Font1, "Menu", new Vector2(array[0].X + (array[0].Width * 1 / 8), array[0].Y + (array[0].Height * 1 / 3)), Color.Black);
            spriteBatch.Draw(Cadre, array[1], Color.White);
            spriteBatch.DrawString(Font1, (_pause == false ? "Pause" : "Resume"), new Vector2(array[1].X + (array[1].Width * 1 / 8), array[1].Y + (array[1].Height * 1 / 3)), Color.Black);
            spriteBatch.Draw(Cadre, array[2], Color.White);
            spriteBatch.DrawString(Font1, "Restart", new Vector2(array[2].X + (array[2].Width * 1 / 8), array[2].Y + (array[2].Height * 1 / 3)), Color.Black);
            spriteBatch.DrawString(Font1, "Next : ", new Vector2(array[3].X + (array[3].Width * 1 / 8), array[3].Y + (array[3].Height * 1 / 3)), Color.Black);
        //    spriteBatch.Draw(Cadre, array[3], Color.White);
            i = 0;
            while (i < 4)
            {
                int j = 0;
                while (j < 4)
                {
                    if (orientation == DisplayOrientation.LandscapeLeft || orientation == DisplayOrientation.LandscapeRight)
                        spriteBatch.Draw(Carre, new Rectangle(array[3].X + (j * 28), array[3].Y + (i * 28 + 40), 28, 28), Color.LightSlateGray);
                    else
                        spriteBatch.Draw(Carre, new Rectangle(array[3].X + (i * 28), array[3].Y + (j * 28 + 40), 28, 28), Color.LightSlateGray);
                    j++;
                }
                i++;
            }
            foreach (piece item in all_piece[next_piece])
            {
                if (next_piece == Type_piece.TypeI)
                {
                    if (orientation == DisplayOrientation.LandscapeLeft || orientation == DisplayOrientation.LandscapeRight)
                        spriteBatch.Draw(Carre, new Rectangle(array[3].X + (item.x * 28), array[3].Y + ((item.y - 4) * 28 + 40), 28, 28), ArrayColor[item.color]);
                    else
                        spriteBatch.Draw(Carre, new Rectangle(array[3].X + ((item.y - 4) * 28), array[3].Y + (item.x * 28 + 40), 28, 28), ArrayColor[item.color]);
                 }
                else
                {
                    if (orientation == DisplayOrientation.LandscapeLeft || orientation == DisplayOrientation.LandscapeRight)
                        spriteBatch.Draw(Carre, new Rectangle(array[3].X + ((item.x + 1) * 28), array[3].Y + ((item.y - 4) * 28 + 40), 28, 28), ArrayColor[item.color]);
                    else
                        spriteBatch.Draw(Carre, new Rectangle(array[3].X + ((item.y - 4) * 28), array[3].Y + ((item.x + 1)  * 28 + 40), 28, 28), ArrayColor[item.color]);
                }
            }
            spriteBatch.DrawString(Font1, "Level : " + _lvl.ToString(), new Vector2(array[4].X + (array[4].Width * 1 / 8), array[4].Y + (array[4].Height * 1 / 3)), Color.Black);
            spriteBatch.DrawString(Font1, "Score : ", new Vector2(array[5].X + (array[5].Width * 1 / 8), array[5].Y + (array[5].Height * 1 / 3)), Color.Black);
            spriteBatch.DrawString(Font1, _score.ToString(), new Vector2(array[5].X + (array[5].Width * 1 / 8), array[5].Y + (array[5].Height * 1 / 3) + 20), Color.Black);

            spriteBatch.Draw(_Left, array[6], Color.White);
            spriteBatch.Draw(_Down, array[7], Color.White);
            spriteBatch.Draw(_Right, array[8], Color.White);
            spriteBatch.Draw(_FlipLeft, array[9], Color.White);
            spriteBatch.Draw(_FlipRight, array[10], Color.White);
        }

        public void Restart()
        {
            _score = 0;
            _lvl = 1;
            _flag = 0;
            _temp = 0;
            _end = false;
            _pause = false;
            choose_next_piece();
            for (int i = 0; i < 4; i++)
                current[i] = new piece(all_piece[next_piece][i]);
            choose_next_piece();
            init_map();
        }
    }
}
