using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cloth;

namespace Cloth
{
    public class ClothChanger : MonoBehaviour
    {
        public SkinnedMeshRenderer mesh;
        public Texture2D texture;
        private Texture2D _defaultTexture;
        public string shaderIdName = "_EmissionMap";

        private void Awake()
        {
            _defaultTexture = (Texture2D)mesh.sharedMaterials[0].GetTexture(shaderIdName);
            ChangeTexture();
        }

        [NaughtyAttributes.Button]
        private void ChangeTexture()
        {
            mesh.sharedMaterials[0].SetTexture(shaderIdName, texture);
        }

        public void ChangeTexture(ClothSetup setup)
        {
            mesh.sharedMaterials[0].SetTexture(shaderIdName, setup.texture);
        }

        public void ResetTexutre()
        {
            mesh.sharedMaterials[0].SetTexture(shaderIdName, _defaultTexture);
        }
    }
}