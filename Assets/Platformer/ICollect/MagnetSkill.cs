using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetSkill : MonoBehaviour, ICollect
{
	public void Collect()
	{
		SkillsPanel.Instance().AddMagnetSkill();

		Destroy(gameObject);
	}
}