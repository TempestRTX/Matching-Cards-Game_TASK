using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI; 

public class GameScreenManager : ScreenManager
{
    [SerializeField] private List<appData.GridDataObj> gridDatas;
    [SerializeField] private appData.BoardLayout currentBoardLayout;
    public override void InitScreen()
    {
        base.InitScreen();
        currentBoardLayout = gameManager.GetCurrentBoardLayout();
    }

    private void SetupGameScreen()
    {
        var activescreen = gridDatas.Where(x => x.layout == currentBoardLayout).FirstOrDefault();
        activescreen.GridParent.SetActive(true);
        AssignCards();
    }

    private void AssignCards()
    {
        
    }

    
}