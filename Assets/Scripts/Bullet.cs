using UnityEngine;

public class Bullet : MonoBehaviour
{ 
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float  _lifetime = 2f;
    
    private Rigidbody _bulletRigidbody;
    
    private void Start()
    {
        Destroy(gameObject, _lifetime);
        _bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        BulletShootBehavior();
    }

    private void BulletShootBehavior()
    {
        _bulletRigidbody.AddRelativeForce(transform.forward * _speed, ForceMode.VelocityChange);
    }

    private void OnTriggerEnter(Collider other)
    {
        IEnemyBehavior enemy = other.GetComponent<IEnemyBehavior>();
        if (enemy != null)
        {
            enemy.TakeDamage(1);
            FX.Instance.EnemyDeathFX(other.transform.position);
            Destroy(gameObject);
        }
    }
}
