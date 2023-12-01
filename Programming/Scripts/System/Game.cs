using KinematicCharacterController.Walkthrough.ClimbingLadders;
using SurockGames.Input;
using UnityEngine;

namespace SurockGames.System
{
    public class Game : MonoBehaviour
    {
        [Header ("Player Settings")]
        [SerializeField] private Player playerPrefab;
        [SerializeField] private Transform teseusSpawn;
        [SerializeField] private Transform minotaurSpawn;
        [SerializeField] private Transform talosSpawn;

        [Header("Camera Settings")]
        [SerializeField] private GameObject cameraPrefab;

        public static UnityInputSystemService InputService;
        private Player player;
        
        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            player = Instantiate(playerPrefab, transform);
            player.InitCharacters(teseusSpawn, minotaurSpawn, talosSpawn);

            cameraPrefab = Instantiate(cameraPrefab, transform);

            InputService = new UnityInputSystemService();
        }
    }
}