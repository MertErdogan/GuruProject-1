using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompletedButtonController : BaseButtonController {

    #region Overrides

    protected override void HandleButtonClick() {
        base.HandleButtonClick();

        FinishController.Instance.MoveFinishLine();

        GameStateManager.Instance.SetGameState(GameState.Game);
    }

    #endregion

}
