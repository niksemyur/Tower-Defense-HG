using UnityEngine;
using Zenject;
using System.Collections.Generic;
using TowerDefense.Gameplay.Grid;
using System;
using TowerDefense.Signals;

namespace TowerDefense.Gameplay.Managers
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private GridCell[] _cells; // Ячейки сетки

        private SignalBus _signalBus;
        private List<GridCell> _emptyCells = new List<GridCell>(); // Список свободных ячеек  

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        // Загрузка сетки - заполняем список свободных ячеек
        public void Initialize()
        {
            foreach (var cell in _cells)
            {
                _emptyCells.Add(cell);
            }
        }

        // Есть ли свободные ячейки?
        public bool IsHaveEmptyCells()
        {
            return _emptyCells.Count > 0;
        }

        // Получить случайную свободную ячейку
        public GridCell GetRandomEmptyCell()
        {
            if (!IsHaveEmptyCells()) return null;
            return _emptyCells[UnityEngine.Random.Range(0, _emptyCells.Count)];
        }

        // Изменить состояние занятости ячейки
        public void SetGridBusyState(GridCell cell, bool state)
        {
            if (state) // Если ячейку занимают
            {
                if (_emptyCells.Contains(cell))
                {
                    _emptyCells.Remove(cell); // Убираем из свободных
                }
            }
            else // Если ячейку освобождают
            {
                if (!_emptyCells.Contains(cell))
                {
                    _emptyCells.Add(cell); // Добавляем в свободные
                }
            }
            cell.SetBusy(state);
            var signal = new OnGridChangedSignal();
            _signalBus.Fire(signal); // Уведомляем об изменении
        }
    }
}