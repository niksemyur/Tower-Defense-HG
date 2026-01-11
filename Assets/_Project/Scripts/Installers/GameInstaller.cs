using TowerDefense.UI;
using UnityEngine;
using Zenject;
using TowerDefense.Configs;
using TowerDefense.Gameplay.Managers;

namespace TowerDefense.Gameplay.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [Header("Scene Managers")]
        [SerializeField] private GameUIController _gameUIController; // UI игры
        [SerializeField] private CurrencyManager _currencyManager;   // Управление валютой
        [SerializeField] private RewardManager _rewardManager;         // Управление наградой за убийство врагов
        [SerializeField] private GridManager _gridManager;           // Игровая сетка
        [SerializeField] private TowerBuilder _towerBuilder;         // Строитель башен
        [SerializeField] private EnemySpawner _enemySpawner;         // Спавнер врагов

        [Header("Configs")]
        [SerializeField] private GameConfig _gameConfig;            // Основные настройки
        [SerializeField] private LevelConfig _levelConfig;          // Управление параметрами игрового уровня

        public override void InstallBindings()
        {
            // Конфиги
            Container.Bind<GameConfig>().FromInstance(_gameConfig).AsSingle();
            Container.Bind<LevelConfig>().FromInstance(_levelConfig).AsSingle();

            // Фабрики
            Container.Bind<EnemyFactory>().AsSingle().NonLazy();
            Container.Bind<TowerFactory>().AsSingle().NonLazy();

            // Менеджеры
            Container.Bind<CurrencyManager>().FromInstance(_currencyManager).AsSingle();
            Container.Bind<RewardManager>().FromInstance(_rewardManager).AsSingle();
            Container.Bind<GridManager>().FromInstance(_gridManager).AsSingle();
            Container.Bind<TowerBuilder>().FromInstance(_towerBuilder).AsSingle();
            Container.Bind<EnemySpawner>().FromInstance(_enemySpawner).AsSingle();
            Container.Bind<GameUIController>().FromInstance(_gameUIController).AsSingle();

            Container.BindInterfacesAndSelfTo<GameInitializer>().AsSingle().NonLazy();
        }

    }
}
