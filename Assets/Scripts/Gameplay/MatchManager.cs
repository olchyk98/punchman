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
        private List<GameObject> allPlayers = new List<GameObject>();

        private void Start()
        {
            SpawnPlayers();
        }

        private void SpawnPlayers() {
            print(NUMBER_OF_PLAYERS);
            for (int f = 1; f <= NUMBER_OF_PLAYERS; ++f)
            {
                // Get spawn position
                Vector2 spawnPointPos = mySpawnPoints[f - 1].position;
                var spawnPointPos3 = new Vector3(spawnPointPos.x, spawnPointPos.y, 1);

                // Instantiate the player
                GameObject playerInstance = Instantiate(myPlayerPrefab, spawnPointPos3, Quaternion.identity);

                // Initialize a player and give it its index
                playerInstance
                    .GetComponent<PlayerHandler>()
                    .InitializeHandler(f);

                print(playerInstance);
                // Append it to the list of players
                allPlayers.Add(myPlayerPrefab);
            }
        }
    }
}
