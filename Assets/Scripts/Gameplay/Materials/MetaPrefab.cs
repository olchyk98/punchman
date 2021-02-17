using UnityEngine;

namespace Gameplay.Materials {

    [System.Serializable]
    public struct MetaPrefab
    {
        [SerializeField] public string myName;
        [SerializeField] public GameObject myObject;
    }
}