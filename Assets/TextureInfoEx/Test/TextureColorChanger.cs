using UnityEngine;

namespace TextureInfoEx.Test
{
    public class TextureColorChanger : MonoBehaviour
    {

        public Material SelfMaterial;
        Color _startColor;

        void Start()
        {
            if (SelfMaterial == null)
            {
                Debug.LogError("No material assigned");
            }
            else
            {
                _startColor = SelfMaterial.color;
            }
        }

        void Update ()
        {
            SelfMaterial.color = _startColor;
            RaycastHit hit;
            if (!Physics.Raycast(transform.position, Vector3.down, out hit)) return;
            var rend = hit.collider.GetComponent<Renderer>();
            if (ReferenceEquals(rend, null)) return;
            var m = rend.sharedMaterial;
            if (ReferenceEquals(m, null)) return;
            TextureData data;
            if (m.mainTexture.GetUserData(out data))
            {
                SelfMaterial.color = data.AssociatedColor;
            }
        }

        void OnDestroy()
        {
            if (SelfMaterial != null)
            {
                SelfMaterial.color = _startColor;
            }
        }
    }
}
