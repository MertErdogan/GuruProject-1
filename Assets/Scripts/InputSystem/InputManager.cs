using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : SingleInstance<InputManager> {

    [SerializeField] private LayerMask _gridLayer;

    private Camera _camera;

    private void Awake() {
        _camera = Camera.main;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0) && !RaycastHelper.IsPointerOverUIObject()) {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100)) {
                if (hit.collider.TryGetComponent(out GridController controller)) {
                    if (controller.IsClicked) return;

                    controller.SetClicked();
                }
            }
        }
    }

}
