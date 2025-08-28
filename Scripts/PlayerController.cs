
using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{ 
    public InputSystem_Actions PlayerInputSystem;
    private Rigidbody _rb;

    [Header("Movement")]
    private int _currentLaneIndex = 1;
    [SerializeField] private float[] lanesXPos = {-3f, 0f, 3f};
    [SerializeField] private float laneChangeSpeed = 12f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float forcedFallSpeed = 10;
    public bool IsGrounded { get => isGrounded; set => isGrounded = value; }
    [SerializeField] private bool isGrounded = true;

    public int currentFormId;
    private bool _changeFormWasPressed;
    
    [Header("Health")]
    [SerializeField] private int maxLives = 3;
    [SerializeField] private float lowestYposToRespawn = -5;
    private Vector3 _respawnPosition;
    public bool IsDead { get => _isDead; set => _isDead = value; }
    private bool _isDead = false;
    public HealthSystem HealthSystem {get => _healthSystem;}
    private HealthSystem _healthSystem;
    private bool _isTookDamageFromFall = false;
    [SerializeField] private float fallDamageInvulDuration;
    
    [Header("Debug")]
    [SerializeField] public bool isInvulreable = false;
    
    [SerializeField] private Animator animator;
    
    private FrameController _currentFrame;
    
    private void Awake()
    {
        PlayerInputSystem = new InputSystem_Actions();
        _rb = GetComponent<Rigidbody>();
        _healthSystem = new HealthSystem(this, maxLives);
        _isTookDamageFromFall = false;
        
        PlayerInputSystem.Player.MoveLane.performed += OnMoveInput;
        
        PlayerInputSystem.Player.Jump.performed += _ => TryJump();
        PlayerInputSystem.Player.Land.performed += _ => ForceLand();
    }

    private void Start()
    {
        _respawnPosition = transform.position;
    }

    private void OnEnable() => PlayerInputSystem.Enable();
    private void OnDisable() => PlayerInputSystem.Disable();


    private void OnMoveInput(InputAction.CallbackContext ctx)
    {
        float direction = ctx.ReadValue<float>();
        
        if (direction < 0 && _currentLaneIndex > 0)
        {
            _currentLaneIndex--;
        }
        else if (direction > 0 && _currentLaneIndex < lanesXPos.Length - 1)
        {
            _currentLaneIndex++;
        }
        
       
    }
    
    private void TryJump()
    { 
        if (isGrounded)
        {
            _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, 0f, _rb.linearVelocity.z); 
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            AudioManager.AudioLibrary.levelSounds.jumpSound.Play();
        }
       
    }
    
    private void ForceLand()
    {
        if(IsGrounded) return;
        Vector3 velocity = _rb.linearVelocity;
        velocity.y = -Mathf.Abs(forcedFallSpeed); 
        _rb.linearVelocity = velocity;
        
    }

    
    private void FixedUpdate()
    {
        Vector3 currentPosition = _rb.position;
        float targetX = lanesXPos[_currentLaneIndex];
        float newX = Mathf.MoveTowards(currentPosition.x, targetX, laneChangeSpeed * Time.fixedDeltaTime);
        transform.position = (new Vector3(newX, currentPosition.y, currentPosition.z));
        
    }

    private void Update()
    {
        CheckIfInFrame();
        CheckPlayerRespawn();
    }

    private void CheckIfInFrame()
    {
        if (PlayerInputSystem.Player.ChooseFormCube.triggered ||
            PlayerInputSystem.Player.ChooseFormSphere.triggered || 
            PlayerInputSystem.Player.ChooseFormCone.triggered)
            _changeFormWasPressed = true;
        
        if (_currentFrame 
            && _changeFormWasPressed 
            && currentFormId == _currentFrame.frameData.frameShapeId) 
        {
            _currentFrame.RegisterHit();
            _currentFrame = null; 
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<FrameController>(out FrameController frame))
        {
            _currentFrame = frame;
            frame.EnterFrame();
            _changeFormWasPressed = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<FrameController>(out FrameController frame) && frame == _currentFrame)
        {
            frame.Missed();
            _healthSystem.TakeDamage();
            _currentFrame = null;
        }
    }

    private void CheckPlayerRespawn()
    {
        if (transform.position.y <= lowestYposToRespawn)
        {
            if (!_isTookDamageFromFall)
            {
                _healthSystem.TakeDamage();
                _isTookDamageFromFall = true;
                StartFallDamageInvulnerability(fallDamageInvulDuration).Forget();
            }
            if (!IsDead)
            {   
                _rb.linearVelocity = Vector3.zero;
                _currentLaneIndex = 1;
                _rb.MovePosition(_respawnPosition + Vector3.up * 2f);
                isGrounded = true;
            }

            
        }
    }

    private async UniTaskVoid StartFallDamageInvulnerability(float duration)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(duration));
        _isTookDamageFromFall = false;
    }
}
