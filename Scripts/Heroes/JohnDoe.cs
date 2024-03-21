namespace WME
{
    public class JohnDoe : BaseHero
    {
        public override string Name => "John Doe";

        public override string Description => "Sample Hero";

        public override string PortraitPath => "icon.svg";

        public override int HealthValue => 20;
    }
}