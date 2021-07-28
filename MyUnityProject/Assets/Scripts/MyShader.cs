using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyShader : MonoBehaviour
{
    public Color LineColor;

    Material m_lineMaterial;

    List<Vector3> m_lineVertices;

    Mesh mesh = null;
    // Use this for initialization
    void Start()
    {
        m_lineVertices = new List<Vector3>();
        //m_lineMaterial = new Material(Shader.Find("Unlit/Color"));
        m_lineMaterial = new Material(Shader.Find("Unlit/CustomWireframeWithTex"));
        m_lineMaterial.hideFlags = HideFlags.HideAndDontSave;
        m_lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;

        MeshFilter meshFilter = this.gameObject.GetComponent<MeshFilter>();
        if (meshFilter)
        {
            mesh = meshFilter.mesh;
        }
        SkinnedMeshRenderer skinedMesh = this.gameObject.GetComponent<SkinnedMeshRenderer>();
        if (skinedMesh)
        {
            mesh = skinedMesh.sharedMesh;
        }
    }


    void OnRenderObject()
    {
        if (mesh)
        {
            Graphics.DrawMesh(mesh, transform.position, transform.rotation, m_lineMaterial, 0);
        }

    }
}