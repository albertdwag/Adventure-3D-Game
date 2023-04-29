using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

namespace Items
{
    public class ItemCollactableBase : MonoBehaviour
    {
        public SFXType sfxType;
        public ItemType itemType;

        public string compareTag = "Player";
        public ParticleSystem particles;
        public float timeToHide = 3f;
        public GameObject graphicItem;

        public Collider hitBox;

        [Header("Sounds")]
        public AudioSource audioSource;

        private void Awake()
        {
            //if (particles != null) particles.transform.SetParent(null);
        }

        protected void OnTriggerEnter(Collider collision)
        {
            if (collision.transform.CompareTag(compareTag))
                Collect();

            //gameObject.GetComponent<Collider>().enabled = false;
        }

        private void PlaySFX()
        {
            SFXPool.Instance.Play(sfxType);
        }

        protected virtual void HideItens()
        {
            if (graphicItem != null) graphicItem.SetActive(false);
            Invoke("HideObject", timeToHide);
        }

        protected virtual void Collect()
        {
            PlaySFX();
            if (hitBox != null) hitBox.enabled = false;
            HideItens();
            OnCollect();
        }

        private void HideObject()
        {
            gameObject.SetActive(false);
        }

        protected virtual void OnCollect()
        {
            if (particles != null) particles.Play();
            if (audioSource != null) audioSource.Play();
            ItemManager.Instance.AddByType(itemType);
        }
    }
}
