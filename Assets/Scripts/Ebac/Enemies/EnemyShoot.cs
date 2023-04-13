using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ebac.Enemy
{
    public class EnemyShoot : EnemyBase
    {
        public GunBase gunBase;

        protected override void Init()
        {
            base.Init();

            // Esta tendo Shake na camera do jogador.
            gunBase.StartShoot();
        }
    }
}
