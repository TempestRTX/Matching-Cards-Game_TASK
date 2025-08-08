using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class appData : MonoBehaviour
{
    public enum AppState
    {
        SplashScreen,
        GameScreen,
        LevelScreen
    }

    public enum UserAction
    {
        Backbutton,
        PlayGame,
        RestartGame,
        LevelSelected
        
    }
    
    public enum BoardLayout
    {
        TwoByTwo=0,   // 2x2
        TwoByThree=1, // 2x3
        FiveBySix=2   // 5x6
    }

    public static class BoardLayoutExtensions
    {
        public static (int rows, int cols) GetDimensions( BoardLayout layout)
        {
            switch (layout)
            {
                case BoardLayout.TwoByTwo:
                    return (2, 2);
                case BoardLayout.TwoByThree:
                    return (2, 3);
                case BoardLayout.FiveBySix:
                    return (5, 6);
                default:
                    return (2, 2);
            }
        }
    }



    #region Events

    public static string OnCardGrouped = "OnCardGrouped";
    public static string OnCardSelected = "OnCardSelected";
    public static string OnGroupDestroyed = "OnGroupDestroyed";

    #endregion
  



}
