using System.Collections.Generic;
using UnityEngine;

public class PlayerTimeTraveled : MonoBehaviour
{
    [HideInInspector] public List<Vector3> positions;
    [HideInInspector] public List<float> xScale;
    [HideInInspector] public List<float> landedOnGroundTimes;
    [HideInInspector] public List<bool> isGroundedList;

    private Animator animator;

    private void Start()
    {
        animator = transform.GetChild(0).gameObject.GetComponent<Animator>();
        Debug.Log(landedOnGroundTimes.Count);
    }

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
        if (isGroundedList != null && isGroundedList.Count > 0){
            animator.SetBool("isGrounded", isGroundedList[0]);
            isGroundedList.RemoveAt(0);
        }
    }

    private void Update()
    {
        if (landedOnGroundTimes != null && landedOnGroundTimes.Count > 0)
        {
            if (Time.timeSinceLevelLoad >= landedOnGroundTimes[0])
            {
                Debug.Log("Landed on ground");
                animator.SetTrigger("landedOnGround");
                landedOnGroundTimes.RemoveAt(0);
            }
        }
    }
}
