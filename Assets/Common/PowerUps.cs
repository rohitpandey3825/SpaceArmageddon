using Assets.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Common
{
    [Serializable]
    public class PowerUps
    {
        public GameObject Sheild;
        public GameObject Speed;
        public GameObject TripleShot;

        public GameObject getRandomPowerUp()
        {
            int i = CommonExtension.getRandomInt(1, 3);
            switch (i)
            {
                case 0:
                    return Sheild;
                case 1:
                    return Speed;
                case 2:
                    return TripleShot;
            }
            return null;
        }
    }
}
