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

        public void Load(GameConfig gameConfig, TowersData towersData)
        {
            
            if (gameConfig.StartPlayerTowers == null || gameConfig.StartPlayerTowers.Length < towerButtons.Length)
            {
                Debug.LogError("No towers configured in GameConfig!");
                return;
            }

            for (int i = 0; i < towerButtons.Length; i++)
            {
                TowerInfo towerInfo = towersData.GetTowerInfo(gameConfig.StartPlayerTowers[i]);
                towerButtons[i].Load(towerInfo.Id, towerInfo.Cost, towerInfo.Name);
            }
        }
    }
}
