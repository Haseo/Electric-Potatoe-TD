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
        private void startAccSensor()
        {
            try
            {
                accSensor.Start();
                AccAllow = true;
            }
            catch (AccelerometerFailedException e)
            {
                AccAllow = false;
                Console.WriteLine(e.ToString());
            }
            catch (UnauthorizedAccessException e)
            {
                AccAllow = false;
                Console.WriteLine(e.ToString());
            }
            accelBuff.X = 0;
            accelBuff.Y = 0;
            accelBuff.Z = 0;
        }

        public void AccelerometerReadingChanged(object sender, AccelerometerReadingEventArgs e)
        {
            accelReading.X = (float)e.X;
            accelReading.Y = (float)e.Y;
            accelReading.Z = (float)e.Z;
        }

        private void mvtBonus()
        {
            double moreThan = 0;

            if (!AccAllow)
                return;
            if (RageMetter_flag > 0)
            {
                if (RageMetter_flag == 1)
                {
                    CoefBonus = 1;
                    RageMetter = 1;
                }
                RageMetter_flag--;
                return;
            }
            else
            {
                if (RageMetter < 20)
                {
                    moreThan = 0.3;
                    CoefBonus = 1.5;
                }
                else if (RageMetter < 50)
                {
                    moreThan = 0.15;
                    CoefBonus = 2;
                }
                else if (RageMetter < 70)
                {
                    moreThan = 0.1;
                    CoefBonus = 3.5;
                }
                else if (RageMetter < 90)
                {
                    moreThan = 0.03;
                    CoefBonus = 4;
                }
                else if (RageMetter < 100)
                {
                    moreThan = 0.03;
                    CoefBonus = 4;
                }
                else if (RageMetter < 110)
                {
                    RageMetter_flag = 175;
                    CoefBonus = 0;
                }
            }

            if (RageMetter_tmp > 2)
            {
                if (RageMetter_tmp > 200)
                    RageMetter_tmp = 10;
                if (RageMetter > 1)
                    RageMetter--;
            }

            if ((accelReading.X > accelBuff.X && accelReading.X - accelBuff.X > moreThan)
                    || accelReading.X > accelBuff.X && accelBuff.X - accelReading.X > moreThan)
            {
                RageMetter++;
                RageMetter_tmp = 0;
            }
            else if ((accelReading.Y > accelBuff.Y && accelReading.Y - accelBuff.Y > moreThan)
            || accelReading.Y > accelBuff.Y && accelBuff.Y - accelReading.Y > moreThan)
            {
                RageMetter_tmp = 0;
                RageMetter++;
            }
            else if ((accelReading.Z > accelBuff.Z && accelReading.Z - accelBuff.Y > moreThan)
            || accelReading.Y > accelBuff.Y && accelBuff.Y - accelReading.Y > moreThan)
            {
                RageMetter_tmp = 0;
                RageMetter++;
            }
            else
            {
                RageMetter_tmp++;
            }

            accelBuff.X = accelReading.X;
            accelBuff.Y = accelReading.Y;
            accelBuff.Z = accelReading.Z;
        }
    }
}
