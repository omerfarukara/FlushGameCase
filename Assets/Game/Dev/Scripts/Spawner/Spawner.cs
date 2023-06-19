using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FlushGameCase.Game
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private SpawnObject prefab;
        [SerializeField] private Vector2Int gridSize;
        [SerializeField] private Vector2 spawnTimeRange;
        
        private readonly Queue<SpawnObject> _spawnObjects = new Queue<SpawnObject>();

        private GridNode[] _gridNodes;
        private Transform _transform;
        private float _spawnTime;

        private void Awake()
        {
            _transform = GetComponent<Transform>(); // for optimization ***
            SetGridNodes();
        }
        
        private void Start()
        {
            _spawnTime = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
        }

        private void Update()
        {
            _spawnTime -= Time.deltaTime;

            if (!(_spawnTime <= 0)) return;
            
            _spawnTime = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
            Spawn();
        }

        private void Spawn()
        {
            if (!GetNewSpawnPosition(out Vector3 position)) return;
            
            if (_spawnObjects.Count == 0)
            {
                SpawnObject newSpawnObject = Instantiate(prefab, transform);
                _spawnObjects.Enqueue(newSpawnObject);
            }
                
            SpawnObject spawnObject = _spawnObjects.Dequeue();
            spawnObject.Initiate(position, ReturnToQueue);
        }

        private void ReturnToQueue(SpawnObject spawnObject)
        {
            ResetGrid(spawnObject.transform.position);
            _spawnObjects.Enqueue(spawnObject);
        }
        
        private void SetGridNodes()
        {
            _gridNodes = new GridNode[gridSize.x * gridSize.y];
            
            Vector3 position = _transform.position;
            float x = position.x - gridSize.x/2f + 0.5f;
            float z = position.z - gridSize.y/2f + 0.5f;
            Vector3 startPosition = new Vector3(x, 0, z);
            
            int index = 0;
            
            for (int i = 0; i < gridSize.x; i++)
            {
                for (int j = 0; j < gridSize.y; j++)
                {
                    _gridNodes[index] = new GridNode
                    {
                        Position = startPosition + new Vector3(i, 0, j)
                    };
                    index++;
                }
            }
        }
        
        private void ResetGrid(Vector3 position)
        {
            _gridNodes.First(m => m.Position == position).IsEmpty = true;
        }
        
        private bool GetNewSpawnPosition(out Vector3 position)
        {
            GridNode[] emptyNodes = _gridNodes.Where(m => m.IsEmpty).ToArray();
            if (emptyNodes.Length == 0)
            {
                position = Vector3.zero;
                return false;
            }
            GridNode gridNode = emptyNodes[Random.Range(0, emptyNodes.Length)];
            
            gridNode.IsEmpty = false;
            position = gridNode.Position;
            return true;
        }
        
        // Editor
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0.71f, 1f, 0f, 0.59f);
            for (int i = 0; i < gridSize.x; i++)
            {
                for (int j = 0; j < gridSize.y; j++)
                {
                    Vector3 position = transform.position - Vector3.right * (gridSize.x / 2f- 0.5f) - Vector3.forward * (gridSize.y / 2f -0.5f);
                    float x = position.x + i;
                    float z = position.z + j;
                    Vector3 center = new Vector3(x, 0, z);
                    Gizmos.DrawWireCube(center , new Vector3(0.95f, 0.001f, 0.95f));
                }
            }
        }
    }
}
