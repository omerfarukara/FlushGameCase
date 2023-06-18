using FlushGameCase.Core;
using UnityEngine;

namespace FlushGameCase.Input
{
    public class MobileInput : MonoSingleton<MobileInput>
    {
        [SerializeField] private Joystick joystick;

        public Vector2 GetDirection()
        {
            return joystick.Direction;
        }

        public float GetHorizontal()
        {
            return joystick.Horizontal;
        }

        public float GetVertical()
        {
            return joystick.Vertical;
        }
    }
}
