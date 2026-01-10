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
        [SerializeField] private CurrencyManager _currencyManager;   // Управление валютой
        [SerializeField] private GridManager _gridManager;           // Игровая сетка
        [SerializeField] private TowerBuilder _towerBuilder;         // Строитель башен

        [Header("Dependent Systems")]
        [SerializeField] private GameUIController _gameUIController; // UI игры
        [SerializeField] private EnemySpawner _enemySpawner;         // Спавнер врагов

        [Header("Configs")]
        [SerializeField] private GameConfig _gameConfig;             // Основные настройки
        [SerializeField] private LevelConfig _levelConfig;          // Управление параметрамми игрового уровня

        private void Awake()
        {
            InitSystems();
        }
        private void Start()
        {
            LoadSystems();
        }

        private void InitSystems()
        {
            _currencyManager.Init(_gameConfig);
            _enemySpawner.Init(_levelConfig);
            _gameUIController.Init(_gameConfig);
        }

        private void LoadSystems ()
        {
            _gridManager.Load();
            _currencyManager.Load();
            _enemySpawner.Load();
        }

        // Примечание по архитектуре:
        // Сейчас используем простой подход с прямыми ссылками в инспекторе.
        // Это нормально для проекта такого масштаба (10-15 классов).
        // Альтернативы (Zenject/DI фреймворки) добавят сложность без пользы.
        // Когда систем станет 30+ и зависимости запутаются - пересмотрим.
    }
}
