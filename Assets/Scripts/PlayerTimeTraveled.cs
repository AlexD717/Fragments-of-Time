using System.Collections.Generic;
using UnityEngine;

public class PlayerTimeTraveled : MonoBehaviour
{
    [HideInInspector] public List<Vector3> positions;
    [HideInInspector] public List<float> xScale;

    private void FixedUpdate()
    {
        if (positions != null && positions.Count > 0)
        {
            transform.position = positions[0];
            positions.RemoveAt(0);
        }
        if (xScale != null && xScale.Count > 0)
        {
            transform.localScale =  new Vector3 (xScale[0], 1, 1);
            xScale.RemoveAt(0);
        }
    }
}
