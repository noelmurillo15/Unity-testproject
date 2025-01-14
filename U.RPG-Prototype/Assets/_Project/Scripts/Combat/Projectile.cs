﻿/*
 * Projectile - 
 * Created by : Allan N. Murillo
 * Last Edited : 2/25/2020
 */

using UnityEngine;
using ANM.Attributes;

namespace ANM.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed = 1f;
        [SerializeField] private bool isHoming = true;
        [SerializeField] private GameObject hitEffect = null;
        [SerializeField] private float maxLifeTime = 10f;
        [SerializeField] private GameObject[] destroyOnHit = null;
        [SerializeField] private float lifeAfterImpact = 2f;

        private Health _target;
        private GameObject _instigator;
        private float _totalDamage;


        private void Start()
        {
            transform.LookAt(GetAimLocation());
        }

        private void Update()
        {
            if (_target == null) return;
            if (isHoming && !_target.IsDead()) transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() != _target) return;
            if (_target.IsDead()) return;

            _target.TakeDamage(_instigator, _totalDamage);
            speed = 0f;

            if (hitEffect != null)
            {
                Instantiate(hitEffect, GetAimLocation(), transform.rotation);
            }

            foreach (var toDestroy in destroyOnHit)
            {
                Destroy(toDestroy);
            }

            Destroy(gameObject, lifeAfterImpact);
        }

        public void SetTarget(Health target, GameObject instigator, float damage)
        {
            _target = target;
            _totalDamage = damage;
            _instigator = instigator;
            Destroy(gameObject, maxLifeTime);
        }   //  Damage is calculated from weapon Scriptable dmg + character base damage

        private Vector3 GetAimLocation()
        {
            var targetCapsule = _target.GetComponent<CapsuleCollider>();
            if (targetCapsule == null) { return _target.transform.position; }
            return _target.transform.position + Vector3.up * (targetCapsule.height * 0.5f);
        }
    }
}