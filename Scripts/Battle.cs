namespace WME
{
    public class Battle
    {
        public static Battle Instance;
        public Fighter Player;
        public Fighter Enemy;

        public Battle(Fighter player, Fighter enemy)
        {
            Instance = this;
            this.Player = player;
            this.Enemy = enemy;
        }
    }
}