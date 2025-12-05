using UnityEngine;

namespace TowerDefense.Gameplay.Grid
{
    // Ячейка игровой сетки. Может быть занята башней или свободна.
    public class GridCell : MonoBehaviour
    {
        public bool IsBusy { get; private set; } // Занята ли ячейка

        // Установить состояние занятости
        public void SetBusy(bool state)
        {
            IsBusy = state;
        }
    }
}