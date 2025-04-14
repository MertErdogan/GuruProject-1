using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonController : BaseButtonController {

    #region Overrides

    protected override void HandleButtonClick() {
        base.HandleButtonClick();

        GameStateManager.Instance.SetGameState(GameState.Game);
    }

    #endregion

}
