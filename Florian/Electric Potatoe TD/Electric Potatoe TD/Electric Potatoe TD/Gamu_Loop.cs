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
        private void game_loop(GameTime gameTime)
        {
            spawningManager(gameTime);
            if (MobList.Count > 0)
            {
                foreach (Mob.Mob mob in MobList)
                {
                    int ret = mob.update();

                    if (ret == 0)
                    {
                        foreach (Node myTurret in TurretList)
                        {
                            if (myTurret.getType() == EType.STRENGHT || myTurret.getType() == EType.SPEED || myTurret.getType() == EType.SHOOTER)
                            {
                                myTurret.putInRange(mob);
                            }
                        }
                    }
                    else if (ret > 0)
                        ;
                    // Change capital en fonction ret
                }
                // Manager Electric : update
                foreach (Node myTurret in TurretList)
                {
                    myTurret.update();
                }
            }
            if (_central.getCapital() == 0)
              _origin.End_Game(false, 0); // Fin du jeu avec defaite du joueur
        }

        private void spawningManager(GameTime gameTime)
        {
            if (currentWave < NewMap.ListOfWaves.Count && NewMap.ListOfWaves[currentWave].ListOfMonster.Count > 0)
            {
                if (gameTime.TotalGameTime - previousSpawnTime > mobSpawnTime)
                {
                    previousSpawnTime = gameTime.TotalGameTime;
                    MobList.Add(NewMap.ListOfWaves[currentWave].SpawnMonster());
                }
            }
            else
            {
                if (currentWave == NewMap.ListOfWaves.Count - 1 && MobList.Count == 0)
                {
                    _origin.End_Game(true, _central.getCapital()); // Fin du jeu avec victoire du joueur
                    // Pour l'instant remplacer par un return
                    return;
                }
                if (MobList.Count == 0)
                  currentWave++;
            }
        }
    }
}
