using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _walkSpeed = 5;
        [SerializeField] private float _runSpeed = 8;
        [SerializeField] private float _rotateSpeed = 10;

        private InputSystem _input;
        private CharacterController _characterController;
        private Camera _playerCamera;

        private Vector3 _velocity;
        private Vector2 _rotation;
        private NativeArray<Vector2> _outputCamera;
        private NativeArray<Vector3> _outputVelocity;

        public void Init(InputSystem input)
        {
            _input = input;
            _characterController = GetComponent<CharacterController>();
            _playerCamera = GetComponentInChildren<Camera>();

            _outputCamera = new NativeArray<Vector2>(2, Allocator.Persistent); 
            _outputVelocity = new NativeArray<Vector3>(2, Allocator.Persistent);

            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            NativeArray<JobHandle> jobs = new NativeArray<JobHandle>(2, Allocator.Temp);

            _outputCamera[0] = _rotation;
            _outputVelocity[0] = _velocity;
            
            CameraRotateCalculation cameraRotateCalculation = new CameraRotateCalculation
            {
                Rotation = _outputCamera,
                DeltaTime = Time.deltaTime,
                MouseDelta = _input.Player.Look.ReadValue<Vector2>(),
                RotateSpeed = _rotateSpeed
            };
            VelocityCalculation velocityCalculation = new VelocityCalculation
            {
                Velocity = _outputVelocity,
                WalkSpeed = _walkSpeed,
                RunSpeed = _runSpeed,
                CameraAnglesY = _playerCamera.transform.localEulerAngles.y,
                Direction = _input.Player.Move.ReadValue<Vector2>(),
                IsSprint = _input.Player.Sprint.IsPressed()
            };

            jobs[0] = cameraRotateCalculation.Schedule();
            jobs[1] = velocityCalculation.Schedule();
            JobHandle.CompleteAll(jobs);

            _rotation = _outputCamera[1];
            _velocity = _outputVelocity[1];

            _playerCamera.transform.localEulerAngles = _rotation;
            _characterController.Move(_velocity * Time.deltaTime);
        }

        private void OnDestroy()
        {
            _input.Player.Disable();
            _outputCamera.Dispose();
            _outputVelocity.Dispose();
        }
        
        [BurstCompile]
        private struct CameraRotateCalculation : IJob
        {
            [ReadOnly] public float DeltaTime;
            [ReadOnly] public Vector2 MouseDelta;
            [ReadOnly] public float RotateSpeed;
            public NativeArray<Vector2> Rotation;

            public void Execute()
            {
                Vector2 rotation = Rotation[0];
                if (MouseDelta.sqrMagnitude < 0.1f) return;

                MouseDelta *= RotateSpeed * DeltaTime;
                rotation.y += MouseDelta.x;
                rotation.x = Mathf.Clamp(rotation.x - MouseDelta.y, -90, 90);
                Rotation[1] = rotation;
            }
        }
        
        [BurstCompile]
        private struct VelocityCalculation : IJob
        {
            [ReadOnly] public float WalkSpeed;
            [ReadOnly] public float RunSpeed;
            [ReadOnly] public float CameraAnglesY;
            [ReadOnly] public bool IsSprint;
            [ReadOnly] public Vector2 Direction;
            public NativeArray<Vector3> Velocity;
    
            public void Execute()
            {
                Vector3 velocity = Velocity[0];
                Direction *= IsSprint ? RunSpeed : WalkSpeed;
                Vector3 move = Quaternion.Euler(0, CameraAnglesY, 0) * new Vector3(Direction.x, 0, Direction.y);
                velocity = new Vector3(move.x, velocity.y, move.z);
                Velocity[1] = velocity;
            }
        }
    }
}