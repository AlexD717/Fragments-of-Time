using UnityEngine;

public class SpringGroup : MonoBehaviour
{
    [SerializeField] private Transform spring1;
    private SpringJoint2D springJoint1;
    [SerializeField] private Transform spring2;
    private SpringJoint2D springJoint2;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        springJoint1 = spring1.gameObject.GetComponent<SpringJoint2D>();
        springJoint2 = spring2.gameObject.GetComponent<SpringJoint2D>();
    }

    private void Update()
    {
        if (player.transform.position.x < transform.position.x)
        {
            springJoint1.enabled = true;
            spring2.localPosition = new Vector2(spring2.transform.position.x, -spring1.localPosition.y);
            springJoint2.enabled = false;
        }
        else
        {
            springJoint2.enabled = true;
            spring1.localPosition = new Vector2(spring1.transform.position.x, -spring2.localPosition.y);
            springJoint1.enabled = false;
        }
    }
}
