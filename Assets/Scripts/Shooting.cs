using UnityEngine;

namespace DefaultNamespace
{
    public class Shooting : MonoBehaviour
    {
        private Transform _gun;
        private float _lastShootTime;
        public GameObject pfBullet;
        public GameObject muzzlePrefab;
        public float shootingCooldown = .25f;
        public float bulletSpeed = 30f;
        
        private void Awake()
        {
            _gun = transform.Find("Gun");
            _lastShootTime = 0;
        }
        
        private void Update()
        {
            Shoot();
        }

        private void Shoot()
        {
            if (!Input.GetButton("Jump"))
                return;
        
            if(_lastShootTime + shootingCooldown > Time.time)
                return;
        
            _lastShootTime = Time.time;
            ShootABullet();
        }

        private void ShootABullet()
        {
            var muzzleVfx = Instantiate(muzzlePrefab, _gun.transform.position, Quaternion.identity);
            var psMuzzle = muzzleVfx.GetComponent<ParticleSystem>();
            if (psMuzzle != null)
            {
                Destroy(muzzleVfx, psMuzzle.main.duration);
            }
            else
            {
                var psChild = muzzleVfx.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(muzzleVfx, psChild.main.duration);
            }
            
            var bullet = Instantiate(pfBullet, _gun.transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().Setup(Vector2.up, bulletSpeed);
        }
    }
}