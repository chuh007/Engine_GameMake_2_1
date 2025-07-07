using System;
using Chuh007Lib.Dependencies;
using UnityEngine;

namespace Chuh007Lib.ObjectPool.RunTime
{
    [Provide]
    public class PoolManagerMono : MonoBehaviour, IDependencyProvider
    {
        [SerializeField] private PoolManagerSO _poolManager;

        private void Awake() {
            _poolManager.Initialize(transform);
        }
    }
}