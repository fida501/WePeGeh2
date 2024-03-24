using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    private SpriteRenderer sprite;
    private Animator animator;
    private Rigidbody2D rb;

    public float playerSpeed = 15f;

    public bool moving = false;

    private float dirX = 0f;

    private enum MovementState { idle, running }


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {

        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            return;
        }
        //animator.SetFloat("Speed", Mathf.Abs(playerSpeed));
        dirX = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(dirX * playerSpeed, rb.velocity.y); ;

        if (Input.GetKeyDown("space"))
        {
            //GetComponent<Rigidbody2D>().velocity = new Vector3(0, 8f, 0);
        }
        UpdateAnimation();


    }

    private void UpdateAnimation()
    {
        MovementState state;
        if (dirX > 0)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }
        animator.SetInteger("state", (int)state);
    }

}
