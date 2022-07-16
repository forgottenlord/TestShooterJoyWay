using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TestShooter
{
    public class PlayerController : MonoBehaviour
    {
        public float minimumY = -60F;
        public float maximumY = 60F;
        float rotationX = 0F;
        float rotationY = 0F;
        public float sensitivityX = 1F;
        public float sensitivityY = 5F;
        private Transform CameraGO;
        [SerializeField]
        private WeaponCarrier LeftHand;
        [SerializeField]
        private WeaponCarrier RightHand;
        [SerializeField]
        private PlayerInput _input;
        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            CameraGO = transform.GetChild(0).transform;
                        
            /*foreach (var a in _input.actions.actionMaps)
                Debug.Log(a.name);*/
        }
        private void Update()
        {
            TakeDrop(KeyCode.Q, LeftHand);
            TakeDrop(KeyCode.E, RightHand);
            ShootWeapon(0, LeftHand);
            ShootWeapon(2, RightHand);
            transform.position += WASD() * Time.deltaTime;
            MouseLook();
        }
        /*private void OnEnable()
        {
            _input.ActivateInput();

        }
        private void OnDisable()
        {
            _input.DeactivateInput();
        }*/
        private Vector3 WASD()
        {
            Vector3 delta = new Vector3();
            delta += Input.GetKey(KeyCode.W) ? transform.forward : new Vector3();
            delta -= Input.GetKey(KeyCode.S) ? transform.forward : new Vector3();
            delta -= Input.GetKey(KeyCode.A) ? transform.right : new Vector3();
            delta += Input.GetKey(KeyCode.D) ? transform.right : new Vector3();
            return delta;
        }
        private void MouseLook()
        {
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
            transform.eulerAngles = new Vector3(0, rotationX, 0);
            CameraGO.localEulerAngles = new Vector3(rotationY, 0, 0);
        }
        private void ShootWeapon(int mouseButtonNum, WeaponCarrier weaponCarrier)
        {
            if (!Input.GetMouseButtonDown(mouseButtonNum)) return;
            if (!weaponCarrier.IsEmpty()) weaponCarrier.ShootWeapon();
        }
        private void TakeDrop(KeyCode keyCode, WeaponCarrier weaponCarrier)
        {
            if (!Input.GetKeyDown(keyCode)) return;
            if (weaponCarrier.IsEmpty())
            {
                //RaycastHit[] hits = Physics.SphereCastAll(weaponCarrier.transform.position, 2f, weaponCarrier.transform.forward);
                RaycastHit[] hits = Physics.RaycastAll(CameraGO.transform.position, CameraGO.transform.forward, 3f);
                Weapon weapon = null;
                foreach (var hit in hits)
                {
                    weapon = hit.transform.GetComponent<Weapon>();
                    if (weapon != null)
                    {
                        weaponCarrier.Take(weapon);
                        break;
                    }
                }
            }
            else
            {
                weaponCarrier.Drop();
            }
        }
    }
}