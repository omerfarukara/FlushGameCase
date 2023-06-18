using UnityEngine;

namespace FlushGameCase.Game.Interfaces
{
    public interface ICollectable
    {
        public GemInfo GetGemInfo { get; }
        public Vector3 GetPosition { get;}
        public float GetScale { get;}
        public bool IsCollectable { get; }
        public void CompleteTask();
    }
}
