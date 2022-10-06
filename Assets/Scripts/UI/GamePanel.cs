using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePanel : Panel
{
    [SerializeField] private TextMeshProUGUI pointTXT;

    [SerializeField] private Panel inputPanel;

    private bool _toolState;


    public void ToolButton()
    {
        _toolState = !_toolState;

        if (_toolState)
            inputPanel.Appear();
        else
            inputPanel.Disappear();
    }

    public void SetPointText(int value)
    {
        pointTXT.text = value.ToString();
    }
}