using UnityEngine;
using System.Xml;

namespace Game
{
    /// <summary>
    /// Se encarga de cargar los datos del XML y se destruye
    /// </summary>
    public class TextsManager : MonoBehaviour
    {
        [Header("XML")]
        public TextAsset GameParametersText;

        /// <summary>
        /// Empieza la lectura de datos
        /// </summary>
        private void Awake()
        {
            //TODO: Movidas de PlayerPrefs
            LoadTexts();
        }

        /// <summary>
        /// Inicializa valores del GameManager en los que va a guardar los datos del XML y empieza la lectura
        /// </summary>
        private void LoadTexts()
        {
            GameManager.Instance.GameParameters = new GameParameters();

            GetGameParametersInfo();

            Destroy(this.gameObject);
        }

        /// <summary>
        /// Obtiene los datos del GameParametersText y los guarda en un atributo en GameManager
        /// </summary>
        private void GetGameParametersInfo()
        {
            TextAsset currentFile = GameParametersText;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(currentFile.text);
            XmlNodeList levelsList = xmlDoc.GetElementsByTagName("gameParameters"); //Se coloca en el primer tag

            foreach (XmlNode levelIndex in levelsList)//gameParameters
            {
                foreach (XmlNode levelInfo in levelIndex)//general, players, enemies...
                {
                    if (levelInfo.Name == "general")
                        GetGeneralParametersInfo(levelInfo);

                    else if (levelInfo.Name == "player")
                        GetPlayerParametersInfo(levelInfo);

                    else if (levelInfo.Name == "enemies")
                        GetEnemiesParametersInfo(levelInfo);
                }
            }
        }

        /// <summary>
        /// Lee los parámetros generales
        /// </summary>
        /// <param name="levelInfo"></param>
        private void GetGeneralParametersInfo(XmlNode levelInfo)
        {
            foreach (XmlNode levelGeneral in levelInfo.ChildNodes) //general
            {
                if (levelGeneral.Name == "time_count")
                    GameManager.Instance.GameParameters.TimeCount = int.Parse(levelGeneral.InnerText);
                else if (levelGeneral.Name == "death_duration")
                    GameManager.Instance.GameParameters.DeathDuration = float.Parse(levelGeneral.InnerText);
            }
        }

        /// <summary>
        /// Lee los parámetros sobre el jugador
        /// </summary>
        /// <param name="levelInfo"></param>
        private void GetPlayerParametersInfo(XmlNode levelInfo)
        {
            foreach (XmlNode levelPlayer in levelInfo.ChildNodes) //player
            {
                if (levelPlayer.Name == "run_speed")
                    GameManager.Instance.GameParameters.PlayerRunSpeed = float.Parse(levelPlayer.InnerText);
                else if (levelPlayer.Name == "jump_height")
                    GameManager.Instance.GameParameters.PlayerJumpHeight = float.Parse(levelPlayer.InnerText);
                else if (levelPlayer.Name == "melee_damage")
                    GameManager.Instance.GameParameters.PlayerMeleeDamage = int.Parse(levelPlayer.InnerText);
                else if (levelPlayer.Name == "abilities")
                {
                    foreach (XmlNode levelAbilities in levelPlayer.ChildNodes) //abilities
                    {
                        if (levelAbilities.Name == "fire")
                        {
                            foreach (XmlNode levelFire in levelAbilities.ChildNodes) //fire
                            {
                                if (levelFire.Name == "damage")
                                    GameManager.Instance.GameParameters.FireDamage = int.Parse(levelFire.InnerText);
                                else if (levelFire.Name == "cast_duration")
                                    GameManager.Instance.GameParameters.FireCastRate = float.Parse(levelFire.InnerText);
                                else if (levelFire.Name == "cast_rate")
                                    GameManager.Instance.GameParameters.FireCastRate = float.Parse(levelFire.InnerText);
                            }
                        }

                        else if (levelAbilities.Name == "ice")
                        {
                            foreach (XmlNode levelIce in levelAbilities.ChildNodes) //ice
                            {
                                if (levelIce.Name == "damage")
                                    GameManager.Instance.GameParameters.IceDamage = int.Parse(levelIce.InnerText);
                                else if (levelIce.Name == "cast_duration")
                                    GameManager.Instance.GameParameters.IceCastRate = float.Parse(levelIce.InnerText);
                                else if (levelIce.Name == "cast_rate")
                                    GameManager.Instance.GameParameters.IceCastDuration = float.Parse(levelIce.InnerText);
                            }
                        }
                    }
                }

            }
        }

        /// <summary>
        /// Lee los parámetros sobre los enemigos
        /// </summary>
        /// <param name="levelInfo"></param>
        private void GetEnemiesParametersInfo(XmlNode levelInfo)
        {
            foreach (XmlNode levelEnemies in levelInfo.ChildNodes) //enemies
            {
                if (levelEnemies.Name == "spider")
                {
                    foreach (XmlNode levelSpider in levelEnemies.ChildNodes) //spider
                    {
                        if (levelSpider.Name == "run_speed")
                            GameManager.Instance.GameParameters.SpiderRunSpeed = float.Parse(levelSpider.InnerText);
                        else if (levelSpider.Name == "melee_damage")
                            GameManager.Instance.GameParameters.SpiderMeleeDamage = int.Parse(levelSpider.InnerText);
                    }
                }

            }
        }

    }
}
