using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : GenericSingleton<GameManager>
{
 
  [SerializeField] private ScreenOrientation deviceOrientation = ScreenOrientation.LandscapeLeft;
  
  public bool IsInit = false;

  private void Start()
  {
    SetDeviceOrientation();
    
  }

 
  private void SetDeviceOrientation()
  {
    Screen.orientation = deviceOrientation;
  }


 
  
  
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
    }
  }

  private void ProcessUserPlayAction( appData.AppState state)
  {
    if (state==appData.AppState.SplashScreen)
    {
      ChangeAppState(appData.AppState.GameScreen);
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
