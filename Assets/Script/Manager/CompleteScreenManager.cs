using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CompleteScreenManager : ScreenManager
{
 
    [SerializeField] private TextMeshProUGUI scoreText;

    public override void InitScreen()
    {
        base.InitScreen();
        scoreText.text = gameManager.CurrentScore.ToString();
        soundManager.PlaySound("LevelWin");
    }

    public void CloseApp()
    {
        Application.Quit();
    }

    public void GotoHome()
    {
        gameManager.OnUserAction(appData.UserAction.Backbutton,ScreenName);
    }
}
