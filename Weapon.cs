namespace Napilnic
{
    public class Weapon
    {
        private readonly Clip _clip;
        private readonly Player _player;

        public Weapon(Clip clip, Player player)
        {
            _clip = clip;
            _player = player;
        }
        private void Fire(Player player)
        {
            if(_clip.CurrentBulletsCount > 0)
            {
                _clip.RemoveBullet();
                _player.TakeDamage(_clip.Bullet.Damage)
            }
            else
            {
                throw new InvalidOperatorException("Недостаточно патронов. Выполните перезарядку");
            }
        }
    }

    public class Clip
    {
        public Bullet Bullet {get; private set;};
        public uint CurrentBulletsCount {get; private set; } 
        public uint MaxBulletsCount {get; private set; }

        public Clip(Bullet bullet, uint maxBulletsCount, uint currentBulletsCount)
        {
            Bullet = bullet; 
            MaxBulletsCount = maxBulletsCount;
            CurrentBulletsCount = currentBulletsCount;
        }

        public void RemoveBullet()
        {
            if(CurrentBulletsCount > 0)
            {
                CurrentBulletsCount--;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

    }

    public class Bullet
    {
        public uint Damage {get; private set; }
    }


    public class Player
    {
        public uint Health {get; private set}
        public uint isDead {get; private set}

        public void TakeDamage(uint damage)
        {
            if(damage <= Health)
            {
                Health -= damage;
            }
            else
            {
                Die();
            }
        }

        private void Die()
        {
            Health = 0;
            isDead = true;
        }
    }

    public class Bot
    {
        public Weapon Weapon {get; private set}

        public Bot(Weapon weapon)
        {
            Weapon = weapon;
        }

        private void OnSeePlayer(Player player)
        {
            Weapon.Fire(player);
        }
    }
}
    