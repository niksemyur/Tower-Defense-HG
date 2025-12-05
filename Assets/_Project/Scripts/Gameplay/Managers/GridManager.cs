using UnityEngine;
using System.Collections.Generic;
using TowerDefense.Gameplay.Grid;
using System;

namespace TowerDefense.Gameplay.Managers
{
    // Управление игровой сеткой. Отслеживает свободные и занятые ячейки.
    public class GridManager : MonoBehaviour
    {
        public static GridManager Instance { get; private set; }

        [SerializeField] private GridCell[] cells; // Все ячейки сетки

        private List<GridCell> emptyCells = new List<GridCell>(); // Список свободных ячеек

        // Событие при изменении сетки (построена/уничтожена башня)
        public event Action OnGridChanged;

        // Инициализация менеджера (Singleton)
        public void Init()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        // Загрузка сетки - заполняем список свободных ячеек
        public void Load()
        {
            foreach (var cell in cells)
            {
                emptyCells.Add(cell);
            }
        }

        // Есть ли свободные ячейки?
        public bool IsHaveEmptyCells()
        {
            return emptyCells.Count > 0;
        }

        // Получить случайную свободную ячейку
        public GridCell GetRandomEmptyCell()
        {
            if (!IsHaveEmptyCells()) return null;
            return emptyCells[UnityEngine.Random.Range(0, emptyCells.Count)];
        }

        // Изменить состояние занятости ячейки
        public void SetGridBusyState(GridCell cell, bool state)
        {
            if (state) // Если ячейку занимают
            {
                if (emptyCells.Contains(cell))
                {
                    emptyCells.Remove(cell); // Убираем из свободных
                }
            }
            else // Если ячейку освобождают
            {
                if (!emptyCells.Contains(cell))
                {
                    emptyCells.Add(cell); // Добавляем в свободные
                }
            }
            cell.SetBusy(state);
            OnGridChanged?.Invoke(); // Уведомляем об изменении
        }
    }
}