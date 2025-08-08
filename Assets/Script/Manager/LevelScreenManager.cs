using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScreenManager : ScreenManager
{
    [SerializeField] private appData.BoardLayout boardLayout;

    public void ChangeBoardLayout(int layoutid)
    {
        gameManager.ChangeBoardLayout(layoutid);
        gameManager.OnUserAction(appData.UserAction.LevelSelected,ScreenName);
    }
}
