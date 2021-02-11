using UnityEngine;

namespace Player
{
    /// <summary>
    /// PlayerInputPacket contains the index of the player that the input should be applied to, as well as what action the player performed.
    /// In the case of a Packet with a enum of PlayerActions.MOVEMENT, the MovementDirection field will be defined.
    /// </summary>
    public class PlayerInputPacket
    {

        public PlayerActions Action { get; }
        public int PlayerIndex { get; }
        public Vector2 MovementDirection { get; }

        /// <summary>
        /// Takes a input manager callback and turns it into a PlayerInputPacket.
        /// </summary>
        /// <param name="action">The given action to perform based of user input</param>
        /// <param name="APlayerIndex">The index of the player that performed the action</param>
        public PlayerInputPacket(PlayerActions AnAction, int APlayerIndex, Vector2 ADirection)
        {
            Action = AnAction;
            PlayerIndex = APlayerIndex;
            MovementDirection = ADirection;
        }
    }
}
