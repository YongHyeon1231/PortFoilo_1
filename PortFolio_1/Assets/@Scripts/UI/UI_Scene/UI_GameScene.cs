using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameScene : UI_Base
{
    [SerializeField]
    TextMeshProUGUI _killCountText;

    [SerializeField]
    Slider _gemSlider;

    [SerializeField]
    TMP_Text _waveValueText;

    [SerializeField]
    Button _pauseButton;

    public void SetGemCountRatio(float ratio)
    {
        _gemSlider.value = ratio;
    }

    public void SetKillCount(int killCount)
    {
        _killCountText.text = $"{killCount}";
    }

    public void SetWaveValueText(int waveValueText)
    {
        _waveValueText.text = $"{waveValueText}";
    }

    bool _pressed = false;
    public void SetPauseButton()
    {
        if (_pressed == false)
        {
            Time.timeScale = 0;
            _pressed = true;
        }
        else
        {
            Time.timeScale = 1;
            _pressed = false;
        } 
    }
}
