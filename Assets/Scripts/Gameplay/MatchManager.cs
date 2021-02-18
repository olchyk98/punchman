using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using UI;
using UnityEngine.SceneManagement;

namespace Gameplay {
    public class MatchManager : MonoBehaviour
    {
        [SerializeField] private GameObject myHud;
        [SerializeField] private List<Transform> mySpawnPoints;
        public static int NUMBER_OF_PLAYERS = 2;
        public Dictionary<int, GameObject> playerPrefabs = new Dictionary<int, GameObject>();

        // A list containing all of the players in game
        private List<PlayerHandler> allPlayers = new List<PlayerHandler>();
        private HUDManager myHudManager;
        
        
        private void Start()
        {
            playerPrefabs = GameManager.characters;
            SpawnPlayers();
            myHudManager = myHud.GetComponent<HUDManager>();
            myHudManager.SetCharacters(playerPrefabs[0], playerPrefabs[1]);
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
                playerHandler.OnKnockbackUpdate += RequestToRerenderStatHud;
                playerHandler.OnGameOver += HandleGameOver;
                

                // Append it to the list of players
                allPlayers.Add(playerHandler);
            }
        }

        private void HandlePlayerAttack(RaycastHit2D hit, Vector2 direction, Attack spec)
        {
            var target = hit.collider.gameObject;
            var targetHandler = target.GetComponent<PlayerHandler>();
            if(targetHandler == null) return;

            targetHandler.ApplyDamage(spec, direction, hit.point);
        }

        private void RequestToRerenderStatHud(int playerIndex, float newKnockback)
        {
            myHudManager.UpdateStat(playerIndex, $"{newKnockback}%");
        }

        private void HandleGameOver(int playerIndex)
        {
            StartCoroutine(LoadLevelAfterDelay(6));
        }

        IEnumerator LoadLevelAfterDelay(float time)
        {
            yield return new WaitForSeconds(time);
            SceneManager.LoadScene("Main Menu");
        }
    }
}
