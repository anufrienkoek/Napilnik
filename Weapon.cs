namespace Napilnik
{
    public abstract class Weapon
    {
        private readonly Clip _clip;
        private readonly Player _player;

        protected Weapon(Clip clip, Player player)
        {
            _clip = clip;
            _player = player;
        }

        public void Fire(Player player)
        {
            if (_clip.CurrentBulletsCount > 0)
            {
                _clip.RemoveBullet();
                _player.TakeDamage(_clip.Bullet.Damage);
            }
            else
            {
                throw new InvalidOperationException("Недостаточно патронов. Выполните перезарядку");
            }
        }
    }

    public abstract class Clip
    {
        public Bullet Bullet { get; private set; }
        public uint CurrentBulletsCount { get; private set; }
        public uint MaxBulletsCount { get; private set; }

        protected Clip(Bullet bullet, uint maxBulletsCount, uint currentBulletsCount)
        {
            Bullet = bullet;
            MaxBulletsCount = maxBulletsCount;
            CurrentBulletsCount = currentBulletsCount;
        }

        public void RemoveBullet()
        {
            if (CurrentBulletsCount > 0)
            {
                CurrentBulletsCount--;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }

    public abstract class Bullet
    {
        public uint Damage { get; private set; }
    }


    public abstract class Player
    {
        public bool IsDead { get; private set; }
        private uint _health;

        public void TakeDamage(uint damage)
        {
            if (damage <= _health)
            {
                _health -= damage;
            }
            else
            {
                Die();
            }
        }

        private void Die()
        {
            _health = 0;
            IsDead = true;
        }
    }

    public class Bot
    {
        private Weapon Weapon { get; set; }

        public Bot(Weapon weapon)
        {
            Weapon = weapon;
        }

        public void OnSeePlayer(Player player)
        {
            Weapon.Fire(player);
        }
    }
}