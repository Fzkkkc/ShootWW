using UnityEngine;

public class FX : MonoBehaviour
{
    [SerializeField] private ParticleSystem _enemyDeathFX;
    public static FX Instance;

    private void Awake() =>
        Instance = this;
    
    public void EnemyDeathFX(Vector3 position)
    {
        _enemyDeathFX.transform.position = position;
        _enemyDeathFX.Play();
    }
}