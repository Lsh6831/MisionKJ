using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshChange : MonoBehaviour
{
    
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public MeshFilter meshFilter;

  void Awake()
  {
        skinnedMeshRenderer.sharedMesh = meshFilter.mesh;
  }
    
}
