using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestShooter
{
    public class Target : MonoBehaviour
    {
        [SerializeField] private int health;
        public void SetDamage(int damage)
        {
            health -= damage;
            Debug.Log(health);
        }
    }
}