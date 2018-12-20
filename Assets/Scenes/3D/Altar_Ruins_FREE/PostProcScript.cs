using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class PostProcScript : MonoBehaviour
{
    private PostProcessingProfile _postProcessingProfile;

    private VignetteModel.Settings _vignetteSettings;

    private void Start()
    {
        _postProcessingProfile = GetComponent<PostProcessingBehaviour>().profile;

        _vignetteSettings = _postProcessingProfile.vignette.settings;
        _color = _vignetteSettings.color;

        InvokeRepeating("RandomColor", 0, 3f);
    }

    private Color _color;

    private void Update()
    {
        _vignetteSettings.color = _color;
        _postProcessingProfile.SetDirty();
//        _vignetteSettings.color = Color.Lerp(_vignetteSettings.color, _color, .4f);
    }

    private void RandomColor()
    {
//        _postProcessingProfile.vignette.enabled = !_postProcessingProfile.vignette.enabled;
        _color = Random.ColorHSV();
        _color.a = 1;
    }
}