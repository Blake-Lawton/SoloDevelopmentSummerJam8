using _game.Scripts.Controllers;
using UnityEngine;

namespace _game.Scripts.Weapons
{
    public abstract class WeaponBase : MonoBehaviour
    {
        public abstract void Tick();
        public abstract void Equipped(PlayerBrain brain);

        public abstract void ResetCoolDown();
    }
}
