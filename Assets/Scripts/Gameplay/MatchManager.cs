using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Gameplay {
    public class MatchManager : MonoBehaviour
    {

        [SerializeField] private List<Transform> mySpawnPoints;
        public static int NUMBER_OF_PLAYERS = 2;
        public Dictionary<int, GameObject> playerPrefabs = new Dictionary<int, GameObject>();

        // A list containing all of the players in game
        private List<PlayerHandler> allPlayers = new List<PlayerHandler>();

        private void Start()
        {
            SpawnPlayers();
        }

        private void SpawnPlayers()
        {
            for (int f = 1; f <= NUMBER_OF_PLAYERS; ++f)
            {
                // Get spawn position
                Vector2 spawnPointPos = mySpawnPoints[f - 1].position;
                var spawnPointPos3 = new Vector3(spawnPointPos.x, spawnPointPos.y, 1);

                // Instantiate the player
                GameObject playerInstance = Instantiate(playerPrefabs[f-1], spawnPointPos3, Quaternion.identity);

                // Initialize a player and give it its index
                var playerHandler = playerInstance
                    .GetComponent<PlayerHandler>();

                playerHandler.InitializeHandler(f);
                playerHandler.OnAttack += HandlePlayerAttack;

                // Append it to the list of players
                allPlayers.Add(playerHandler);
            }
        }

        private void HandlePlayerAttack(RaycastHit2D hit, Attack spec)
        {
            var target = hit.collider.gameObject;
            var targetHandler = target.GetComponent<PlayerHandler>();
            if(targetHandler == null) return;

            targetHandler.ApplyDamage(spec, hit.point);
        }
    }
}
