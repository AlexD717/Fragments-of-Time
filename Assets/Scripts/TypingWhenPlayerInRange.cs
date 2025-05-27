using UnityEngine;

[RequireComponent(typeof(TypingEffect))]
public class TypingWhenPlayerInRange : MonoBehaviour
{
    [SerializeField] private float playerRange;
    [SerializeField] private bool activateWithTimeTravelPlayer;
    private TypingEffect typingEffect;
    private bool startedTyping = false;
    private Transform player;
    private Transform timeTraveledPlayer;

    private void Awake()
    {
        typingEffect = GetComponent<TypingEffect>();
        typingEffect.startTyping = false;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (activateWithTimeTravelPlayer)
        {
            GameObject timeTraveledPlayerObject = GameObject.FindGameObjectWithTag("PlayerTimeTraveled");
            if (timeTraveledPlayerObject == null)
            {
                activateWithTimeTravelPlayer = false;
            }
            else
            {
                timeTraveledPlayer = timeTraveledPlayerObject.transform;
            }
        }
    }

    private void Update()
    {
        if (!startedTyping)
        {
            float distanceToPlayer = Vector2.Distance(player.position, transform.position);
            if (activateWithTimeTravelPlayer)
            {
                float distanceToTimeTraveledPlayer = Vector2.Distance(timeTraveledPlayer.position, transform.position);
                if (distanceToTimeTraveledPlayer < distanceToPlayer)
                {
                    distanceToPlayer = distanceToTimeTraveledPlayer;
                }
            }
            if (distanceToPlayer <= playerRange)
            {
                typingEffect.startTyping = true;
                startedTyping = true;
            }
        }
    }
}