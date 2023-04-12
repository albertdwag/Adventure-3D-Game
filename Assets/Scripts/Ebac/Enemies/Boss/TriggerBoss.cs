using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ebac.Boss
{
    public class TriggerBoss : MonoBehaviour
    {
        public string tagToCompare = "Player";
        public GameObject bossCamera;
        public Color gizmoColor = Color.yellow;

        [SerializeField] private GameObject objectBoss;
        [SerializeField] private BossBase boss;

        private void Awake()
        {
            bossCamera.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(tagToCompare))
            {
                TurnCameraOn();
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

        private void TurnCameraOn()
        {
            bossCamera.SetActive(true);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawCube(transform.position, transform.localScale);
        }
    }
}
