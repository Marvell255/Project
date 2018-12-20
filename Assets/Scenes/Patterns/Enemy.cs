using System;
using UnityEngine;
using Zenject;

namespace Scenes.Patterns
{
	public class Enemy : MonoBehaviour
	{
		public class EnemyFactory : FactoryBindInfo
		{
			public EnemyFactory(Type factoryType) : base(factoryType)
			{
			}
		}
	}
}