using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _01Scripts.Players
{
    [CreateAssetMenu(fileName = "PlayerInputSO", menuName = "SO/PlayerInputSO")]
    public class PlayerInputSO : ScriptableObject, Controls.IPlayerActions
    {
        [SerializeField] private LayerMask whatIsGround;
        // public event Action<Vector2> OnMovementChange;
        public event Action OnAttackPressed;
        
        public Vector2 MovementKey { get; private set; }
        public Vector2 LookKey { get; private set; }
        
        private Controls _controls;
        private Vector2 _screenPosition; // 마우스 좌표
        private Vector3 _worldPosition;

        private void OnEnable()
        {
            if(_controls == null)
            {
                _controls = new Controls();
                _controls.Player.SetCallbacks(this);
            }
            _controls.Enable();
            
        }

        private void OnDisable()
        {
            _controls.Player.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MovementKey = context.ReadValue<Vector2>();
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            LookKey = context.ReadValue<Vector2>();
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnAttackPressed?.Invoke();
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            
        }


        public Vector3 GetWorldPosition()
        {
            Camera mainCam = Camera.main;
            Debug.Assert(mainCam != null,"No main camera");
            Ray camRay = mainCam.ScreenPointToRay(_screenPosition);
            if (Physics.Raycast(camRay, out RaycastHit hit, mainCam.farClipPlane, whatIsGround))
            {
                _worldPosition = hit.point;
            }
            return _worldPosition;
        }
    }
}
