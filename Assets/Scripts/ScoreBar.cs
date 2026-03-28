using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    private void Update()
    {
        if (GameManager.Instance != null)
        {
            slider.value = GameManager.score;
        }
    }
}
