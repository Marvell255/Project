using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Image Bar;
    public float Speed = 0.5f;
    public Vector3 Percent;
    
    private const int MaxHeight = 452;
    private bool _force;
    private float _timer;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!_force) return;

        _timer += Time.deltaTime;

        Percent = Vector3.Lerp(new Vector3(Bar.rectTransform.sizeDelta.x, 10, 1), new Vector3(Bar.rectTransform.sizeDelta.x, MaxHeight, 1), Mathf.PingPong(_timer * Speed, 1));

        Bar.rectTransform.sizeDelta = Percent;
    }

    public void ForceStart()
    {
        _force = true;
        gameObject.SetActive(true);
        _timer = 0;
    }

    public float ForceEnd()
    {
        _force = false;
        gameObject.SetActive(false);
        return Mathf.PingPong(_timer * Speed, 1);
    }
}