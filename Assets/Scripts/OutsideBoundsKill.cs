using UnityEngine;

public class OutsideBoundsKill : MonoBehaviour
{
    [SerializeField] private Transform bottomPos;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (player.transform.position.y < bottomPos.position.y)
        {
            player.GetComponent<Player>().InstaKillPlayer();
        }
    }
}
