using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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
    
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI comboText;
    
    private appData.GridDataObj ActivegridData;
    public override void InitScreen()
    {
        base.InitScreen();
        currentBoardLayout = gameManager.GetCurrentBoardLayout();
        SetupGameScreen();
        eventManager.Subscribe(appData.OnCardSelected,OnCardSelected);
        eventManager.Subscribe(appData.OnScoreUpdated,UpdateScore);
        UpdateScore(this);
    }

    private void UpdateScore(object args)
    {
        scoreText.text="Score : " + gameManager.CurrentScore.ToString();
        comboText.text = "Combo X"+ gameManager.comboCount.ToString();
    }
    

    int ? firstid=null;
    int ? secondId=null;
    CardFlipController cardFlipControllerOne = new CardFlipController();
    private void OnCardSelected(object data)
    {
        CardFlipController card = (CardFlipController)data;
        if (firstid == null)
        {
            firstid = card.CardID;
            cardFlipControllerOne = card;
            return;
        }
        secondId = card.CardID;
        StartCoroutine(CheckCardPair(card));
    }

    private IEnumerator CheckCardPair(CardFlipController cardFlipController)
    {
        yield return new WaitUntil(() => cardFlipController.IsFaceUp);
        if (firstid == secondId)
        {
             
            //Correct pair
            gameManager.AddMatchPoints();
            soundManager.PlaySound("Sucess");
        }
        else
        {
            //Wrong pair
            gameManager.AddMismatchPenalty();
            yield return new WaitForSeconds(0.5f);
            soundManager.PlaySound("Fail");
            cardFlipController.ResetCard();
            cardFlipControllerOne.ResetCard();
            
        }
        firstid=null;
        secondId=null;

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