using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class HeroInputControl : MonoBehaviour
{
    private Button _button;
    private HeroPortraitController _heroPortraitController;

    private void Start()
    {
        _heroPortraitController = GameObject.FindWithTag("Player").GetComponent<HeroPortraitController>();
    }

    public void PressedJump()
    {
        _heroPortraitController.PressedJump();
    }

    public void LUp()
    {
        _heroPortraitController.LeftButtonPressed = false;
    }

    public void LDown()
    {
        _heroPortraitController.LeftButtonPressed = true;
    }

    public void RUp()
    {
        _heroPortraitController.RightButtonPressed = false;
    }

    public void RDown()
    {
        _heroPortraitController.RightButtonPressed = true;
    }
}