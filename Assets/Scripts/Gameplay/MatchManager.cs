using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Gameplay {
    public class MatchManager : MonoBehaviour
    {

        [SerializeField] private GameObject myPlayerPrefab;
        [SerializeField] private List<Transform> mySpawnPoints;
        public static int NUMBER_OF_PLAYERS = 2;

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
                GameObject playerInstance = Instantiate(myPlayerPrefab, spawnPointPos3, Quaternion.identity);

                // Initialize a player and give it its index
                var playerHandler = playerInstance
                    .GetComponent<PlayerHandler>();

                playerHandler.InitializeHandler(f);
                playerHandler.OnAttack += HandlePlayerAttack;

                // Append it to the list of players
                allPlayers.Add(playerHandler);
            }
        }

        private void HandlePlayerAttack(RaycastHit2D hit, float damage)
        {
            var target = hit.collider.gameObject;
            var targetHandler = target.GetComponent<PlayerHandler>();
            if(targetHandler == null) return;

            targetHandler.ApplyDamage(damage, hit.point);
        }
    }
}
