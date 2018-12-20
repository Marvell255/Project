using UnityEngine;
using System.Collections;

public class HeroController : MonoBehaviour
{
    public float MaxSpeed = 5f;
    public float JumpSpeed = 8f;
    public bool PlatformRelativeJump;
    public bool AllowWallGrab = true;
    public bool AllowWallJump = true;

    public bool DisableGravityDuringWallGrab;
    public LayerMask WallGrabMask;

    public float WallJumpControlDelay = 0.15f;
    float _wallJumpControlDelayLeft;

    public Vector2 GrabPoint = new Vector2(0.45f, 0f);

    private MovingPlatform _movingPlatform;
    private Animator _anim;
    private bool _groundedLastFrame;
    private bool _jumping;

    private void Start()
    {
        _anim = GetComponentInChildren<Animator>();        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        // If we collided against a platform, grab a copy of it and we can use it as our zero point for IsGrounded.
        var mp = col.transform.root.GetComponent<MovingPlatform>();
        if (mp != null)
        {
            Debug.Log("movingPlatform: " + mp.gameObject.name);
            _movingPlatform = mp;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        var star = collider.gameObject.GetComponent<Star>();
        if (star != null)
        {
            star.Collect();
        }
    }

    private bool IsGrounded()
    {
        if (Mathf.Abs(RelativeVelocity().y) < 0.1f)
        {
            // Checking floats for exact equality is bad. Also good for platforms that are moving down.

            // Since it's possible for our velocity to be exactly zero at the apex of our jump,
            // we actually require two zero velocity frames in a row.

            if (_groundedLastFrame)
                return true;

            _groundedLastFrame = true;
        }
        else
        {
            _groundedLastFrame = false;
        }

        return false;
    }

    private bool IsGrabbing()
    {
        if (AllowWallGrab == false)
            return false;

        // FIXME: Is there any chance we want to set movingPlatform here?

        // If we're pushing the joystick in the direction of our facing and an OverlapCircle test indicates a grabbable surface at the grabPoint, return true.
        return ((Input.GetAxisRaw("Horizontal") > 0 && transform.localScale.x > 0) || (Input.GetAxisRaw("Horizontal") < 0 && transform.localScale.x < 0)) &&
               Physics2D.OverlapCircle(transform.position + new Vector3(GrabPoint.x * transform.localScale.x, GrabPoint.y, 0), 0.2f, WallGrabMask);
    }

    private void Update()
    {
        // Get____Down and Get____Up are only reliable inside of Update(), not FixedUpdate().
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.UpArrow))
            _jumping = true;
    }

    private Vector2 RelativeVelocity()
    {
        return GetComponent<Rigidbody2D>().velocity - PlatformVelocity();
    }

    private Vector2 PlatformVelocity()
    {
        if (_movingPlatform == null)
            return Vector2.zero;

        return _movingPlatform.GetComponent<Rigidbody2D>().velocity;
    }

    private void FixedUpdate()
    {
        var isGrounded = IsGrounded();
        var isGrabbing = !isGrounded && _wallJumpControlDelayLeft <= 0 && IsGrabbing();

        if (_movingPlatform != null && !_groundedLastFrame && !isGrabbing && !isGrounded)
        {
            // We aren't grounded or grabbing.  Making sure to clear our platform.
            _movingPlatform = null;
        }

        // FIXME: This results in weird drifting with our current colliders
        if (DisableGravityDuringWallGrab)
        {
            if (isGrabbing)
                GetComponent<Rigidbody2D>().gravityScale = 0.2f;
            else
                GetComponent<Rigidbody2D>().gravityScale = 1;
        }

        // We start off by assuming we are maintaining our velocity.
        var xVel = GetComponent<Rigidbody2D>().velocity.x;
        var yVel = GetComponent<Rigidbody2D>().velocity.y;

        // If we're grounded, maintain our velocity at platform velocity, with slight downward pressure to maintain the collision.
        if (isGrounded)
        {
            yVel = PlatformVelocity().y - 0.01f;
        }

        // Some moves (like walljumping) might introduce a delay before x-velocity is controllable
        _wallJumpControlDelayLeft -= Time.deltaTime;

        if (isGrounded || isGrabbing)
        {
            _wallJumpControlDelayLeft = 0; // Clear the delay if we're in contact with the ground/wall
        }

        // Allow x-velocity control
        if (_wallJumpControlDelayLeft <= 0)
        {
            xVel = Input.GetAxis("Horizontal") * MaxSpeed;
            xVel += PlatformVelocity().x;
        }

        if (isGrabbing && RelativeVelocity().y <= 0)
        {
            // NOTE:  Depending on friction and gravity, the character
            // will still be sliding down unless we turn off gravityScale
            yVel = PlatformVelocity().y;

            // If we are are zero velocity (or "negative" relative to the facing of the wall)
            // set our velocity to zero so we don't bounce off
            // Also ensures that we don't inter-penetrate for one frame, which could cause us
            // to get stuck on a "ledge" between blocks.
            if (RelativeVelocity().x * transform.localScale.x <= 0)
            {
                xVel = PlatformVelocity().x;
            }
        }

        if (_jumping && (isGrounded || (isGrabbing && AllowWallJump)))
        {
            // NOTE: As-is, neither vertical velocity nor walljump speed is affected by PlatformVelocity().
            yVel = JumpSpeed;
            if (PlatformRelativeJump)
                yVel += PlatformVelocity().y;

            if (isGrabbing)
            {
                xVel = -MaxSpeed * transform.localScale.x;
                _wallJumpControlDelayLeft = WallJumpControlDelay;
            }
        }

        _jumping = false;


        // Apply the calculate velocity to our rigidbody
        GetComponent<Rigidbody2D>().velocity = new Vector2(
            xVel,
            yVel
        );

        // Update facing
        var scale = transform.localScale;
        if (scale.x < 0 && Input.GetAxis("Horizontal") > 0)
        {
            scale.x = 1;
        }
        else if (scale.x > 0 && Input.GetAxis("Horizontal") < 0)
        {
            scale.x = -1;
        }

        transform.localScale = scale;

        // Update animations
        _anim.SetFloat("xSpeed", Mathf.Abs(RelativeVelocity().x));

        if (isGrabbing)
            _anim.SetFloat("ySpeed", Mathf.Abs(1000));
        else
            _anim.SetFloat("ySpeed", RelativeVelocity().y);
    }
}