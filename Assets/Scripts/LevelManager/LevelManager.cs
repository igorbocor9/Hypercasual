using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public Transform container;
    public List<GameObject> levels;
    [SerializeField] private int _index;
    private GameObject _currentLevel;

    private void Awake()
    {
        SpawnNextLevel();
    }

    void SpawnNextLevel()
    {
        if (_currentLevel != null)
        {
            Destroy(_currentLevel);
            _index++;

            if (_index >= levels.Count)
            {
                ResetLevelIndex();
            }
        }

        _currentLevel = Instantiate(levels[_index], container);
        _currentLevel.transform.localPosition = Vector3.zero;
    }

    void ResetLevelIndex()
    {
        _index = 0;
    }
}
