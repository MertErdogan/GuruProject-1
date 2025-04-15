using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackMaterialManager : SingleInstance<StackMaterialManager> {

    [SerializeField] private List<Material> _stackMaterials;

    #region Get Material

    public Material GetStackMaterial() {
        return _stackMaterials[Random.Range(0, _stackMaterials.Count)];
    }

    #endregion

}
