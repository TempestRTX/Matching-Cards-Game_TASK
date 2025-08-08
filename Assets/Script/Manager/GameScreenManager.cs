using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class GameScreenManager : ScreenManager
{
    [Header("Card Settings")]
    [SerializeField] private GameObject defaultCardPrefab;
    [SerializeField] private int row;
    [SerializeField] private int column;

    [Header("Layout Settings")]
    [SerializeField] private GameObject cardParent;
    [SerializeField] private GridLayoutGroup gridLayout;

    public override void InitScreen()
    {
        base.InitScreen();

        // Get layout from GameManager
        (row, column) = gameManager.GetBoardLayout();

        // Ensure GridLayout matches column count
        gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayout.constraintCount = column;

        
        AutoAdjustCellSize();

        // Create card deck
        CreateDeckOfCards(row * column);
    }

    private void CreateDeckOfCards(int numberOfCards)
    {
        // Clean up old cards
        foreach (Transform child in cardParent.transform)
        {
            Destroy(child.gameObject);
        }

        // Spawn new cards
        for (int i = 0; i < numberOfCards; i++)
        {
            Instantiate(defaultCardPrefab, cardParent.transform);
        }
    }

    private void AutoAdjustCellSize()
    {
        RectTransform parentRect = cardParent.GetComponent<RectTransform>();
        if (parentRect != null)
        {
            float totalSpacing = gridLayout.spacing.x * (column - 1);
            float cellWidth = (parentRect.rect.width - totalSpacing - gridLayout.padding.left - gridLayout.padding.right) / column;
            float cellHeight = cellWidth; // square cards — adjust if needed
            gridLayout.cellSize = new Vector2(cellWidth, cellHeight);
        }
    }
}