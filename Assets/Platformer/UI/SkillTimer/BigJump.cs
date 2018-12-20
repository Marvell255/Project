namespace UI.SkillTimer
{
    public class BigJump : SkillTimer
    {
        protected override void OnStartSkillEffect()
        {
            Hero.JumpSpeed = 25;
        }

        protected override void OnFinishSkillEffect()
        {
            Hero.JumpSpeed = 16;
        }
    }
}