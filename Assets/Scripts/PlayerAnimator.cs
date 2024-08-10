using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_MOVING = "isMoving";
    private Animator animator;

    [SerializeField] private Player player;

    // Start is called before the first frame update
    public void Awake()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool(IS_MOVING, player.IsMoving());
    }
}
