﻿using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Zenject;

public class FPSInstaller : MonoInstaller
{
	public override void InstallBindings()
	{
		Container.Bind<InjectClass>().AsSingle();
	}
}
