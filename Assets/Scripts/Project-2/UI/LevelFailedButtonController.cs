using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFailedButtonController : BaseButtonController {

    #region Overrides

    protected override void HandleButtonClick() {
        base.HandleButtonClick();

        SceneManager.LoadScene("Project-2-Main");
    }

    #endregion

}
