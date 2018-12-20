using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseSkill : MonoBehaviour, ICollect
{
    public void Collect()
    {
        SkillsPanel.Instance().AddHorseSkill();

        Destroy(gameObject);
    }
}