using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter
{
    public abstract class Weapon : MonoBehaviour
    {
        public abstract void Shoot();
        [SerializeField]
        private GameObject shotVfx;
        [SerializeField]
        private GameObject bulletGO;
        [SerializeField]
        private int Damage;
    }
}