using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerTimeTraveled : MonoBehaviour
{
    [HideInInspector] public List<Vector3> positions;
    [HideInInspector] public List<float> xScale;
    [HideInInspector] public List<int> animationStates;

    private Animator animator;

    private Transform character;
    [SerializeField] private float opacityPerSecondToRemove;

    private void Start()
    {
        animator = transform.GetChild(0).gameObject.GetComponent<Animator>();

        character = transform.GetChild(0);
    }

    private void FixedUpdate()
    {
        if (positions != null && positions.Count > 0)
        {
            transform.position = positions[0];
            positions.RemoveAt(0);
        }
        else
        {
            LowerOpacityOfPlayerImage();
        }
        if (xScale != null && xScale.Count > 0)
        {
            transform.localScale =  new Vector3 (xScale[0], 1, 1);
            xScale.RemoveAt(0);
        }
        if (animationStates != null && animationStates.Count > 0)
        {
            animator.SetInteger("state", animationStates[0]);
            animationStates.RemoveAt(0);
        }
    }

    private void LowerOpacityOfPlayerImage()
    {
        bool completelyTransparent = false;
        foreach (Transform child in character)
        {
            SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
            Color originalcolor = spriteRenderer.color;
            spriteRenderer.color = new Color(originalcolor.r, originalcolor.g, originalcolor.b, originalcolor.a -= opacityPerSecondToRemove * Time.deltaTime);   
            if (spriteRenderer.color.a <= 0)
            {
                completelyTransparent = true;
            }
        }
        if (completelyTransparent) { Destroy(gameObject); }
    }
}
