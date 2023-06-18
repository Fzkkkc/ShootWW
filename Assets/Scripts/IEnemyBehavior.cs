using System;

public interface IEnemyBehavior
{
    int Reward { get; }
    
    int Health { get; }
    
    void EnemyMoveForward(); 
    
    event Action<IEnemyBehavior> OnEnemyKilledEvent;
    event Action<IEnemyBehavior> OnEnemyReachedEndEvent;
    void TakeDamage(int damage);
}