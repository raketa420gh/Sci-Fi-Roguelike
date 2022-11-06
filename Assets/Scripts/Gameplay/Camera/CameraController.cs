using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _playerFollowCamera;
    [SerializeField] private CinemachineVirtualCamera _inventoryCamera;

    private Animator _animator;

    public CinemachineVirtualCamera PlayerFollowCamera => _playerFollowCamera;
    public CinemachineVirtualCamera InventoryCamera => _inventoryCamera;
    
    private void Awake() => 
        _animator = GetComponent<Animator>();

    public void SetPlayerFollowCamera() => 
        _animator.Play(AnimationCameraStateNames.PlayerFollow);

    public void SetInventoryCamera() => 
        _animator.Play(AnimationCameraStateNames.Inventory);
}
