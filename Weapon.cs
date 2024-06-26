using System;

namespace NapilnicTask1
{
    public abstract class Weapon
    {
        protected Weapon(Clip clip) 
        {
            Clip = clip ?? throw new ArgumentNullException(nameof(clip));
        }
        
        public Clip Clip { get; }

        public void Fire(Player player)
        {
            if (player == null) 
                throw new ArgumentNullException(nameof(player));
            
            if (Clip.CurrentBulletsCount <= 0) 
                return;
            
            Clip.RemoveBullet();
            player.TakeDamage(Clip.Bullet.Damage);
        }
    }

    public abstract class Clip
    {
        protected Clip(Bullet bullet, uint currentBulletsCount)
        {
            Bullet = bullet ?? throw new ArgumentNullException(nameof(bullet));

            if (currentBulletsCount == 0) 
                throw new ArgumentOutOfRangeException(nameof(currentBulletsCount));

            CurrentBulletsCount = currentBulletsCount;
        }

        public Bullet Bullet { get; }
        public uint CurrentBulletsCount { get; private set; }

        public void RemoveBullet()
        {
            if(CurrentBulletsCount > 0) 
                CurrentBulletsCount--;
        }
    }

    public abstract class Bullet
    {
        protected Bullet (uint damage)
        {
            if (damage <= 0) 
                throw new ArgumentOutOfRangeException(nameof(damage));
        
            Damage = damage;
        }

        public uint Damage { get; }
    }


    public abstract class Player
    {
        private uint _health;

        protected Player(uint health)
        {
            if (health <= 0) 
                throw new ArgumentOutOfRangeException(nameof(health));

            _health = health;
            IsDead = false;
        }

        private bool IsDead { get; set; }

        public void TakeDamage(uint damage)
        {
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

    public abstract class Bot
    {
        private readonly Weapon _weapon;

        protected Bot(Weapon weapon)
        {
            _weapon = weapon ?? throw new ArgumentNullException(nameof(weapon));
        }

        public void OnSeePlayer(Player player) 
        {
            if (player == null) 
                throw new ArgumentNullException(nameof(player));
            
            if(_weapon.Clip.CurrentBulletsCount > 0)
                _weapon.Fire(player);
        }
    }
}