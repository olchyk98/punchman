using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.Materials
{
    /// <summary>
    /// This whole shit show is a bad excuse for the fact that you cant set field values per script, in built games. Only in Edit Mode.
    /// If you need any clarification on this pls just message me, ok ? 
    /// </summary>
    public class PrefabManager : MonoBehaviour
    {

        /// <summary>
        /// Meant to be used in-order to store prefabs that should:
        /// 1. Always be loaded so they can be quickly accessed.
        /// 2. and this is key CAN'T BE APPLIED IN EDITOR.
        /// </summary>
        [SerializeField] private List<MetaPrefab> myPrefabs;
    
        public static PrefabManager Main;
        private void Awake()
        {
            if(Main != null && Main != this) {
                DestroyImmediate(gameObject);
                return;
            }
            Main = this;
            DontDestroyOnLoad(gameObject);
        }
        
        /// <summary>
        /// Queries the list of materials available and finds the one with a matching name
        /// </summary>
        /// <param name="aName">The name to search for</param>
        /// <returns>The material matching the given name, if there is one. Otherwise an exception is thrown.</returns>
        public GameObject GetPrefabByName(string aName)
        {
            return myPrefabs.First(m => m.myName == aName).myObject;
        }
    
    }
}
