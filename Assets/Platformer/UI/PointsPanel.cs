using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointsPanel : MonoBehaviour
{
    public TextMeshProUGUI PointsLabel;

    private int _points;

    private void Start()
    {
        UpdatePointsLabelText();

        CheckPrefs();
    }

    private void CheckPrefs()
    {
        if (!PlayerPrefs.HasKey("welcome"))
        {
            PlayerPrefs.SetString("welcome", name);
            PlayerPrefs.Save(); // force
            
            print("welcome to Game");
        }
        
        PlayerPrefs.DeleteAll();
    }

    public void AddPoints(int value)
    {
        _points += value;

        UpdatePointsLabelText();
    }

    private void UpdatePointsLabelText()
    {
        PointsLabel.text = _points.ToString();
    }

    public static PointsPanel Instance()
    {
        return GameObject.Find("Canvas/PointsPanel").GetComponent<PointsPanel>();
    }
}