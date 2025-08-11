using System;
using _game.Scripts.Controllers.Enemy;

namespace _game.Scripts.Global
{
    public static class GlobalEvents 
    {
        public static event Action<EnemyBrain> OnEnemyDeath;
        public static event Action OnEndRound;
        public static event Action OnEnterChaos;

        public static void EnemyDied(EnemyBrain brain)
        {
            OnEnemyDeath?.Invoke(brain);
        }
        
        public static void EndRound()
        {
            OnEndRound?.Invoke();
        }

        public static void EnterChaos()
        {
            OnEnterChaos?.Invoke();
        }
    }
}
