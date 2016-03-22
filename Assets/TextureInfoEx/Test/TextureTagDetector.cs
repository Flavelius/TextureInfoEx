using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace TextureInfoEx.Test
{
    public class TextureTagDetector : MonoBehaviour
    {

        [Serializable]
        public class TextureTagChangeEvent: UnityEvent<TextureSurfaceType> { }

        public Vector3 RaycastDirection = Vector3.down;

        public float RaycastInterval = 0.1f;

        public bool ShowRayDirection;

        public TextureTagChangeEvent OnTagChanged;

        void Start()
        {
            StartCoroutine(DetectionRoutine());
        }

        IEnumerator DetectionRoutine()
        {
            RaycastHit hit;
            Collider currentCollider = null;
            Renderer currentRenderer = null;
            var currentSurfaceType = TextureSurfaceType.Undefined;
            while (true)
            {
                if (Physics.Raycast(transform.position, RaycastDirection, out hit))
                {
                    if (hit.collider != currentCollider)
                    {
                        currentCollider = hit.collider;
                        currentRenderer = currentCollider.GetComponent<Renderer>();
                    }
                }
                else
                {
                    if (currentRenderer != null)
                    {
                        currentRenderer = null;
                        currentCollider = null;
                        currentSurfaceType = TextureSurfaceType.Undefined;
                        OnTagChanged.Invoke(currentSurfaceType);
                    }
                }
                if (currentRenderer != null)
                {
                    var mat = currentRenderer.sharedMaterial;
                    if (mat != null)
                    {
                        var tex = mat.mainTexture;
                        TextureData myData;
                        if (tex != null && tex.GetUserData(out myData))
                        {
                            if (currentSurfaceType != myData.SurfaceType)
                            {
                                currentSurfaceType = myData.SurfaceType;
                                OnTagChanged.Invoke(currentSurfaceType);
                            }
                        }
                    } 
                }
                yield return new WaitForSeconds(RaycastInterval);
            }
        }

        public void TestDebugSurfaceType(TextureSurfaceType type)
        {
            Debug.Log(type);
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, RaycastDirection);
        }
    }
}
