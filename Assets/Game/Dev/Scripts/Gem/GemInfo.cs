using Sirenix.OdinInspector;
using UnityEngine;

namespace FlushGameCase.Game
{
    [System.Serializable]
    public class GemInfo
    {
        [SerializeField] [ReadOnly] private int id;
        [SerializeField] private string name;
        [SerializeField] private int price;
        [SerializeField] private Sprite icon;
        [SerializeField] private Mesh mesh;
        [SerializeField] private Material material;

        public int Id
        {
            get => id;
            set => id = value;
        }
        public string Name => name;
        public int Price => price;
        public Sprite Icon => icon;
        public Mesh Mesh => mesh;
        public Material Material => material;
    }
}
