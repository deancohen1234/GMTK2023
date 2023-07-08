using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    public TextMeshProUGUI rankText;
    public TextMeshProUGUI scoreText;
    public CanvasGroup canvasGroup;

    public void Show(float score, string rank)
    {
        score *= 100f;

        rankText.text = rank;
        scoreText.text = score.ToString("0.00") + "%";

        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void Hide()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

    }
}
