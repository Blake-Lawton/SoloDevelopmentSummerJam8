using _game.Scripts.Data.WeaponData;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileData", menuName = "Scriptable Objects/Projectile Data")]
public class ProjectileData : WeaponData
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifetime;
    public float Speed => _speed;
    public float Lifetime => _lifetime;
}