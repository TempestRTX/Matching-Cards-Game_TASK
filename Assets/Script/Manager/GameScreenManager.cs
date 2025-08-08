using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI; 

public class GameScreenManager : ScreenManager
{
    [SerializeField] private List<appData.GridDataObj> gridDatas;
    [SerializeField] private appData.BoardLayout currentBoardLayout;
    [SerializeField] private SpriteAtlas Card_Atlas;
    [SerializeField] private Sprite DefaultSprite;
    [SerializeField] private string SpriteCode;
    
    private appData.GridDataObj ActivegridData;
    public override void InitScreen()
    {
        base.InitScreen();
        currentBoardLayout = gameManager.GetCurrentBoardLayout();
        SetupGameScreen();
    }

    private void SetupGameScreen()
    {
        ActivegridData = gridDatas.Where(x => x.layout == currentBoardLayout).FirstOrDefault();
        ActivegridData.GridParent.SetActive(true);
        AssignCards();
       
    }

    private void AssignCards()
    {
        //Init ids
        List<int> CardIDs = new List<int>();
        for (int i = 0; i < ActivegridData.Cards.Count/2; i++)
        {
            CardIDs.Add(i);
            CardIDs.Add(i);
        }
        CardIDs = CardIDs.OrderBy(x => Random.value).ToList();
        int _cardindex = 0;
        foreach (var card in ActivegridData.Cards)
        {
            card.Initialize
            (Card_Atlas.GetSprite(SpriteCode + CardIDs[_cardindex].ToString()),
                DefaultSprite,
                CardIDs[_cardindex]);
            _cardindex++;
        }



    }

    
}