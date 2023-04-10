using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ebac.Boss
{
    public class TriggerBoss : MonoBehaviour
    {
        [SerializeField] private GameObject objectBoss;
        [SerializeField] private BossBase boss;
        public string tagToCompare = "Player";

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(tagToCompare))
            {
                objectBoss.SetActive(true);
                StartCoroutine(InitBossCoroutine());
            }
        }

        IEnumerator InitBossCoroutine()
        {
            boss.SwitchState(BossAction.INIT);
            yield return new WaitForSeconds(2f);
            boss.SwitchState(BossAction.WALK);
        }
    }
}
