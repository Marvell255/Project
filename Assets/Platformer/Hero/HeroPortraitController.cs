using UnityEngine;
using System.Collections;
using AnyPortrait;

public class HeroPortraitController : MonoBehaviour
{
    public float MaxSpeed = 5f;
    public float JumpSpeed = 8f;
    public bool PlatformRelativeJump;
    public bool AllowWallGrab = true;
    public bool AllowWallJump = true;

    public bool DisableGravityDuringWallGrab;
    public LayerMask WallGrabMask;

    public float WallJumpControlDelay = 0.15f;
    private float _wallJumpControlDelayLeft;

    public Vector2 GrabPoint = new Vector2(0.45f, 0f);

    private MovingPlatform _movingPlatform;
    private Vector3 _lastPlatformPosition = Vector3.zero;
    private Vector3 _currentPlatformDelta = Vector3.zero;
    private Animator _anim;
    private bool _groundedLastFrame;
    private bool _jumping;

    private Rigidbody2D _heroRigidbody2D;
    public bool MagnetActive { get; set; }
    public bool LeftButtonPressed { get; set; }
    public bool RightButtonPressed { get; set; }

    private AudioSource _audioSource;

    public AudioClip AudioCollectStar;
    public AudioClip AudioCollectHorse;
    public AudioClip AudioCollectJump;
    public AudioClip AudioWalk;
    public AudioClip AudioJump;
    
    private void Awake()
    {
        _heroRigidbody2D = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    public static HeroPortraitController Instance()
    {
        return GameObject.FindGameObjectWithTag("Player").GetComponent<HeroPortraitController>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        var mp = col.gameObject.GetComponent<MovingPlatform>();
        if (mp != null)
        {
            SetHeroPlatform(mp);
        }
    }

    private void SetHeroPlatform(MovingPlatform movingPlatform)
    {
        print(movingPlatform.name);

        _movingPlatform = movingPlatform;
        _lastPlatformPosition = movingPlatform.transform.position;
    }

    private void ClearHeroPlatform()
    {
        _movingPlatform = null;
        _lastPlatformPosition = Vector3.zero;
        _currentPlatformDelta = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var collect = col.gameObject.GetComponent<ICollect>();

        if (collect != null)
            collect.Collect();
    }

    private bool IsGrounded()
    {
        if (Mathf.Abs(_heroRigidbody2D.velocity.y) < 0.1f)
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
        return (Input.GetAxisRaw("Horizontal") > 0 && transform.localScale.x > 0 ||
                Input.GetAxisRaw("Horizontal") < 0 && transform.localScale.x < 0) &&
               Physics2D.OverlapCircle(
                   transform.position + new Vector3(GrabPoint.x * transform.localScale.x, GrabPoint.y, 0), 0.2f,
                   WallGrabMask);
    }

    public void PressedJump()
    {
        _jumping = true;
    }

    private void Update()
    {
        // Get____Down and Get____Up are only reliable inside of Update(), not FixedUpdate().
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            _jumping = true;
        }

        var isGrounded = IsGrounded();
        var isGrabbing = !isGrounded && _wallJumpControlDelayLeft <= 0 && IsGrabbing();

        // We aren't grounded or grabbing.  Making sure to clear our platform.
        if (_movingPlatform != null && !_groundedLastFrame && !isGrabbing && !isGrounded)
            ClearHeroPlatform();

        if (_movingPlatform != null)
        {
            //Determine how far platform has moved
            _currentPlatformDelta = _movingPlatform.transform.position - _lastPlatformPosition;
            _lastPlatformPosition = _movingPlatform.transform.position;

            transform.position += _currentPlatformDelta;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + new Vector3(GrabPoint.x * transform.localScale.x, GrabPoint.y, 0), 0.2f);
    }

    public enum HeroAnimationStatus
    {
        Idle,
        Run,
        Air,
    }

    private HeroAnimationStatus _moveStatus = HeroAnimationStatus.Idle;
    public apPortrait Portrait;
    private int _jumpCount;

    public void CrossFade(HeroAnimationStatus status)
    {
        if (_moveStatus == status) return;

        Portrait.CrossFade(status.ToString());
        _moveStatus = status;
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
//            if (isGrabbing)
//                _heroRigidbody2D.gravityScale = 0.2f;
//            else
//                _heroRigidbody2D.gravityScale = 1;
        }

        // We start off by assuming we are maintaining our velocity.
        var xVel = _heroRigidbody2D.velocity.x;
        var yVel = _heroRigidbody2D.velocity.y;

        // If we're grounded, maintain our velocity at platform velocity, with slight downward pressure to maintain the collision.

        if (isGrounded)
        {
            yVel = _heroRigidbody2D.velocity.y - 1f;
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
            if (LeftButtonPressed)
                xVel = -MaxSpeed;
            else if (RightButtonPressed)
                xVel = MaxSpeed;
            else
                xVel = Input.GetAxis("Horizontal") * MaxSpeed;
        }

        if (_jumping && ((isGrounded || _jumpCount < 2) || (isGrabbing && AllowWallJump)))
        {
            // NOTE: As-is, neither vertical velocity nor walljump speed is affected by PlatformVelocity().
            yVel = JumpSpeed;
            if (PlatformRelativeJump)
                yVel += 12;

            if (isGrabbing)
            {
                xVel = 0;
                yVel = -MaxSpeed * transform.localScale.x;
                _wallJumpControlDelayLeft = WallJumpControlDelay;
            }

            Jump();
        }


        // Apply the calculate velocity to our rigidbody
        _heroRigidbody2D.velocity = new Vector2(xVel, yVel);

        // Update facing
        var scale = transform.localScale;

        if (transform.localScale.x < 0 && (Input.GetAxis("Horizontal") > 0 || RightButtonPressed))
            scale.x = 1;
        else if (scale.x > 0 && (Input.GetAxis("Horizontal") < 0 || LeftButtonPressed))
            scale.x = -1;

        transform.localScale = scale;


        if (_jumping)
        {
            _jumping = false;
            return;
        }

        var x = Mathf.Abs(_heroRigidbody2D.velocity.x);
        if (x > 0.1 && isGrounded)
            CrossFade(HeroAnimationStatus.Run);
        if (x < 0.1 && isGrounded)
        {
            CrossFade(HeroAnimationStatus.Idle);
        }
    }

    public void Jump()
    {
        if (_moveStatus != HeroAnimationStatus.Air)
        {
            _audioSource.PlayOneShot(AudioJump);
            Portrait.Play("Jump");
            Portrait.CrossFadeQueued("Air");

            _moveStatus = HeroAnimationStatus.Air;

            _jumpCount = 1;
        }
        else if (_jumpCount == 1)
        {
            //_portrait.StopAll();
            _audioSource.PlayOneShot(AudioJump);
            Portrait.Play("DoubleJump");
            Portrait.CrossFadeQueued("Air");

            _moveStatus = HeroAnimationStatus.Air;


            _jumpCount = 2;
        }
    }

    public void PlayStarCollect()
    {
        _audioSource.PlayOneShot(AudioCollectStar);
    }
}