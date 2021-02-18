using UnityEngine;

namespace UI
{
    [System.Serializable]
    public struct UIHUDController
    {
        [SerializeField] public GameObject Name;
        [SerializeField] public GameObject Image;
        [SerializeField] public GameObject Stat;
    }
}