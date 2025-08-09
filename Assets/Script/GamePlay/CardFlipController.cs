using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

[RequireComponent(typeof(RectTransform))]
public class CardFlipController : MonoBehaviour
{
    private EventManager _eventManager;
    
    [Header("Card Visuals")]
    public Image cardImage;             // Assign in Inspector
    public Sprite frontSprite;          // Card face
    public Sprite backSprite;           // Card back

    [Header("Flip Settings")]
    public float flipDuration = 0.25f;  // Total flip time (sec)
    public float bounceScale = 1.05f;   // Max scale during flip

    [Header("Audio")]
    public AudioClip flipSound;         // Assign in Inspector
    public AudioSource audioSource;     // Assign in Inspector

    public bool IsFaceUp { get; private set; }
    public bool IsFlipping { get; private set; }
    public bool IsMatched { get; private set; }

    public int CardID { get; private set; }
    public event Action<CardFlipController> OnFlipComplete;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        if (audioSource == null && flipSound != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        ResetCard();
    }

    public void Initialize(Sprite front, Sprite back, int cardId)
    {
        if (front == null)
            Debug.LogError("front is null" + cardId);
        frontSprite = front;
        backSprite = back;
        cardImage.sprite = backSprite;
        IsFaceUp = false;
        IsMatched = false;
        CardID=cardId;
        _eventManager = EventManager.Instance;
    }

    public void Flip()
    {
        if (IsFlipping || IsMatched || IsFaceUp) return;
           _eventManager.TriggerEvent(appData.OnCardSelected,this);
            StartCoroutine(FlipRoutine());
            
    }
    public void ForceFlipFaceUp()
    {
        StartCoroutine(FlipRoutine());
    }
   

    public void SetMatched(bool matched)
    {
        IsMatched = matched;
        if (matched)
        {
            
        }
    }

    private IEnumerator FlipRoutine()
    {
        IsFlipping = true;
        float halfDuration = flipDuration / 2f;
        Vector3 originalScale = rectTransform.localScale;

        // First half: rotate 0° → 90°, scale up
        float t = 0f;
        while (t < halfDuration)
        {
            t += Time.deltaTime;
            float progress = t / halfDuration;

            float yRot = Mathf.Lerp(0f, 90f, progress);
            rectTransform.localRotation = Quaternion.Euler(0f, yRot, 0f);

            float scale = Mathf.Lerp(1f, bounceScale, progress);
            rectTransform.localScale = originalScale * scale;

            yield return null;
        }

        // Mid-flip: swap sprite & play sound
        IsFaceUp = !IsFaceUp;
        cardImage.sprite = IsFaceUp ? frontSprite : backSprite;
        if (flipSound != null) audioSource.PlayOneShot(flipSound);

        // Second half: rotate 90° → 0°, scale down
        t = 0f;
        while (t < halfDuration)
        {
            t += Time.deltaTime;
            float progress = t / halfDuration;

            float yRot = Mathf.Lerp(90f, 0f, progress);
            rectTransform.localRotation = Quaternion.Euler(0f, yRot, 0f);

            float scale = Mathf.Lerp(bounceScale, 1f, progress);
            rectTransform.localScale = originalScale * scale;

            yield return null;
        }

        IsFlipping = false;
        OnFlipComplete?.Invoke(this);
    }

    public void ResetCard()
    {
        StopAllCoroutines();
        rectTransform.localRotation = Quaternion.identity;
        rectTransform.localScale = Vector3.one;
        IsFaceUp = false;
        IsMatched = false;
        cardImage.sprite = backSprite;
        IsFlipping = false;
    }
}
