using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoopingScroll : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private float scrollSpeed = 50f;
    
    private RectTransform contentRectTransform;
    private HorizontalLayoutGroup layoutGroup;
    private float contentWidth;
    private float normalizedPosition;
    
    private void Start()
    {
        contentRectTransform = scrollRect.content;
        layoutGroup = contentRectTransform.GetComponent<HorizontalLayoutGroup>();
        contentWidth = CalculateContentWidth();
    }

    private void Update()
    {
        float deltaScroll = scrollSpeed * Time.deltaTime;
        float currentScroll = scrollRect.horizontalNormalizedPosition;
        
        normalizedPosition += deltaScroll / contentWidth;
        normalizedPosition %= 1f;
        
        scrollRect.horizontalNormalizedPosition = normalizedPosition;
    }

    private float CalculateContentWidth()
    {
        float spacing = layoutGroup.spacing;
        float childCount = contentRectTransform.childCount;
        float childWidth = 75;

        float contentWidth = (childWidth + spacing) * childCount - spacing;
        return contentWidth;
    }
}
