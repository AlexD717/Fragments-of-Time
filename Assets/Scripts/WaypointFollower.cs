using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool loopAround;
    [SerializeField] private Transform waypointParent;
    private Vector2[] waypointPositions;
    private int waypointIndex = 0;

    private void Start()
    {
        waypointPositions = new Vector2[waypointParent.childCount];
        for (int i = 0; i < waypointParent.childCount; i++)
        {
            waypointPositions[i] = waypointParent.GetChild(i).position;
        }
    }

    private void Update()
    {
        Vector2 positionToMoveTo = waypointPositions[waypointIndex];
        if (Vector2.Distance(transform.position, positionToMoveTo) < 0.2f)
        {
            NextWaypoint();
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, positionToMoveTo, speed * Time.deltaTime);
        }

    }

    private void NextWaypoint()
    {
        waypointIndex++;
        if (waypointIndex >= waypointPositions.Length)
        {
            if (loopAround)
            {
                waypointIndex = 0;
            }
            else
            {
                waypointIndex = waypointPositions.Length - 1;
            }
        }
    }
}
