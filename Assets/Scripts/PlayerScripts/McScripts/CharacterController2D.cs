using UnityEngine;
using UnityEngine.Rendering;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
    [Range(0, 1)][SerializeField] private float m_CrouchSpeed = .36f;           // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [Range(0, .3f)][SerializeField] private float m_MovementSmoothing = .05f;   // How much to smooth out the movement
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
    [SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching

    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    public bool m_Grounded;            // Whether or not the player is grounded.
    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 velocity = Vector3.zero;

    //wall jump
    [SerializeField] private Transform m_WallCheck;
    [SerializeField] private LayerMask m_WhatIsWall;
    public bool m_IsTouchingWall;
    const float k_WallCheckRadius = 0.2f;
    public float wallJumpHorizontalForce = 1f;
    public float walljumpForceUp;

    [SerializeField] private float m_CoyoteTime = 0.2f; // Coyote time in seconds
    private float m_CoyoteTimeCounter; // Counter for coyote time

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                break;
            }
        }

        if (wasGrounded && !m_Grounded)
        {
            m_CoyoteTimeCounter = m_CoyoteTime;
        }
        else if (m_Grounded)
        {
            m_CoyoteTimeCounter = m_CoyoteTime;
        }
        else
        {
            m_CoyoteTimeCounter -= Time.fixedDeltaTime;
        }

        m_IsTouchingWall = Physics2D.OverlapCircle(m_WallCheck.position, k_WallCheckRadius, m_WhatIsWall);
    }


    public void Move(float move, bool crouch, bool jump, bool wallJump)
    {
        // If crouching, check to see if the character can stand up
        if (!crouch)
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
                crouch = true;
            }
        }

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {

            // If crouching
            if (crouch)
            {
                // Reduce the speed by the crouchSpeed multiplier
                move *= m_CrouchSpeed;

                // Disable one of the colliders when crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = false;
            }
            else
            {
                // Enable the collider when not crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = true;
            }

            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref velocity, m_MovementSmoothing);

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }

        if (m_IsTouchingWall && !m_Grounded && wallJump)
        {
            m_Rigidbody2D.velocity= new Vector2(-gameObject.transform.localScale.x * wallJumpHorizontalForce, walljumpForceUp);
            Flip();
        }
        else if ((m_Grounded || m_CoyoteTimeCounter > 0f) && jump)
        {
            // Add a vertical force to the player.
            m_Grounded = false;
            m_CoyoteTimeCounter = 0f; // Reset coyote time when jumping
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }
    }
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
