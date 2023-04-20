using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cloth
{
    public class ClothItemJump : ClothItemBase
    {
        public float jumpForce = 36f;

        public override void Collect()
        {
            base.Collect();
            Player.Instance.ChangeJumpForce(jumpForce, duration);
        }
    }
}
