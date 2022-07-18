using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TestShooter
{
    public class WeaponCarrier : MonoBehaviour
    {
        public Weapon weapon;
        public bool IsEmpty()
        {
            return weapon == null;
        }
        public void Take(Weapon _weapon)
        {
            weapon = _weapon;
            weapon.transform.SetParent(transform);
            weapon.transform.localPosition = new Vector3();
            weapon.transform.localEulerAngles = new Vector3();
            //StartCoroutine(MagnitWeapon(_weapon));
        }
        private IEnumerator MagnitWeapon(Weapon _weapon)
        {
            Vector3 diff = _weapon.transform.position - transform.position;
            while (diff.magnitude > .3f)
            {
                diff = _weapon.transform.position - transform.position;
                _weapon.transform.position -= diff * Time.fixedDeltaTime;
                yield return new WaitForSeconds(Time.fixedDeltaTime);
            }
            weapon.transform.SetParent(transform);
            weapon.transform.localPosition = new Vector3();
            weapon.transform.localEulerAngles = new Vector3();
        }

        public void Drop()
        {
            weapon.transform.SetParent(null);
            weapon = null;
        }
        public void ShootWeapon()
        {
            weapon.Shoot();
        }
    }
}
