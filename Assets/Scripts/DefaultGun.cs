using UnityEngine;

public class DefaultGun : MonoBehaviour, IWeapon
{
    public int BulletPoints { get; set; }
    
    [SerializeField] private GameObject _bulletPrefab;

    [SerializeField] private Transform[] _shootPositions;
    
    private float _cooldownBetweenShots = 0.15f;
    private float _timerFromLastBullet = 0f;

    private void Update()
    {
        _timerFromLastBullet += Time.deltaTime;
    }

    public void Start()
    {
        BulletPoints = 1; 
    }
    
    public void Shoot()
    {
        if (_timerFromLastBullet >= _cooldownBetweenShots)
        {
            _timerFromLastBullet = 0f;
            
            int shootPointCount = Mathf.Min(BulletPoints, _shootPositions.Length);

            for (int i = 0; i < shootPointCount; i++)
            {
                Transform shootPosition = _shootPositions[i];
                GameObject bullet = Instantiate(_bulletPrefab, shootPosition.position, shootPosition.rotation);
                bullet.transform.Translate(0, 0, 1f);
            }
        }
    }
}