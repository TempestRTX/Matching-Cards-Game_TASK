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

    private void CheckifGameOver()
    {
       var matchedcards=ActivegridData.Cards.Where(x=>x.IsMatched).Count();
       if (matchedcards == ActivegridData.Cards.Count)
       {
           gameManager.OnUserAction(appData.UserAction.GameCompleted,ScreenName);
       }
    }
    

    private List<CardFlipController> pendingSelection = new List<CardFlipController>();
    private HashSet<int> matchedCardIDs = new HashSet<int>();

    private void OnCardSelected(object data)
    {
        CardFlipController card = (CardFlipController)data;

        
        if (matchedCardIDs.Contains(card.CardID) || card.IsFlipping || pendingSelection.Contains(card))
            return;

        pendingSelection.Add(card);

        
        if (pendingSelection.Count == 2)
        {
            var first = pendingSelection[0];
            var second = pendingSelection[1];
            pendingSelection.Clear();
            StartCoroutine(CheckCardPair(first, second));
        }
    }

    private IEnumerator CheckCardPair(CardFlipController card1, CardFlipController card2)
    {
        yield return new WaitUntil(() => card1.IsFaceUp && card2.IsFaceUp);

        if (card1.CardID == card2.CardID)
        {
            matchedCardIDs.Add(card1.CardID);
            gameManager.AddMatchPoints();
            soundManager.PlaySound("Success");
            card1.SetMatched(true);
            card2.SetMatched(true);
            CheckifGameOver();
        }
        else
        {
            gameManager.AddMismatchPenalty();
            yield return new WaitForSeconds(0.5f);
            soundManager.PlaySound("Fail");
            card1.ResetCard();
            card2.ResetCard();
        }
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