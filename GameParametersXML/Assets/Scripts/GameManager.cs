using UnityEngine;

namespace Game
{
    /// <summary>
    /// Clase persistente entre escenas
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        #region Persistent_Singleton

        public static GameManager Instance;

        /// <summary>
        /// Persistent Singleton
        /// </summary>
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }

        #endregion Persistent_Singleton

        /// <summary>
        /// Parámetros del juego
        /// </summary>
        public GameParameters GameParameters;
    }
}
