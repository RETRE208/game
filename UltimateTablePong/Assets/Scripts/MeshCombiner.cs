using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCombiner : MonoBehaviour {

    public bool isAvatarPreview;

    private Vector3 scale;

    private void Awake()
    {
        CombineMeshes();
    }

    private void CombineMeshes()
    {
        Quaternion oldRot = transform.rotation;
        Vector3 oldPos = transform.position;

        if (isAvatarPreview)
        {
            scale = new Vector3(0.004f, 1, 1);
        } else
        {
            scale = new Vector3(1, 1, 0.0035f);
        }

        transform.rotation = Quaternion.identity;
        transform.position = Vector3.zero;

        MeshFilter[] filters = GetComponentsInChildren<MeshFilter>();

        Mesh finalMesh = new Mesh();

        CombineInstance[] combiners = new CombineInstance[filters.Length];

        for (int a = 0; a < filters.Length; a++)
        {
            if (filters[a].transform == transform) continue;

            combiners[a].subMeshIndex = 0;
            combiners[a].mesh = filters[a].sharedMesh;
            combiners[a].transform = filters[a].transform.localToWorldMatrix;
        }

        finalMesh.CombineMeshes(combiners);

        GetComponent<MeshFilter>().sharedMesh = finalMesh;

        transform.rotation = oldRot;
        transform.position = oldPos;
        transform.localScale = scale;

        for (int a = 0; a < transform.childCount; a++)
        {
            transform.GetChild(a).gameObject.SetActive(false);
        }
    }
}
