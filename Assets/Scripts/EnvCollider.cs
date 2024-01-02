using System;
using UnityEngine;

public class EnvCollider : MonoBehaviour
{
    [SerializeField] private Material hideMaterial; //Material that will show when hideMesh (in GameManager) is off
    [SerializeField] private Material showMaterial; //Material that will show when hideMesh (in GameManager) is on
    private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        UpdateMaterial();

        GameManager.Instance.OnMeshingRenderChange += GameManager_OnMeshingRenderChange;
    }

    private void GameManager_OnMeshingRenderChange(object sender, EventArgs e)
    {
        UpdateMaterial();
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnMeshingRenderChange -= GameManager_OnMeshingRenderChange;
    }

    private void UpdateMaterial()
    {
        if (GameManager.Instance.getMeshRenderState())
        {
            meshRenderer.material = showMaterial;
        }
        else {
            meshRenderer.material = hideMaterial;
        }
    }
}
