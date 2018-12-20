using System;
using System.Collections;
using System.Collections.Generic;
using UI.SkillTimer;
using UnityEngine;

public class SkillsPanel : MonoBehaviour
{
    public GameObject HorseSkill, MagnetSkill;

    public GameObject BankPrefab;

    private HeroPortraitController _heroPortraitController;

    private void Start()
    {
        _heroPortraitController = GameObject.FindWithTag("Player").GetComponent<HeroPortraitController>();
    }

    public static SkillsPanel Instance()
    {
        return GameObject.Find("Canvas/SkillsPanel").GetComponent<SkillsPanel>();
    }

    public void AddHorseSkill(float duration = 10)
    {
        var skillPrefab = Instantiate(HorseSkill, Vector3.zero, transform.rotation, transform);
        skillPrefab.GetComponent<BigJump>().EnableSkill(duration, _heroPortraitController);
    }

    public void AddMagnetSkill(float duration = 10)
    {
        var skillPrefab = Instantiate(MagnetSkill, Vector3.zero, transform.rotation, transform);
        skillPrefab.GetComponent<Magnet>().EnableSkill(duration, _heroPortraitController);
    }

    public void OpenBank()
    {
        Instantiate(BankPrefab, GameObject.Find("Canvas").transform);
    }
}