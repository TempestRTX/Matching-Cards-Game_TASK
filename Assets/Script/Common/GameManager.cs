using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : GenericSingleton<GameManager>
{
 
  [SerializeField] private ScreenOrientation deviceOrientation = ScreenOrientation.LandscapeLeft;
  [SerializeField] private appData.BoardLayout ActiveboardLayout;
  public bool IsInit = false;

  private void Start()
  {
    //SetDeviceOrientation();
    IsInit = true;
    InitScore();
   
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

  public appData.BoardLayout GetCurrentBoardLayout()
  {
    return ActiveboardLayout;
  }

  public void ChangeBoardLayout(int layoutid)
  {
    ActiveboardLayout=(appData.BoardLayout) layoutid;
  }
  #endregion

  #region Score Logic

  [SerializeField] private int matchPoints = 100;
  [SerializeField] private int comboBonus = 50;
  [SerializeField] private int mismatchPenalty = 10;

  public int CurrentScore { get; private set; }
  public int comboCount { get; private set; }

  public void InitScore()
  {
    CurrentScore = 0;
    comboCount = 0;

  }

  public void AddMatchPoints()
  {
    comboCount++;
    int points = matchPoints + (comboCount - 1) * comboBonus;
    CurrentScore += points;
    SaveScore();
  }

  public void AddMismatchPenalty()
  {
    comboCount = 0;
    CurrentScore -= mismatchPenalty;
    if (CurrentScore < 0) CurrentScore = 0;
    SaveScore();
  }

  public void UpdateScorefromLoadData(int score, int combo)
  {
    CurrentScore = score;
    comboCount = combo;
  }

  private void SaveScore()
  {
    EventManager.Instance.TriggerEvent(appData.OnScoreUpdated);
    PlayerPrefs.SetInt("Score", CurrentScore);
    PlayerPrefs.SetInt("Combo", comboCount);
    PlayerPrefs.Save();
  }

  private void LoadScore()
  {
    CurrentScore = PlayerPrefs.GetInt("Score", 0);
    comboCount = PlayerPrefs.GetInt("Combo", 0);
  }

  #endregion

  
  
  #region State Manager

  public void OnUserAction(appData.UserAction action, appData.AppState state)
  {
    switch (action)
    {
      case appData.UserAction.Backbutton:
        //Go back to previous screen
        ProcessBackButton(state);
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
      case appData.UserAction.GameCompleted:
        ProcessUserGameComplete(state);
        break;
    }
  }

  private void ProcessUserGameComplete(appData.AppState state)
  {
    if (state==appData.AppState.GameScreen)
    {
      ChangeAppState(appData.AppState.CompleteScreen);
    }
   
  }

  private void ProcessBackButton(appData.AppState state)
  {
    if (state == appData.AppState.CompleteScreen)
    {
      ChangeAppState(appData.AppState.LevelScreen);
    }
    else if(state==appData.AppState.GameScreen)
    {
      ChangeAppState(appData.AppState.LevelScreen);
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


  #region Save Game

  public void SaveGame(appData.BoardLayout layout ,appData.GameSaveData saveData )
  {
    PlayerPrefs.SetString(layout.ToString(), JsonUtility.ToJson(saveData));
    PlayerPrefs.Save();
    Debug.Log("Game saved.");
  }

  public appData.GameSaveData LoadGame(appData.BoardLayout layout)
  {
    var Data=PlayerPrefs.GetString(layout.ToString());
    return JsonUtility.FromJson<appData.GameSaveData>(Data);
  }

  public void DeleteGame(appData.BoardLayout layout)
  {
    PlayerPrefs.DeleteKey(layout.ToString());
  }

  #endregion
}
