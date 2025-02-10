using System.Collections.Generic;
using UnityEngine;

public class PlayerTimeTraveled : MonoBehaviour
{
    [HideInInspector] public List<Vector3> positions;

    private void FixedUpdate()
    {
        if (positions != null && positions.Count > 0)
        {
            transform.position = positions[0];
            positions.RemoveAt(0);
        }
    }
}
