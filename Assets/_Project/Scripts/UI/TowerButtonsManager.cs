using UnityEngine;
using TowerDefense.Configs;

namespace TowerDefense.UI
{
    public class TowerButtonsManager : MonoBehaviour
    {
        [SerializeField] private TowerButton[] towerButtons;

        public void Init()
        {
            foreach (var buttonController in towerButtons)
            {
                buttonController.Init();
            }
        }

        public void Load(GameConfig gameConfig)
        {
            
            if (gameConfig.PlayerTowers == null || gameConfig.PlayerTowers.Length < towerButtons.Length)
            {
                Debug.LogError("No towers configured in GameConfig!");
                return;
            }

            for (int i = 0; i < towerButtons.Length; i++)
            {
                towerButtons[i].Load(gameConfig.PlayerTowers[i]);
            }
        }
    }
}
