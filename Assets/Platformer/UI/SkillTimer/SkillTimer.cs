using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.SkillTimer
{
    public class SkillTimer : MonoBehaviour
    {
        public float Duration;

        protected HeroPortraitController Hero;

        private float _durationVar;
        private Image[] _images;
        private bool _skillEnabled;

        public void EnableSkill(float duration, HeroPortraitController hero)
        {
            _images = GetComponentsInChildren<Image>();
            Hero = hero;

            Duration = _durationVar = duration;

            _skillEnabled = true;

            OnStartSkillEffect();
        }

        private void FixedUpdate()
        {
            if (!_skillEnabled) return;

            SkillUpdate();

            if (_durationVar > 0)
            {
                UpdateDuration();

                UpdateImages();
            }
            else
            {
                Finish();
            }
        }

        private void Finish()
        {
            OnFinishSkillEffect();

            Destroy(gameObject);
        }

        private void UpdateImages()
        {
            var percent = _durationVar / Duration;

            foreach (var image in _images)
                image.fillAmount = percent;
        }

        private void UpdateDuration()
        {
            _durationVar -= Time.deltaTime;
        }

        protected virtual void OnStartSkillEffect()
        {
            throw new NotImplementedException();
        }

        protected virtual void OnFinishSkillEffect()
        {
            throw new NotImplementedException();
        }

        protected virtual void SkillUpdate()
        {
        }
    }
}