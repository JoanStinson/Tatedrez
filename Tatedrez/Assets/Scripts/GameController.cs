using static JGM.Game.LocalizationService;
using Random = UnityEngine.Random;

namespace JGM.Game
{
    public class GameController
    {
        private readonly ILocalizationService m_localizationService;

        public GameController(ILocalizationService localizationService)
        {
            m_localizationService = localizationService;
        }

        public GameModel BuildGameModel(GameSettings gameSettings)
        {
            return new GameModel(gameSettings);
        }

        public void ChangeLanguageToRandom()
        {
            Language currentLanguage = m_localizationService.currentLanguage;
            Language randomLanguage;

            do
            {
                randomLanguage = (Language)Random.Range(0, (int)Language.Count);
            }
            while (randomLanguage == currentLanguage);

            m_localizationService.SetLanguage(randomLanguage);
        }

        public void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}