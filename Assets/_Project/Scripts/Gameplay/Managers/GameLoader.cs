using TowerDefense.UI;
using UnityEngine;
using TowerDefense.Configs;

namespace TowerDefense.Gameplay.Managers
{
    // Главный загрузчик игры. Инициализирует все системы в правильном порядке.
    // ВАЖНО: Сейчас используем простой подход с прямыми ссылками через инспектор.
    // Не переходим на Zenject/DI фреймворки пока проект простой - это overkill.
    // Когда систем станет 20+ и зависимости запутаются - тогда рассмотрим DI.
    public class GameLoader : MonoBehaviour
    {
        [Header("Base Systems")]
        [SerializeField] private CurrencyManager currencyManager; // Управление деньгами
        [SerializeField] private GridManager gridManager;         // Игровая сетка
        [SerializeField] private TowerBuilder towerBuilder;       // Строитель башен

        [Header("Dependent Systems")]
        [SerializeField] private GameUIController gameUIController; // UI игры
        [SerializeField] private EnemyFactory enemyFactory;         // Фабрика врагов
        [SerializeField] private EnemySpawner enemySpawner;         // Спавнер врагов
        [SerializeField] private RewardManager rewardManager;       // Награды за убийства

        [Header("Configs")]
        [SerializeField] private GameConfig gameConfig;   // Основные настройки
        [SerializeField] private TowersData towersData;   // Конфиг башней
        [SerializeField] private EnemyData enemyData;     // Конфиг врагов

        private void Awake()
        {
            InitSystems();
        }
        private void Start()
        {
            LoadSystems();
        }

        //Подписки на события, создание синглтонов
        private void InitSystems()
        {
            // 1. Базовые системы (нижний уровень)
            gridManager.Init();
            currencyManager.Init(gameConfig);
            towerBuilder.Init(towersData);

            // 2. Системы, зависящие от базовых (верхний уровень)
            gameUIController.Init();
            enemyFactory.Init(enemyData);
            enemySpawner.Init(enemyFactory);
            rewardManager.Init();
        }

        //Инициализация с обновлением данных, вызывает ивенты после подписания в Init
        private void LoadSystems ()
        {
            // 1. Базовые системы (нижний уровень)
            gridManager.Load();
            currencyManager.Load();

            // 2. Системы, зависящие от базовых (верхний уровень)
            gameUIController.Load(gameConfig, towersData);
            enemySpawner.Load();
        }

        // Примечание по архитектуре:
        // Сейчас используем простой подход с прямыми ссылками в инспекторе.
        // Это нормально для проекта такого масштаба (10-15 классов).
        // Альтернативы (Zenject/DI фреймворки) добавят сложность без пользы.
        // Когда систем станет 30+ и зависимости запутаются - пересмотрим.
    }
}
