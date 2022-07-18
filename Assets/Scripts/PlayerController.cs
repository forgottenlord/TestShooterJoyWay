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
        public InputActionReference triggerAction;
        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            CameraGO = transform.GetChild(0).transform;
            mouse = Mouse.current;
            keyboard = Keyboard.current;
        }
        Mouse mouse;
        Keyboard keyboard;
        private void Update()
        {
            MouseLook(Mouse.current.delta.ReadValue());
            transform.position += WASD() * Time.deltaTime;
            if (mouse.leftButton.wasPressedThisFrame) ShootWeapon(LeftHand);
            if (mouse.rightButton.wasPressedThisFrame) ShootWeapon(RightHand);
            if (keyboard.qKey.wasPressedThisFrame) TakeDrop(LeftHand);
            if (keyboard.eKey.wasPressedThisFrame) TakeDrop(RightHand);
        }
        private Vector3 WASD()
        {
            Vector3 delta = new Vector3();
            delta += keyboard.wKey.isPressed ? transform.forward : new Vector3();
            delta -= keyboard.sKey.isPressed ? transform.forward : new Vector3();
            delta -= keyboard.aKey.isPressed ? transform.right : new Vector3();
            delta += keyboard.dKey.isPressed ? transform.right : new Vector3();
            return delta;
        }
        private void MouseLook(Vector2 mouseNewPos)
        {
            rotationX += mouseNewPos.x;
            rotationY -= mouseNewPos.y;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
            transform.eulerAngles = new Vector3(0, rotationX, 0);
            CameraGO.localEulerAngles = new Vector3(rotationY, 0, 0);
            Debug.Log("mouseNewPos" + mouseNewPos);
        }
        private void ShootWeapon(WeaponCarrier weaponCarrier)
        {
            if (!weaponCarrier.IsEmpty()) weaponCarrier.ShootWeapon();
        }
        private void TakeDrop(WeaponCarrier weaponCarrier)
        {
            if (weaponCarrier.IsEmpty())
            {
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