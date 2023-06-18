using UnityEngine;

public class PlayerDragging : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _playerMaxPosX;
    [Space] [SerializeField] private TouchSlider _touchSlider;
    
    private bool _isPointerDown;
    private Vector3 _playerPosition;
    
    public PlayerController PlayerController;
    public DefaultGun DefaultGun;
    public delegate void PlayerShootEvent();
    public event PlayerShootEvent OnPlayerShootEvent;
    
    private void Start()
    {
        AddListeners();
    }

    private void Update()
    {
        if (_isPointerDown)
            SmoothMovePlayerX();
    }

    private void OnPointerDown() => 
        _isPointerDown = true;

    private void OnPointerDrag(float xMovement)
    {
        if (_isPointerDown)
        {
            MovePlayerX(xMovement); 
            PlayerController.PerformShoot(DefaultGun);
            OnPlayerShootEvent?.Invoke();
        }
    }
    
    private void OnDestroy() =>
        RemoveListeners();

    private void SmoothMovePlayerX()
    {
        PlayerController.transform.position = Vector3.Lerp(
            PlayerController.transform.position,
            _playerPosition,
            _moveSpeed * Time.deltaTime);
    }

    private void MovePlayerX(float xMovement)
    {
        _playerPosition = PlayerController.transform.position;
        _playerPosition.x = xMovement * _playerMaxPosX;
    }
    
    private void AddListeners()
    {
        _touchSlider.OnPointerDownEvent += OnPointerDown;
        _touchSlider.OnPointerDragEvent += OnPointerDrag;
    }
    
    private void RemoveListeners()
    {
        _touchSlider.OnPointerDownEvent -= OnPointerDown;
        _touchSlider.OnPointerDragEvent -= OnPointerDrag;
    }
}
