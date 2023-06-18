using UnityEngine;

public class PlayerController : MonoBehaviour, IControllable
{
    [HideInInspector] public Rigidbody MainPlayerRigidbody;
    
    private void Awake()
    {
        MainPlayerRigidbody = GetComponent<Rigidbody>();
    }

    public void PerformShoot(IWeapon weapon)
    {
        weapon.Shoot();
    }
}