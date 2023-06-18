using UnityEngine;

public class AssistantPlayer : MonoBehaviour, IControllable
{
    public DefaultGun DefaultGun;
    [SerializeField] private PlayerDragging _playerDragging;

    private void Start()
    {
        _playerDragging.OnPlayerShootEvent += () => { PerformShoot(DefaultGun); };
    }

    public void PerformShoot(IWeapon weapon)
    {
        weapon.Shoot();
    }
}