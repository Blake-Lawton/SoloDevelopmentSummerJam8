using System.Collections;
using _game.Scripts.Controllers;
using _game.Scripts.Controllers.Enemy;
using _game.Scripts.Managers;
using _game.Scripts.Weapons.EnemyWeapons;
using UnityEngine;
using UnityEngine.Serialization;

public class MageController : EnemyAttackController
{
    [SerializeField] private Projectile _projectile;
    [SerializeField] private Transform _firePoint;
    private float _currentCoolDown;
        
    public override void AttackClimax()
    {
        var arrow = Instantiate(_projectile, _firePoint.position, _firePoint.rotation);
        arrow.SetUp(_stats.Damage);
        
        if (UpgradeManager.Instance.DoubleProjectile)
            StartCoroutine(SpawnExtraProjectile());
    }

    private IEnumerator SpawnExtraProjectile()
    {
        yield return new WaitForSeconds(0.8f);
        var arrow = Instantiate(_projectile, _firePoint.position, _firePoint.rotation);
        arrow.SetUp(_stats.Damage);
    }
        
    public override void Handle()
    {
        _currentCoolDown -= Time.deltaTime;
        if (Vector3.Distance(transform.position, PlayerBrain.Instance.transform.position) <= _stats.Range && _currentCoolDown <= 0)
        {
            if(_isAttacking)
                return;
            _isAttacking = true;
            _movement.IsMoving = false;
            _animator.Attack();
            _currentCoolDown = _stats.Cooldown;
        }
    }
}
