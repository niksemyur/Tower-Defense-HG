using Zenject;
using TowerDefense.Gameplay.Managers;
using TowerDefense.UI;
public class GameInitializer: IInitializable
{
    [Inject] private GameUIController _gameUIController;
    [Inject] private CurrencyManager _currencyManager; 
    [Inject] private RewardManager _rewardManager; 
    [Inject] private GridManager _gridManager;
    [Inject] private EnemySpawner _enemySpawner;
    [Inject] private TowerBuilder _towerBuilder;

    public void Initialize()
    {
        _currencyManager.Initialize();
        _rewardManager.Initialize();
        _gridManager.Initialize();
        _enemySpawner.Initialize();
        _towerBuilder.Initialize();
        _gameUIController.Initialize();
    }
}
