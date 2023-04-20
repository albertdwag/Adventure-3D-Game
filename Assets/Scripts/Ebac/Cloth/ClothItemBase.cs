using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cloth
{
    public class ClothItemBase : MonoBehaviour
    {
        public ClothType clothType;
        public string compareTag = "Player";

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.CompareTag(compareTag))
                Collect();
        }

        public virtual void Collect()
        {
            Debug.Log("Collected");
            HideObject();
        }

        public void HideObject()
        {
            gameObject.SetActive(false);
        }
    }
}
