using UnityEngine;

namespace Game
{
    /// <summary>
    /// Contiene parámetros ajustables del juego
    /// </summary>
    [System.Serializable]
    public class GameParameters
    {
        [Header("General")]
        public int TimeCount;
        public float DeathDuration;

        [Header("Player")]
        public float PlayerRunSpeed;
        public float PlayerJumpHeight;
        public int PlayerMeleeDamage;

        [Header("Fire")]
        public int FireDamage;
        public float FireCastDuration;
        public float FireCastRate;

        [Header("Ice")]
        public int IceDamage;
        public float IceCastDuration;
        public float IceCastRate;

        [Header("Spider")]
        public float SpiderRunSpeed;
        public int SpiderMeleeDamage;
    }
}
