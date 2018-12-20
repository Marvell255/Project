using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
	public float ShakeTime;
	public float ShakeAmount;
	public float ShakeSpeed;
	private Transform CamTransform;

	private void Start () 
	{
		CamTransform = Camera.main.transform;
		StartCoroutine(Shake());
	}

	public IEnumerator Shake()
	{
		Vector3 origPosition = CamTransform.localPosition;
		float elapsedTime = 0.0f;
		while(elapsedTime < ShakeTime)
		{
			Vector3 randomPoint = origPosition + Random.insideUnitSphere * ShakeAmount;
			CamTransform.localPosition = Vector3.Lerp(CamTransform.localPosition, randomPoint, Time.deltaTime * ShakeSpeed);
			yield return null;
			elapsedTime += Time.deltaTime;
		}
		CamTransform.localPosition = origPosition;
	}
}