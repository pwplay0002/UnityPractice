using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon/NewWeapon")]
public class Weapon : ScriptableObject
{
    [SerializeField]
    private GameObject _source = null;

    [SerializeField]
    private GameObject _effect = null;

    [SerializeField, Range(10.0f, 50.0f)]
    private float _damage = 10.0f;
    public float Damage { get { return _damage; } }//외부에서 사용할 수 있도록

    [SerializeField, Range(1.0f, 10.0f)]
    private float _range = 2.0f;
    public float Range { get { return _range; } }

    [SerializeField]
    private bool _bRightHanded = true;

    [SerializeField]
    private AnimatorOverrideController _override = null;

    [SerializeField]
    private Projectile _projectile = null;
    public bool HasProjectile { get { return _projectile != null; } }

    [SerializeField, Range(1.0f, 20.0f)]
    private float _speed = 8.0f;

    [SerializeField, Range(1.0f, 5.0f)]
    private float _lifeTime = 3.0f;
    public float LifeTime { set { _lifeTime = value; } }

    [SerializeField]
    private bool _bHoming = false;


    const string _weaponName = "Weapon";

    public void Spawn(Transform right, Transform left, Animator animator)
    {
        DestroyWeapon(right, left);
        if(_source != null)
        {
            Transform hand = _bRightHanded ? right : left;
            GameObject weapon = Instantiate(_source, hand);

            weapon.name = _weaponName;
        }
        if (_override != null && animator != null)
            animator.runtimeAnimatorController = _override;
    }

    private void DestroyWeapon(Transform right, Transform left)
    {
        Transform old = right.Find(_weaponName);
        if (old == null)
            old = left.Find(_weaponName);

        if (old == null) return;

        old.name = "DestroyWeapon";
        Destroy(old.gameObject);
    }

    public void LaunchProjectile(Transform right, Transform left, Damage damage)
    {
        Transform hand = null;
        hand = _bRightHanded ? left : right;

        Projectile projectile = Instantiate(_projectile, hand.position, Quaternion.identity);
        projectile.SetTarget(damage, _damage);
        projectile.Speed = _speed;
        projectile.LifeTime = _lifeTime;
        projectile.Homing = _bHoming;
        projectile.Effect = _effect;
    }

}
