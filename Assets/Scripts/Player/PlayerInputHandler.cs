using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Input;
using Gameplay;

namespace Player {
    public class PlayerInputHandler : MonoBehaviour
    {
        public UnityAction<PlayerInputPacket> OnMove;
        public UnityAction<PlayerInputPacket> OnFire;

        public void Update()
        {
            for(var ma = 1; ma <= MatchManager.NUMBER_OF_PLAYERS; ++ma) {
                HandleMovementTick(ma);
                HandleAttackTick(ma);
            }
        }

        private void HandleAttackTick(int playerIndex)
        {
            if (!GetButton($"P{playerIndex}_Fire")) return;

            var packet = new PlayerInputPacket(
                PlayerActions.FIRE,
                playerIndex,
                new Vector2()
            );

            OnFire?.Invoke(packet);
        }

        private void HandleMovementTick(int playerIndex) {
            var horizontalValue = GetAxisRaw($"P{playerIndex}_Horizontal");
            var verticalValue = GetAxisRaw($"P{playerIndex}_Vertical");

            if(horizontalValue == 0f && verticalValue == 0f) return;

            Vector2 direction = new Vector2(
                horizontalValue,
                verticalValue
            );

            var packet = new PlayerInputPacket(
                PlayerActions.MOVEMENT,
                playerIndex,
                direction
            );
            OnMove?.Invoke(packet);
        }
    }
}
