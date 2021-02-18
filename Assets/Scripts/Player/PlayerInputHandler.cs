using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Input;
using Gameplay;
using UnityEngine.InputSystem;

namespace Player {
    public class PlayerInputHandler : MonoBehaviour
    {
        public UnityAction<PlayerInputPacket> OnMove;
        public UnityAction<PlayerInputPacket, AttackTypes> OnFire;

        private float myMinimumControllerAxis = 0.15f;

        private void Start()
        {
            // Polls controllers at 120hz explicitly instead of 60.
            InputSystem.pollingFrequency = 120;
        }


        public void Update()
        {

            for(var ma = 1; ma <= MatchManager.NUMBER_OF_PLAYERS; ++ma) {
                HandleMovementTick(ma);
                HandleAttackTick(ma);
            }
        }

        private void HandleAttackTick(int playerIndex)
        {
            bool isDefault = KeyboardOrControllerSelectionButton($"P{playerIndex}_Fire");
            bool isSpecial = KeyboardOrControllerSelectionButton($"P{playerIndex}_SpecialFire");
            
            int type = 
                isDefault ? (int)AttackTypes.DEFAULT :
                isSpecial ? (int)AttackTypes.SPECIAL :
                -1;
            if (type < 0) return;

            var packet = new PlayerInputPacket(
                PlayerActions.FIRE,
                playerIndex,
                new Vector2()
            );
            
            OnFire?.Invoke(packet, (AttackTypes) type);
        }

        private bool KeyboardOrControllerSelectionButton(string buttonName)
        {
            return GetButton($"CONTROLLER{buttonName}") || GetButton(buttonName);
        }

        private float KeyboardOrControllerSelectionAxis(string axisName)
        {
            var controller = GetAxisRaw($"CONTROLLER{axisName}");
            if (Mathf.Abs(controller) < myMinimumControllerAxis)
            {
                controller = 0f;
            }
            var keyboard = GetAxisRaw($"{axisName}");
            return controller == 0f ? keyboard : controller;
        }

        private void HandleMovementTick(int playerIndex) {
            // todo: investigate fucked movement, might just be my controller though.
            var horizontalValue = KeyboardOrControllerSelectionAxis($"P{playerIndex}_Horizontal");
            var verticalValue = KeyboardOrControllerSelectionAxis($"P{playerIndex}_Vertical");
            var verticalDownValue = GetAxisRaw($"CONTROLLERP{playerIndex}_VerticalDown");
            if (verticalDownValue < 0f)
            {
                // yeah sorry im just getting this done i dont care if its nested fuck you
                if (Mathf.Abs(verticalValue) < 0.5f)
                {
                    verticalValue = verticalDownValue;
                }
            }

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
