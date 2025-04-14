using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : SingleInstance<GridManager> {

    [SerializeField] private GridPool _pool;

    private List<GridController> _gridControllers;

    #region Spawn/Despawn Grid

    public GridController SpawnGridController() {
        GridController grid = _pool.GetObjectFromPool();

        _gridControllers.Add(grid);

        return grid;
    }

    public void DespawnGridController(GridController controller) {
        controller.SetPosition(transform, Vector3.zero, true);

        controller.Deactivate();
        _pool.AddObjectToPool(controller);
    }

    public void ClearGridControllers(int size) {
        if (_gridControllers != null && _gridControllers.Count > 0) {
            for (int i = 0; i < _gridControllers.Count; i++) {
                DespawnGridController(_gridControllers[i]);
            }
        }

        _gridControllers = new List<GridController>(size);
    }

    #endregion

    #region Get Grids

    public GridController GetGridController(Vector3 gridIndex) {
        GridController grid = _gridControllers.Find(g => g.GridIndex == gridIndex);
        if (grid == null) {
            return null;
        }

        return grid;
    }

    #endregion

}
