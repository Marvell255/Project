using System.Collections;
using System.Collections.Generic;
using UI.SkillTimer;
using UnityEngine;

public class Magnet : SkillTimer
{
    protected override void OnStartSkillEffect()
    {
        Hero.MagnetActive = true;
    }

    protected override void OnFinishSkillEffect()
    {
        Hero.MagnetActive = false;
    }
}