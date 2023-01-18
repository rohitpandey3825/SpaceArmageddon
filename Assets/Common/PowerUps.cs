using Assets.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Common
{
    public enum PowerUp
    {
        None = -1,
        Sheild = 0,
        Speed = 1,
        tripleShot = 2,
    }

    [Serializable]
    public class PowerUps
    {
        public GameObject Sheild;
        public GameObject Speed;
        public GameObject TripleShot;

        public GameObject getRandomPowerUp()
        {
            int i = CommonExtension.getRandomInt(0, 100) % 3;
            switch ((PowerUp)i)
            {
                case PowerUp.Sheild:
                    return Sheild;
                case PowerUp.Speed:
                    return Speed;
                case PowerUp.tripleShot:
                    return TripleShot;
            }
            return null;
        }
    }
}
