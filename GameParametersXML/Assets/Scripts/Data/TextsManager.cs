using UnityEngine;
using System.Xml;
using System.Globalization;

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
            LoadTexts();
        }

        /// <summary>
        /// Parsea un string a float (1.5)
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private static float FloatParse(string number)
        {
            return float.Parse(number, CultureInfo.InvariantCulture.NumberFormat);
        }

        /// <summary>
        /// Inicializa valores del GameManager en los que va a guardar los datos del XML y empieza la lectura, se destruye cuando acaba
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

            //Cargamos el XML
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(currentFile.text);

            XmlNodeList levelsList = xmlDoc.GetElementsByTagName("gameParameters"); //Se coloca en el primer tag

            foreach (XmlNode levelIndex in levelsList)//gameParameters
            {
                foreach (XmlNode levelInfo in levelIndex)//general, players, enemies...
                {
                    switch (levelInfo.Name)
                    {
                        case "general":
                            GetGeneralParametersInfo(levelInfo);
                            break;
                        case "player":
                            GetPlayerParametersInfo(levelInfo);
                            break;
                        case "enemies":
                            GetEnemiesParametersInfo(levelInfo);
                            break;
                    }
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
                switch (levelGeneral.Name)
                {
                    case "time_count":
                        GameManager.Instance.GameParameters.TimeCount = int.Parse(levelGeneral.InnerText);
                        break;
                    case "death_duration":
                        GameManager.Instance.GameParameters.DeathDuration = FloatParse(levelGeneral.InnerText);
                        break;
                }
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
                switch (levelPlayer.Name)
                {
                    case "run_speed":
                        GameManager.Instance.GameParameters.PlayerRunSpeed = FloatParse(levelPlayer.InnerText);
                        break;
                    case "jump_height":
                        GameManager.Instance.GameParameters.PlayerJumpHeight = FloatParse(levelPlayer.InnerText);
                        break;
                    case "melee_damage":
                        GameManager.Instance.GameParameters.PlayerMeleeDamage = int.Parse(levelPlayer.InnerText);
                        break;
                    case "abilities":
                        foreach (XmlNode levelAbilities in levelPlayer.ChildNodes) //abilities
                        {
                            switch (levelAbilities.Name)
                            {
                                case "fire":
                                    foreach (XmlNode levelFire in levelAbilities.ChildNodes) //fire
                                    {
                                        switch (levelFire.Name)
                                        {
                                            case "damage":
                                                GameManager.Instance.GameParameters.FireDamage = int.Parse(levelFire.InnerText);
                                                break;
                                            case "cast_duration":
                                                GameManager.Instance.GameParameters.FireCastRate = FloatParse(levelFire.InnerText);
                                                break;
                                            case "cast_rate":
                                                GameManager.Instance.GameParameters.FireCastRate = FloatParse(levelFire.InnerText);
                                                break;
                                        }
                                    }
                                    break;
                                case "ice":
                                    foreach (XmlNode levelIce in levelAbilities.ChildNodes) //ice
                                    {
                                        switch (levelIce.Name)
                                        {
                                            case "damage":
                                                GameManager.Instance.GameParameters.IceDamage = int.Parse(levelIce.InnerText);
                                                break;
                                            case "cast_duration":
                                                GameManager.Instance.GameParameters.IceCastRate = FloatParse(levelIce.InnerText);
                                                break;
                                            case "cast_rate":
                                                GameManager.Instance.GameParameters.IceCastDuration = FloatParse(levelIce.InnerText);
                                                break;
                                        }
                                    }
                                    break;

                            }

                        }
                        break;
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
                switch (levelEnemies.Name)
                {
                    case "spider":
                        foreach (XmlNode levelSpider in levelEnemies.ChildNodes) //spider
                        {
                            switch (levelSpider.Name)
                            {
                                case "run_speed":
                                    GameManager.Instance.GameParameters.SpiderRunSpeed = FloatParse(levelSpider.InnerText);
                                    break;
                                case "melee_damage":
                                    GameManager.Instance.GameParameters.SpiderMeleeDamage = int.Parse(levelSpider.InnerText);
                                    break;
                            }

                        }
                        break;
                }

            }
        }

    }
}
