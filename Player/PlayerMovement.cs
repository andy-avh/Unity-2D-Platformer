using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed; // control speed of the player, can be directly edited form unity
    [SerializeField] private float jumpPower; // speed/power of a jump
    [SerializeField] private LayerMask groundLayer; // 
    [SerializeField] private LayerMask wallLayer; //
    private Rigidbody2D body;
    private Animator anim;
    // private bool grounded; // helps keep track if the player is on the ground, used for jump animation (no longer needed)
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown; //responsible for creating delays between wall jumps
    private float horizontalInput; // so we can access the horizontalInput during a wall jump


    private void Awake() // loaded everytime script is loaded
    {
        // grab references
        body = GetComponent<Rigidbody2D>(); // check for player's rigidbody 2d, and store inside body variable
        anim = GetComponent<Animator>(); // get acccess to our unity animator
        boxCollider = GetComponent<BoxCollider2D>();

    }

    // moving the player
    private void Update() // runs on every frame
    {
        horizontalInput = Input.GetAxis("Horizontal"); // store GetAxis(Horizontal) value 


        // creating the 'flip' animation, so player is facing the way they are moving (left and right)
        if (horizontalInput> 0.01f) // check if player is moving right
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < -0.01f) // if not right, must be left
        {
            transform.localScale = new Vector3(-1, 1, 1); // -1 on the x-axis for left
        }

        // set animator parameters
        // if x axis value is >0 or <0, show run animation using the boolean parameter made in the animator, name run
        // arrow keys not pressed = no movement = 0
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded()); // check if grounded to run jump animation

        // wall jumping mechanic
        if (wallJumpCooldown > 0.2f)
        {
            
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y); // vector is collection of numbers, for speed and direction - horizontal will go from -1 to 1

            if(onWall() && !isGrounded()) // checks if player is on the wall and not grounded
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero; // if player jumps on wall, becomes 'stuck'
            }
            else
            {
                body.gravityScale = 7; // this resets the gravity level of the player after they get off the wall, so they don't levitate
            }
            // jumping mechanic
            if (Input.GetKey(KeyCode.Space))    // if player presses space AND is touching the ground... (the '&& grounded' also stops player from drifting to space/infinte jumping)
            {
                Jump(); // call jump function ... jump!   
            }
        }
        else
        {
            wallJumpCooldown += Time.deltaTime;
        }


    }

    private void Jump()
    {
        // regular jump
        if(isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower); // ...jump!
            anim.SetTrigger("jump");
        }
        // this is for wall jumping
        else if(onWall() && !isGrounded()) // checks if player is on the wall and not grounded 
        {
            if (horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z); // flip player in opposite direction from the wall
            }
            else
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 2, 6);
                // gets scale of player on the x axis, makes it negative. so 1 when facing right, -1 for left. pushes player away from wall to wall jump 
                // * 2 is force of the outwards push from the wall
                // 6 is force the player is pushed up
            }
            wallJumpCooldown = 0; // wait a bit before next jump
            
        }
        
        // grounded = false; // once player jumps, no longer on the ground... duh

    }

    
    // using this method which returns a bool to tell if the player is on the ground, rather than the variable at the top
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer); // raycast - fires 'laser' (virtual line) at colliders, retruning true or false | boxcast is similar, instead uses a 'box' rather than a line
        // ^ 1st - origin of the box, 2nd - size of the box , 3rd - angle (don't need it so 0), 4th - direction, 5th - distance how far from player, 6th - layeredmask 
        return raycastHit.collider != null; // if nothing - return null (false)
    }


    // method for wall jumping
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer); // raycast - fires 'laser' (virtual line) at colliders, retruning true or false | boxcast is similar, instead uses a 'box' rather than a line
        // ^ same as isGrounded, except box is cast left/right to look for walls, and not ground
        return raycastHit.collider != null; // if nothing - return null (false)
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall(); // horizontalInput = 0 - not moving, is on the ground, and not on a wall(jump), if any of these are not met, it will return false and the player cannot attack
    }
}
