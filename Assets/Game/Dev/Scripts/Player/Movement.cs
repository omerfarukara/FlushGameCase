using System;
using FlushGameCase.Core;
using FlushGameCase.Input;
using UnityEngine;

namespace FlushGameCase.Game.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class Movement : MonoBehaviour
    {
        private Transform _transform;
        private Rigidbody _rigidbody;

        private float _moveSpeed;
        private float _horizontal;
        private float _vertical;
        private Vector3 _direction;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _transform = GetComponent<Transform>(); // For optimization ***
        }

        private void Start()
        {
            _moveSpeed = DataManager.Instance.GameData.MoveSpeed;
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = new Vector3(_horizontal, 0, _vertical) * _moveSpeed;
        }

        private void Update()
        {
            _horizontal = MobileInput.Instance.GetHorizontal();
            _vertical = MobileInput.Instance.GetVertical();
            _direction = new Vector3(_horizontal, 0, _vertical).normalized;
            
            if (_direction.magnitude < 0.1f) return;

            _transform.forward = _direction;
        }
    }
}
