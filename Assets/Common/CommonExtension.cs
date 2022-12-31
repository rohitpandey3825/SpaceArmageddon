using UnityEngine;

namespace Common
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
    }
}