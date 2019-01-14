using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_DrawBounds : MonoBehaviour
{
    SkinnedMeshRenderer rend;
    public Vector3 center;
    public float radius;
    public float buffer = .1f;

    float x;
    float y;
    float z;

    void Start()
    {
        rend = GetComponent<SkinnedMeshRenderer>();
    }

    private void Update()
    {
        center = rend.bounds.center;
        x = rend.bounds.extents.x;
        y = rend.bounds.extents.y;
        z = rend.bounds.extents.z;
        if (x > z) radius = x;
        else radius = z;
    }

    // Draws a wireframe sphere in the Scene view, fully enclosing
    // the object.
    void OnDrawGizmosSelected()
    {
        // A sphere that fully encloses the bounding box.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(center, radius + buffer);
    }
}
