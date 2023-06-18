using System;
using UnityEngine;

namespace FlushGameCase.Game
{
    public abstract class SpawnObject : MonoBehaviour
    {
        protected Action<SpawnObject> completedAction;

        public abstract void Initiate(Vector3 position, Action<SpawnObject> callback);

        public abstract void CompleteTask();
    }
}
