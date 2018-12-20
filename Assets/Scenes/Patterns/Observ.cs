using System.Collections.Generic;
using UnityEngine;

public class Observ : MonoBehaviour
{
	private void Start()
	{
		ConcreteSubject s = new ConcreteSubject();
		s.Atach(new ConreteObserver(s, "X"));
		s.Atach(new ConreteObserver(s, "Y"));
		s.Atach(new ConreteObserver(s, "Z"));

		s.SubjectState = "ABC";
		s.Notify();

		s.SubjectState = "666";
		s.Notify();
	}
}

public class ConcreteSubject : Subject
{
	private string _subjectState;

	public string SubjectState
	{
		get { return _subjectState; }
		set { _subjectState = value; }
	}
}

public class Subject
{
	private List<Observer> _observers = new List<Observer>();

	public void Atach(Observer observer)
	{
		_observers.Add(observer);
	}

	public void Detach(Observer observer)
	{
		_observers.Remove(observer);
	}

	public void Notify()
	{
		foreach (var item in _observers)
		{
			item.Update();
		}
	}
}

public abstract class Observer
{
	public abstract void Update();
}

public class ConreteObserver : Observer
{
	private string _name;
	private string _observerState;
	private ConcreteSubject _subject;

	public ConreteObserver(ConcreteSubject subject, string name)
	{
		this._subject = subject;
		this._name = name;
	}
    
	public override void Update()
	{
		_observerState = _subject.SubjectState;
		Debug.Log("Observer" + _name + "new state is " + _observerState);
	}

	public ConcreteSubject Subject
	{
		get { return _subject; }
		set { _subject = value; }
	}
    
}

// ngix - ssl
// bluetooth wifi
