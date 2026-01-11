using UnityEngine;
using TowerDefense.Configs;

[CreateAssetMenu(fileName = "TowerDataBase", menuName = "_Assets/_Project/Configs/TowersDataBase")]
public class TowerDataBase : ScriptableObject
{
    [SerializeField] private TowerData[] _towers;

    public TowerData GetTower (string towerID)
    {
        foreach (var tower in _towers)
        {
            if (tower.TowerId == towerID) return tower;
        }
        Debug.LogError($"Башня с ID '{towerID}' не найдена");
        return null;
    }
}
