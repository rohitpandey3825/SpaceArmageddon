using UnityEngine;

namespace Assets.Common
{
    public static class CommonExtension
    {
        public static float getRandomSpeed(int from, int to)
        {
           return Random.Range(from, to) * Time.deltaTime;
        }

        public static float getRandomFloat(float from, float to)
        {
            return Random.Range(from, to);
        }

        public static int getRandomInt(int from, int to)
        {
            return Random.Range(from, to);
        }
        
        public static Color getRandomColor()
        {
            return Random.ColorHSV();
        }
    }
}