using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : GenericSingleton<GameManager>
{
 
  [SerializeField] private ScreenOrientation deviceOrientation = ScreenOrientation.LandscapeLeft;
  [SerializeField] private appData.BoardLayout ActiveboardLayout;
  public bool IsInit = false;

  private void Start()
  {
    SetDeviceOrientation();
    
  }
  private void SetDeviceOrientation()
  {
    Screen.orientation = deviceOrientation;
  }
  
  #region Game Data

  public (int rows, int cols) GetBoardLayout()
  {
    return appData.BoardLayoutExtensions.GetDimensions(this.ActiveboardLayout);
  }

  public void ChangeBoardLayout(int layoutid)
  {
    ActiveboardLayout=(appData.BoardLayout) layoutid;
  }
  #endregion


 
  
  
  #region State Manager

  public void OnUserAction(appData.UserAction action, appData.AppState state)
  {
    switch (action)
    {
      case appData.UserAction.Backbutton:
        //Go back to previous screen
        break;
      case appData.UserAction.PlayGame:
        ProcessUserPlayAction(state);
        break;
      case appData.UserAction.RestartGame:
        ProcessUserRestartAction(state);
        break;
      case appData.UserAction.LevelSelected:
        ProcessUserLevelSelect(state);
        break;
    }
  }

  private void ProcessUserLevelSelect(appData.AppState state)
  {
    if (state==appData.AppState.LevelScreen)
    {
      ChangeAppState(appData.AppState.GameScreen);
    }
  }

  private void ProcessUserPlayAction( appData.AppState state)
  {
    if (state==appData.AppState.SplashScreen)
    {
      ChangeAppState(appData.AppState.LevelScreen);
    }
   
  }

  private void ProcessUserRestartAction(appData.AppState state)
  {
    ChangeAppState(state);  
  }

  private void ChangeAppState(appData.AppState state)
  {
    SceneManager.LoadScene(state.ToString());
  }
  
  #endregion

}
