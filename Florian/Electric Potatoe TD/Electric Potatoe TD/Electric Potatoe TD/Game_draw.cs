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
    public partial class Game
    {
        public void draw(int FrameStart, int FPS, int CurrentFrame, int CurrentMobFrame, int SheetSize)
        {
            if (_zoom == false)
            {
                draw_map(FrameStart, FPS, CurrentFrame, SheetSize);
                draw_content();
                draw_link();
                draw_mobs(CurrentMobFrame);
                draw_bullet(CurrentFrame);
            }
            else
            {
                draw_mapZoom(FrameStart, FPS, CurrentFrame, SheetSize);
                draw_contentZoom();
                if (_moveTouch == true && _ListWay.Count > 0)
                    draw_newNode();
                blanco();
                if (_selectFlag > 0)
                    draw_Selected();
            }

        }

        public void draw_Selected()
        {

            if ((_selectFlag == 1 || _selectFlag == 2) && _node != null)
            {
                _origin.spriteBatch.DrawString(RageMetter_font, "NODE", new Vector2(_position[5].X, _position[5].Y), Color.White);
                _origin.spriteBatch.DrawString(RageMetter_font, "     Niveau " + _node.getTowerLevel().ToString(), new Vector2(_position[6].X, _position[6].Y), Color.White);
                if (_node.getNodeLevel() < 3)
                    _origin.spriteBatch.DrawString(RageMetter_font, "|AMÉLIORER NODE|\n    Coût: " + _node.getCostNode(), new Vector2(_position[7].X, _position[7].Y),
                        (_node.getCostNode() >= _central.getCapital() ? Color.LightSlateGray : Color.White));
            }
            if (_selectFlag == 1)
            {
                _origin.spriteBatch.DrawString(RageMetter_font, "|CRÉER TOUR|", new Vector2(_position[8].X, _position[8].Y), (Color.White));
            }
            if (_selectFlag == 2 && _turret != null)
            {
                _origin.spriteBatch.DrawString(RageMetter_font, "TOUR", new Vector2(_position[8].X, _position[8].Y), Color.White);
                _origin.spriteBatch.DrawString(RageMetter_font, "     Niveau : " + _turret.getTowerLevel().ToString(), new Vector2(_position[9].X, _position[9].Y), Color.White);
                if (_turret.getTowerLevel() < 4)
                    _origin.spriteBatch.DrawString(RageMetter_font, "|AMÉLIORER TOUR|\n    Coût: " + _turret.getCostTower(), new Vector2(_position[10].X, _position[10].Y),
                        (_turret.getCostTower() >= _central.getCapital() ? Color.LightSlateGray : Color.White));
            }
            if (_selectFlag == 1 || _selectFlag == 2)
            {
                _origin.spriteBatch.DrawString(RageMetter_font, "|ACTIVER|", new Vector2(_position[11].X, _position[11].Y), Color.White);
                _origin.spriteBatch.DrawString(RageMetter_font, "|DÉSACTIVER|", new Vector2(_position[12].X, _position[12].Y), Color.White);
            }
            _origin.spriteBatch.DrawString(RageMetter_font, "|RETOUR|", new Vector2(_position[13].X, _position[13].Y), Color.White);
            if (_selectFlag == 3)
            {
                _origin.spriteBatch.DrawString(RageMetter_font, "CRÉER TOUR", new Vector2(_position[14].X, _position[14].Y), Color.White);
                _origin.spriteBatch.DrawString(RageMetter_font, "|CLASSIQUE| " + Tower.get_cost_tower(EType.SHOOTER).ToString(), new Vector2(_position[15].X, _position[15].Y),
                    (Tower.get_cost_tower(EType.SHOOTER) >= _central.getCapital() ? Color.LightSlateGray : Color.White));
                _origin.spriteBatch.DrawString(RageMetter_font, "|PUISSANTE| " + Tower.get_cost_tower(EType.STRENGHT).ToString(), new Vector2(_position[16].X, _position[16].Y),
                    (Tower.get_cost_tower(EType.STRENGHT) >= _central.getCapital() ? Color.LightSlateGray : Color.White));
                _origin.spriteBatch.DrawString(RageMetter_font, "|RAPIDE| " + Tower.get_cost_tower(EType.SPEED).ToString(), new Vector2(_position[17].X, _position[17].Y),
                    (Tower.get_cost_tower(EType.SPEED) >= _central.getCapital() ? Color.LightSlateGray : Color.White));
                _origin.spriteBatch.DrawString(RageMetter_font, "|GENERATEUR| " + Tower.get_cost_tower(EType.GENERATOR).ToString(), new Vector2(_position[18].X, _position[18].Y),
                    (Tower.get_cost_tower(EType.GENERATOR) >= _central.getCapital() ? Color.LightSlateGray : Color.White));
            }
        }

        public void draw_newNode()
        {
            Vector2 stand = _ListWay.Last<Vector2>();

            if (can_access() == true)
            {
                if (_central.CanaddLink() == true &&
                    ((_central._position.X == Touch.X && _central._position.Y == Touch.Y) ||
                    (_central._position.X == (Touch.X - 1) && _central._position.Y == Touch.Y) ||
                    (_central._position.X == Touch.X && _central._position.Y == (Touch.Y + 1)) ||
                    (_central._position.X == (Touch.X - 1) && _central._position.Y == (Touch.Y + 1))))
                {
                    _origin.spriteBatch.Draw(NoConstruct, new Rectangle((int)pos_map.X + (size_caseZoom * ((int)stand.X - (int)Zoom.X)), (int)pos_map.Y + (size_caseZoom * ((int)stand.Y - (int)Zoom.Y)), size_caseZoom, size_caseZoom), Color.White);
                }
                else
                    _origin.spriteBatch.Draw(TypeTexture[EType.NODE], new Rectangle((int)pos_map.X + (size_caseZoom * ((int)stand.X - (int)Zoom.X)), (int)pos_map.Y + (size_caseZoom * ((int)stand.Y - (int)Zoom.Y)), size_caseZoom, size_caseZoom), new Rectangle(0 * FrameSize.X, 0, FrameSize.X, FrameSize.Y), Color.White);
            }
            else
            {
                _origin.spriteBatch.Draw(NoConstruct, new Rectangle((int)pos_map.X + (size_caseZoom * ((int)stand.X - (int)Zoom.X)), (int)pos_map.Y + (size_caseZoom * ((int)stand.Y - (int)Zoom.Y)), size_caseZoom, size_caseZoom), Color.White);
            }
        }

        public void draw_link()
        {
            nodeLink.ForEach(delegate(Pair<Vector2, Vector2> buf)
            {
               _origin.spriteBatch.DrawString(RageMetter_font, "Tower !", buf.First, Color.Black);
            });
   

        }

        public void drawLine(Vector2 origine, Vector2 dest)
        {

        }

        public void draw_mapZoom(int FrameStart, int FPS, int CurrentFrame, int SheetSize)
        {
            int x = (int)Zoom.X, y = (int)Zoom.Y;

            for (x = (int)Zoom.X; x < Zoom.X + 7 && x < mapX; x++)
            {
                for (y = (int)Zoom.Y; y < Zoom.Y + 5 && y < mapY; y++)
                {
                    switch (this.map[x, y])
                    {
                        case EMap.BACKGROUND: _origin.spriteBatch.Draw(MapTexture[EMapTexture.GROUND], new Rectangle((int)pos_map.X + (size_caseZoom * (x - (int)Zoom.X)), (int)pos_map.Y + (size_caseZoom * (y - (int)Zoom.Y)), size_caseZoom, size_caseZoom), Color.White); break;
                        case EMap.CANYON_HORIZONTAL: _origin.spriteBatch.Draw(MapTexture[EMapTexture.HORIZONTAL], new Rectangle((int)pos_map.X + (size_caseZoom * (x - (int)Zoom.X)), (int)pos_map.Y + (size_caseZoom * (y - (int)Zoom.Y)), size_caseZoom, size_caseZoom), Color.White); break;
                        case EMap.CENTRAL: _origin.spriteBatch.Draw(MapTexture[EMapTexture.GROUND], new Rectangle((int)pos_map.X + (size_caseZoom * (x - (int)Zoom.X)), (int)pos_map.Y + (size_caseZoom * (y - (int)Zoom.Y)), size_caseZoom * 2, size_caseZoom * 2), Color.White); break;
                        case EMap.CANYON_VERTICAL: _origin.spriteBatch.Draw(MapTexture[EMapTexture.VERTICAL], new Rectangle((int)pos_map.X + (size_caseZoom * (x - (int)Zoom.X)), (int)pos_map.Y + (size_caseZoom * (y - (int)Zoom.Y)), size_caseZoom, size_caseZoom), Color.White); break;
                        case EMap.CANYON_TOPRIGHT: _origin.spriteBatch.Draw(MapTexture[EMapTexture.TOPRIGHT], new Rectangle((int)pos_map.X + (size_caseZoom * (x - (int)Zoom.X)), (int)pos_map.Y + (size_caseZoom * (y - (int)Zoom.Y)), size_caseZoom, size_caseZoom), Color.White); break;
                        case EMap.CANYON_TOPLEFT: _origin.spriteBatch.Draw(MapTexture[EMapTexture.TOPLEFT], new Rectangle((int)pos_map.X + (size_caseZoom * (x - (int)Zoom.X)), (int)pos_map.Y + (size_caseZoom * (y - (int)Zoom.Y)), size_caseZoom, size_caseZoom), Color.White); break;
                        case EMap.CANYON_BOTRIGHT: _origin.spriteBatch.Draw(MapTexture[EMapTexture.BOTRIGHT], new Rectangle((int)pos_map.X + (size_caseZoom * (x - (int)Zoom.X)), (int)pos_map.Y + (size_caseZoom * (y - (int)Zoom.Y)), size_caseZoom, size_caseZoom), Color.White); break;
                        case EMap.CANYON_BOTLEFT: _origin.spriteBatch.Draw(MapTexture[EMapTexture.BOTLEFT], new Rectangle((int)pos_map.X + (size_caseZoom * (x - (int)Zoom.X)), (int)pos_map.Y + (size_caseZoom * (y - (int)Zoom.Y)), size_caseZoom, size_caseZoom), Color.White); break;
                    }
                }
            }
            for (x = (int)Zoom.X; x < Zoom.X + 7 && x < mapX; x++)
            {
                for (y = (int)Zoom.Y; y < Zoom.Y + 5 && y < mapY; y++)
                {
                    if (this.map[x, y] == EMap.CENTRAL)
                        _origin.spriteBatch.Draw(MapTexture[EMapTexture.CENTRALTEX], new Rectangle((int)pos_map.X + (size_caseZoom * (x - (int)Zoom.X)), ((int)pos_map.Y + (size_caseZoom * (y - (int)Zoom.Y))) - size_caseZoom, size_caseZoom * 2, size_caseZoom * 2), Color.White);
                }
            }
            foreach (Node myTurret in TurretList)
            {
                Color turretColor = new Color();
                if (myTurret._activated)
                    turretColor = LevelColor[myTurret.getNodeLevel()];
                else
                    turretColor = Color.Gray;
                if ((int)myTurret.getPosition().X >= (int)Zoom.X && (int)myTurret.getPosition().X < (int)Zoom.X + 7 && (int)myTurret.getPosition().Y >= (int)Zoom.Y && (int)myTurret.getPosition().Y < (int)Zoom.Y + 5)
                {
                    _origin.spriteBatch.Draw(TypeTexture[myTurret.getType()], new Rectangle((int)pos_map.X + (size_caseZoom * ((int)myTurret.getPosition().X - (int)Zoom.X)), (int)pos_map.Y + (size_caseZoom * ((int)myTurret.getPosition().Y - (int)Zoom.Y)), size_caseZoom, size_caseZoom), new Rectangle(CurrentFrame * FrameSize.X, 0, FrameSize.X, FrameSize.Y), turretColor);
                    if (myTurret.getType() == EType.STRENGHT || myTurret.getType() == EType.SPEED || myTurret.getType() == EType.SHOOTER || myTurret.getType() == EType.GENERATOR)
                        _origin.spriteBatch.Draw(LevelTexture[myTurret.getTowerLevel()], new Rectangle((int)pos_map.X + (size_caseZoom * ((int)myTurret.getPosition().X - (int)Zoom.X)), (int)pos_map.Y + (size_caseZoom * ((int)myTurret.getPosition().Y - (int)Zoom.Y)), size_caseZoom, size_caseZoom), Color.White);
                }
            }
        }

        public void draw_map(int FrameStart, int FPS, int CurrentFrame, int SheetSize)
        {
            int x = 0, y = 0;

            for (x = 0; x < mapX; x++)
            {
                for (y = 0; y < mapY; y++)
                {
                    switch (this.map[x, y])
                    {
                        case EMap.BACKGROUND: _origin.spriteBatch.Draw(MapTexture[EMapTexture.GROUND], new Rectangle((int)pos_map.X + (size_case * x), (int)pos_map.Y + (size_case * y), size_case, size_case), Color.White); break;
                        case EMap.CANYON_HORIZONTAL: _origin.spriteBatch.Draw(MapTexture[EMapTexture.HORIZONTAL], new Rectangle((int)pos_map.X + (size_case * x), (int)pos_map.Y + (size_case * y), size_case, size_case), Color.White); break;
                        case EMap.CENTRAL: _origin.spriteBatch.Draw(MapTexture[EMapTexture.GROUND], new Rectangle((int)pos_map.X + (size_case * x), (int)pos_map.Y + (size_case * y), size_case * 2, size_case * 2), Color.White); break;
                        case EMap.CANYON_VERTICAL: _origin.spriteBatch.Draw(MapTexture[EMapTexture.VERTICAL], new Rectangle((int)pos_map.X + (size_case * x), (int)pos_map.Y + (size_case * y), size_case, size_case), Color.White); break;
                        case EMap.CANYON_TOPLEFT: _origin.spriteBatch.Draw(MapTexture[EMapTexture.TOPLEFT], new Rectangle((int)pos_map.X + (size_case * x), (int)pos_map.Y + (size_case * y), size_case, size_case), Color.White); break;
                        case EMap.CANYON_TOPRIGHT: _origin.spriteBatch.Draw(MapTexture[EMapTexture.TOPRIGHT], new Rectangle((int)pos_map.X + (size_case * x), (int)pos_map.Y + (size_case * y), size_case, size_case), Color.White); break;
                        case EMap.CANYON_BOTLEFT: _origin.spriteBatch.Draw(MapTexture[EMapTexture.BOTLEFT], new Rectangle((int)pos_map.X + (size_case * x), (int)pos_map.Y + (size_case * y), size_case, size_case), Color.White); break;
                        case EMap.CANYON_BOTRIGHT: _origin.spriteBatch.Draw(MapTexture[EMapTexture.BOTRIGHT], new Rectangle((int)pos_map.X + (size_case * x), (int)pos_map.Y + (size_case * y), size_case, size_case), Color.White); break;
                    }
                }
            }
            for (x = 0; x < mapX; x++)
            {
                for (y = 0; y < mapY; y++)
                {
                    if (this.map[x, y] == EMap.CENTRAL)
                        _origin.spriteBatch.Draw(MapTexture[EMapTexture.CENTRALTEX], new Rectangle((int)pos_map.X + (size_case * x), (int)pos_map.Y + (size_case * y) - size_case, size_case * 2, size_case * 2), Color.White);
                }
            }
            Vector2 pos = new Vector2();
            foreach (Node myTurret in TurretList)
            {
                Color turretColor = new Color();
                if (myTurret._activated)
                    turretColor = LevelColor[myTurret.getNodeLevel()];
                else
                    turretColor = Color.Gray;
                pos.X = (int)pos_map.X + (size_case * (int)myTurret.getPosition().X);
                pos.Y = (int)pos_map.Y + (size_case * (int)myTurret.getPosition().Y);
                _origin.spriteBatch.Draw(TypeTexture[myTurret.getType()], new Rectangle((int)pos.X, (int)pos.Y, size_case, size_case), new Rectangle(CurrentFrame * FrameSize.X, 0, FrameSize.X, FrameSize.Y), turretColor);
                if (myTurret.getType() == EType.STRENGHT || myTurret.getType() == EType.SPEED || myTurret.getType() == EType.SHOOTER || myTurret.getType() == EType.GENERATOR)
                    _origin.spriteBatch.Draw(LevelTexture[myTurret.getTowerLevel()], new Rectangle((int)pos_map.X + (size_case * (int)myTurret.getPosition().X), (int)pos_map.Y + (size_case * (int)myTurret.getPosition().Y), size_case, size_case), Color.White);
            }
        }

        public void draw_mobs(int CurrentFrame)
        {
            Vector2 pos = new Vector2();
            _origin.spriteBatch.DrawString(RageMetter_font, "Test : " + WayPoints[0].X.ToString() + " " + WayPoints[0].Y.ToString(), new Vector2(_position[4].X + 200, _position[4].Y), Color.Black);
            foreach (Mob.Mob myMob in MobList)
            {
                pos.X = (int)pos_map.X + (size_case * (int)myMob.MobPos.X);
                pos.Y = (int)pos_map.Y + (size_case * (int)myMob.MobPos.Y);
                _origin.spriteBatch.Draw(MobTexture[(EMobType)myMob.GetMobType()], new Rectangle((int)myMob.MobPos.X + 20, (int)myMob.MobPos.Y + 20, size_case - 20, size_case - 20), new Rectangle(CurrentFrame * FrameSize.X, 0, FrameSize.X, FrameSize.Y), Color.White);
                //new Rectangle(CurrentFrame * FrameSize.X, 0, FrameSize.X, FrameSize.Y), 
			}
		}

        public void draw_content()
        {
            int i = 1;
            _origin.spriteBatch.Draw(Menu, _position[0], Color.White);
            _origin.spriteBatch.Draw(RageMetter_top, _position[1], (RageMetter > 99 ? Color.Red : Color.White));
            while (i < 98)
            {
                _origin.spriteBatch.Draw(RageMetter_mid, new Rectangle(_position[2].X, _position[2].Y + (_position[2].Height * (i / 2)), _position[2].Width, _position[2].Height),
                    (RageMetter >= (100 - i) ? Color.Red : Color.White));
                i++;
            }
            _origin.spriteBatch.Draw(RageMetter_bot, _position[3], (RageMetter > 0 ? Color.Red : Color.White));
            _origin.spriteBatch.DrawString(RageMetter_font, RageMetter.ToString(), new Vector2(_position[3].X + (_position[3].Width / 3), _position[3].Y + (_position[3].Height / 3)), Color.White);
            _origin.spriteBatch.DrawString(RageMetter_font, "RESOURCES : " + _central.getCapital().ToString(), new Vector2(_position[4].X, _position[4].Y), Color.White);
        }

        public void draw_bullet(int CurrentFrame)
        {
            foreach (Shoot myBullet in BulletList)
            {
                EBulletType bulletText = (EBulletType)myBullet.GetType();

                _origin.spriteBatch.Draw(BulletTexture[(EBulletType)myBullet.GetType()], new Rectangle((int)myBullet.Coord.X + size_case / 2, (int)myBullet.Coord.Y + size_case / 2, size_case / 2, size_case / 2), new Rectangle(CurrentFrame * BulletFrameSize.X, 0, BulletFrameSize.X, BulletFrameSize.Y), Color.White);
                //new Rectangle(CurrentFrame * FrameSize.X, 0, FrameSize.X, FrameSize.Y), 
            }
        }

        public void draw_contentZoom()
        {
            //     _origin.spriteBatch.DrawString(RageMetter_font, "Capital : " + _central.getCapital().ToString(), new Vector2(_position[4].X, _position[4].Y), Color.Black);
            _origin.spriteBatch.DrawString(RageMetter_font, "test : " + test, new Vector2(_position[4].X, _position[4].Y), Color.Black);
        }

        public void blanco()
        {

            _origin.spriteBatch.Draw(Blanco, new Rectangle((_origin.graphics.PreferredBackBufferWidth / 3) * 2, 0, _origin.graphics.PreferredBackBufferWidth/ 3, _origin.graphics.PreferredBackBufferHeight), Color.White);
        }
    }
}
