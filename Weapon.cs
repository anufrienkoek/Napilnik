namespace NapilnicTask1
{
    public abstract class Weapon
    {
        private readonly Clip _clip;

        protected Weapon(Clip clip) 
        {
            _clip = clip ?? throw new ArgumentNullException(nameof(clip));
        }

        public void Fire(Player player)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));
            if (_clip.CurrentBulletsCount <= 0) return;
            
            _clip.RemoveBullet();
            player.TakeDamage(_clip.Bullet.Damage);
        }
    }

    public abstract class Clip
    {
        private readonly uint _maxBulletsCount;
        private uint _currentBulletsCount;

        protected Clip(Bullet bullet, uint maxBulletsCount, uint currentBulletsCount)
        {
            Bullet = bullet ?? throw new ArgumentNullException(nameof(bullet));
            if (maxBulletsCount <= 0) throw new ArgumentOutOfRangeException(nameof(maxBulletsCount));
            if (currentBulletsCount > maxBulletsCount) throw new ArgumentOutOfRangeException(nameof(currentBulletsCount));

            _maxBulletsCount = maxBulletsCount;
            _currentBulletsCount = currentBulletsCount;
        }

        public Bullet Bullet { get; }

        public void RemoveBullet()
        {
            if(_currentBulletsCount > 0) 
                _currentBulletsCount--;
        }
    }

    public abstract class Bullet
    {
        protected Bullet (uint damage)
        {
            if (damage <= 0) throw new ArgumentOutOfRangeException(nameof(damage));
        
            Damage = damage;
        }

        public uint Damage {get; }
    }


    public abstract class Player
    {
        private uint _health;

        protected Player(uint health)
        {
            if (health <= 0) throw new ArgumentOutOfRangeException(nameof(health));

            _health = health;
            IsDead = false;
        }

        public bool IsDead {get; private set; }

        public void TakeDamage(uint damage)
        {
            if (damage <= 0) throw new ArgumentOutOfRangeException(nameof(damage));

            if (damage < _health)
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
        private Weapon _weapon;

        public Bot(Weapon weapon)
        {
            _weapon = weapon;
        }

        public void OnSeePlayer(Player player) 
        {
            if (player == null) throw new ArgumentNullException(nameof(player));
            _weapon.Fire(player);
        }
    }
}