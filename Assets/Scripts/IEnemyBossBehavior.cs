using System;

public interface IEnemyBossBehavior
{
    event Action<IEnemyBossBehavior> OnEnemyBossKilledEvent;     
}