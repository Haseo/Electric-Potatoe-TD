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
using Microsoft.Devices.Sensors;
using Electric_Potatoe_TD.Mob;

namespace Electric_Potatoe_TD
{
    public enum EMap
    {
        BACKGROUND = 0,
        CANYON_HORIZONTAL = 1,
        CENTRAL = 2,
        CANYON_VERTICAL = 3,
        CANYON_TOPLEFT = 4,
        CANYON_BOTLEFT = 5,
        CANYON_TOPRIGHT = 6,
        CANYON_BOTRIGHT = 7,
    };

    public enum EBulletType
    { 
        BULLET = 0,
        FAST = 1,
        SPREAD = 2,
    };

    public enum EMapTexture
    {
        GROUND = 0,
        HORIZONTAL = 1,
        VERTICAL = 2,
        TOPLEFT = 3,
        BOTLEFT = 4,
        TOPRIGHT = 5,
        BOTRIGHT = 6,
        CENTRALTEX = 7,
        CENTRALLOW,
        CENTRALHIGH
    };

    public partial class Game
    {
        public double CoefBonus;
        public List<Pair <Vector2, Vector2>> nodeLink = new List<Pair<Vector2,Vector2>>(); 
        Accelerometer accSensor;
        Vector3 accelReading = new Vector3();
        Vector3 accelBuff = new Vector3();
        Boolean AccAllow;
        Game1 _origin;
        Texture2D Menu;
		Texture2D Button;
        Texture2D Blanco;
        Texture2D RageMetter_top;
        Texture2D RageMetter_mid;
        Texture2D RageMetter_bot;
        SpriteFont RageMetter_font;
        Rectangle[] _position;
        public Dictionary<EBulletType, Texture2D> BulletTexture;
        public Dictionary<EType, Texture2D> TypeTexture;
        public Dictionary<EMobType, Texture2D> MobTexture;
        public Dictionary<EMapTexture, Texture2D> MapTexture;
        public Dictionary<int, Color> LevelColor;
        public Dictionary<int, Texture2D> LevelTexture;
        public Texture2D NoConstruct;
        public List<Node> TurretList;
        public List<Mob.Mob> MobList;
        public List<Shoot> BulletList;
        MapLoader NewMap = new MapLoader();

        Potatoe _central;

        //Animations
        Point   FrameSize;
        Point   BulletFrameSize;

        int RageMetter;
        int RageMetter_flag;
        int RageMetter_tmp;
        // Map
        EMap[,] map;
        int mapX, mapY;
        Vector2 pos_map;
        List<Vector2> WayPoints;
        public int size_case;
        public int size_caseZoom;

        Vector2 Touch;
        bool _moveTouch;
        int _TouchFlag;
        int _ValueTouch;

        Vector2 Zoom;
        bool _zoom;
        List<Vector2> _ListWay;
        List<bool> _ValidWay;

        // Wave
        int currentWave;

        // Mob Spawn
        TimeSpan mobSpawnTime;
        TimeSpan previousSpawnTime;

        // Selection
        Node _node;
        Node _turret;
        int _selectFlag;
        String test = "";

        public List<Electric_Potatoe_TD.Mob.Mob> listTarget = new List<Electric_Potatoe_TD.Mob.Mob>();

        public Game(Game1 game)
        {
            CoefBonus = 1;
            RageMetter_tmp = 0;
            _origin = game;
            RageMetter = 1;
            RageMetter_flag = 1;
            _TouchFlag = 0;
            _ValueTouch = 0;
            _selectFlag = 0;
            _moveTouch = false;
            _zoom = false;
            Zoom = new Vector2(0, 0);
            _central = new Potatoe(0, 0, this);
            BulletList = new List<Shoot>();
            BulletTexture = new Dictionary<EBulletType, Texture2D>();
            TypeTexture = new Dictionary<EType, Texture2D>();
            MobTexture = new Dictionary<EMobType, Texture2D>();
            accSensor = new Accelerometer();
            accSensor.ReadingChanged += new EventHandler<AccelerometerReadingEventArgs>(AccelerometerReadingChanged);
            startAccSensor();
            MapTexture = new Dictionary<EMapTexture, Texture2D>();
            LevelColor = new Dictionary<int, Color>();
            LevelTexture = new Dictionary<int, Texture2D>();
            FrameSize = new Point(40, 40);
            BulletFrameSize = new Point(20, 20);

            mobSpawnTime = TimeSpan.FromSeconds(1.0f);
            previousSpawnTime = TimeSpan.Zero;
        }

        public void Oriented_changed()
        {
            //if (RageMetter < 100)
             //   RageMetter = (RageMetter + 3);
            //if (RageMetter > 100)
            //    RageMetter = 100;
            //RageMetter_flag = 0;
        }

        public void Initialize()
        {
            MobList = new List<Mob.Mob>();
            TurretList = new List<Node>();
            BulletList = new List<Shoot>();

            int stand = (_origin.graphics.PreferredBackBufferHeight - (_origin.graphics.PreferredBackBufferHeight / 6)) / 55;

            _ListWay = new List<Vector2>();
            _ValidWay = new List<bool>();
            _position = new Rectangle[]
             { new Rectangle(_origin.graphics.PreferredBackBufferWidth * 9 / 10, _origin.graphics.PreferredBackBufferHeight * 5 / 6, _origin.graphics.PreferredBackBufferWidth / 10, _origin.graphics.PreferredBackBufferHeight / 6),
               new Rectangle(_origin.graphics.PreferredBackBufferWidth * 9 / 10, stand, _origin.graphics.PreferredBackBufferWidth / 10, stand * 2),
               new Rectangle(_origin.graphics.PreferredBackBufferWidth * 9 / 10, stand * 2, _origin.graphics.PreferredBackBufferWidth / 10, stand),
               new Rectangle(_origin.graphics.PreferredBackBufferWidth * 9 / 10, stand * 49, _origin.graphics.PreferredBackBufferWidth / 10, stand * 10),
               new Rectangle(_origin.graphics.PreferredBackBufferWidth * 1 / 3, _origin.graphics.PreferredBackBufferHeight * 10 / 11, 0, 0),
               
               // Selected Node
               new Rectangle(_origin.graphics.PreferredBackBufferWidth * 7 / 10, stand * 2, 250, stand * 10),
               new Rectangle(_origin.graphics.PreferredBackBufferWidth * 7 / 10, stand * 5, 250, stand * 10),
               new Rectangle(_origin.graphics.PreferredBackBufferWidth * 7 / 10, stand * 8, 250, stand * 10),
               
               // Selected Turret
               new Rectangle(_origin.graphics.PreferredBackBufferWidth * 7 / 10, stand * 21, 250, stand * 10),
               new Rectangle(_origin.graphics.PreferredBackBufferWidth * 7 / 10, stand * 24, 250, stand * 10),
               new Rectangle(_origin.graphics.PreferredBackBufferWidth * 7 / 10, stand * 27, 250, stand * 10),
               
               // Activate Desactivate
               new Rectangle(_origin.graphics.PreferredBackBufferWidth * 7 / 10, stand * 37, 250, stand * 10),
               new Rectangle(_origin.graphics.PreferredBackBufferWidth * 7 / 10, stand * 47, 250, stand * 10),

               // Continue
               new Rectangle(_origin.graphics.PreferredBackBufferWidth * 7 / 10, stand * 57, 250, stand * 10),

               // Specialize Node
               new Rectangle(_origin.graphics.PreferredBackBufferWidth * 7 / 10, stand * 2, 250, stand * 10),
               new Rectangle(_origin.graphics.PreferredBackBufferWidth * 7 / 10, stand * 12, 250, stand * 10),
               new Rectangle(_origin.graphics.PreferredBackBufferWidth * 7 / 10, stand * 22, 250, stand * 10),
               new Rectangle(_origin.graphics.PreferredBackBufferWidth * 7 / 10, stand * 32, 250, stand * 10),
               new Rectangle(_origin.graphics.PreferredBackBufferWidth * 7 / 10, stand * 42, 250, stand * 10),
             };
        }

        public void LoadContent()
        {
            Menu = _origin.Content.Load<Texture2D>("Menu");
            Blanco = _origin.Content.Load<Texture2D>("blanco");
            RageMetter_top = _origin.Content.Load<Texture2D>("RageMeterHigh");
            RageMetter_mid = _origin.Content.Load<Texture2D>("RageMeterMiddle");
            RageMetter_bot = _origin.Content.Load<Texture2D>("RageMeterLow");
            RageMetter_font = _origin.Content.Load<SpriteFont>("RageMetter");
            MobTexture[EMobType.PEON] = _origin.Content.Load<Texture2D>("Mob3");
            MobTexture[EMobType.SPEED] = _origin.Content.Load<Texture2D>("Mob4");
            MobTexture[EMobType.TANK] = _origin.Content.Load<Texture2D>("Mob2");
            MobTexture[EMobType.BERSERK] = _origin.Content.Load<Texture2D>("Mob1");
            MobTexture[EMobType.BOSS] = _origin.Content.Load<Texture2D>("MobBoss");
            MapTexture[EMapTexture.GROUND] = _origin.Content.Load<Texture2D>("GeometryGround");
            MapTexture[EMapTexture.HORIZONTAL] = _origin.Content.Load<Texture2D>("GeometryCanyonHorizontal");
            MapTexture[EMapTexture.VERTICAL] = _origin.Content.Load<Texture2D>("GeometryCanyonVertical");
            MapTexture[EMapTexture.TOPLEFT] = _origin.Content.Load<Texture2D>("GeometryCanyonTopLeft");
            MapTexture[EMapTexture.TOPRIGHT] = _origin.Content.Load<Texture2D>("GeometryCanyonTopRight");
            MapTexture[EMapTexture.BOTLEFT] = _origin.Content.Load<Texture2D>("GeometryCanyonBotLeft");
            MapTexture[EMapTexture.BOTRIGHT] = _origin.Content.Load<Texture2D>("GeometryCanyonBotRight");
            MapTexture[EMapTexture.CENTRALTEX] = _origin.Content.Load<Texture2D>("ReactorN");
            MapTexture[EMapTexture.CENTRALLOW] = _origin.Content.Load<Texture2D>("ReactorD");
            MapTexture[EMapTexture.CENTRALHIGH] = _origin.Content.Load<Texture2D>("ReactorH");       
            TypeTexture[EType.SPEED] = _origin.Content.Load<Texture2D>("TowerFast");
            TypeTexture[EType.SHOOTER] = _origin.Content.Load<Texture2D>("TowerNormal");
            TypeTexture[EType.STRENGHT] = _origin.Content.Load<Texture2D>("TowerHeavy");
            TypeTexture[EType.NODE] = _origin.Content.Load<Texture2D>("Node");
            TypeTexture[EType.GENERATOR] = _origin.Content.Load<Texture2D>("TowerGenerator");
            NoConstruct = _origin.Content.Load<Texture2D>("NoConstruct");
            LevelColor[0] = Color.White;
            LevelColor[1] = Color.GreenYellow;
            LevelColor[2] = Color.Orange;
            LevelColor[3] = Color.Red;
            LevelTexture[0] = _origin.Content.Load<Texture2D>("Level0");
            LevelTexture[1] = _origin.Content.Load<Texture2D>("Level1");
            LevelTexture[2] = _origin.Content.Load<Texture2D>("Level2");
            LevelTexture[3] = _origin.Content.Load<Texture2D>("Level3");
            LevelTexture[4] = _origin.Content.Load<Texture2D>("Level4");
            BulletTexture[EBulletType.BULLET] = _origin.Content.Load<Texture2D>("BulletNormal");
            BulletTexture[EBulletType.FAST] = _origin.Content.Load<Texture2D>("BulletFast");
            BulletTexture[EBulletType.SPREAD] = _origin.Content.Load<Texture2D>("BulletHeavy");
			Button = _origin.Content.Load<Texture2D>("Button");
        }

        public void UnloadContent()
        {
        }

        protected Vector2 ident_pos(Vector2 pos)
        {
            int x, y;

            if (_zoom == false &&
                (pos.X >= ((_origin.graphics.PreferredBackBufferWidth * 9 / 10) - 10) ||
                pos.Y >= ((_origin.graphics.PreferredBackBufferHeight * 9 / 10) - 10)))
                return (new Vector2(-1, -1));
            if (_zoom == true && 
                (pos.X >= ((_origin.graphics.PreferredBackBufferWidth * 8 / 12) - 10) ||
                pos.Y >= ((_origin.graphics.PreferredBackBufferHeight * 8 / 10) - 10)))
                return (new Vector2(-1, -1));
            if (_zoom == true)
            {
                x = ((int)pos.X - 10) / size_caseZoom;
                y = ((int)pos.Y - 10) / size_caseZoom;
            }
            else
            {
                x = ((int)pos.X - 10) / size_case;
                y = ((int)pos.Y - 10) / size_case;
            }
            if (_zoom == true)
                return (new Vector2(x + Zoom.X, y + Zoom.Y));
            return (new Vector2(x, y));
        }

        private  bool can_access()
        {
            bool can_create = true;

            add_wayTouch();
            foreach (bool value in _ValidWay)
            {
                if (value == false)
                {
                    can_create = false;
                }
            }
            return (can_create);
        }


        private void make_connect(Vector2 new_node, Vector2 old_node)
        {
            Node newn = null, oldn = null, oldt = null;

            foreach (Node turret in TurretList)
            {
                if (turret._position.X == new_node.X && turret._position.Y == new_node.Y)
                    newn = turret;
                if (turret._position.X == old_node.X && turret._position.Y == old_node.Y)
                {
                    if (turret.getType() != EType.NODE)
                        oldt = turret;
                    else
                        oldn = turret;
                }
            }
            if (newn != null && (oldn != null || oldt != null))
            {
                if (oldt != null)
                    ElectricityManager.linkNode(oldt, newn);
                else
                    ElectricityManager.linkNode(oldn, newn);
            }
            else if (newn != null && ((_central._position.X == old_node.X && _central._position.Y == old_node.Y) ||
                (_central._position.X == (old_node.X - 1) && _central._position.Y == old_node.Y) ||
                (_central._position.X == old_node.X && _central._position.Y == (old_node.Y + 1)) ||
                (_central._position.X == (old_node.X - 1) && _central._position.Y == (old_node.Y + 1))))
            {
                ElectricityManager.linkNode(_central, newn);
            }
        }

        private void transfert_connect(Vector2 position)
        {
            Node oldn = null, newn = null;

            foreach (Node turret in TurretList)
            {
                if (turret._position.X == position.X && turret._position.Y == position.Y)
                {
                    if (turret.getType() != EType.NODE)
                        newn = turret;
                    else
                        oldn = turret;
                }
            }
            if (newn != null && oldn != null)
            {
                List<Node> listLink = oldn._peerOut;

                while (listLink.Count > 0)
                {
                    ElectricityManager.linkNode(newn, listLink.First<Node>());
                    ElectricityManager.unlinkNode(oldn, listLink.First<Node>());
                }
            }
        }


        public void Restart()
        {
            RageMetter = 0;
            RageMetter_flag = 0;
            _TouchFlag = 0;
            _ValueTouch = 0;
            _selectFlag = 0;
            Zoom = new Vector2(0, 0);
            _moveTouch = false;
            _zoom = false;
            mapFiller();
            //turretFiller();
            //FakeModFiller();
            //FakeBulletFiller();
            
            TurretList.Clear();
            MobList.Clear();
            BulletList.Clear();
            _central.setCapital(1000);
        }

        public int getScore()
        {
            return (_central.getScore());
        }

        public void mobIsDead(Mob.Mob mob)
        {
            int i = 0;

            _central.AddCapital((int) (mob.MobMaxPV * CoefBonus));
            while (i < MobList.Count)
            {
                if (mob == MobList[i])
                    MobList.RemoveAt(i);
                i++;
            }
            foreach (Node myTurret in TurretList)
            {
               myTurret.removeMobCorpse(mob);
            }
        }

        public bool checkEndGame()
        {
            if (_central.getCapital() <= 0)
                return true;
            return false;
        }
    }
}