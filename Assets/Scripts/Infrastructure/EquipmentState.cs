public class EquipmentState : PlayerState
{
    private readonly StateMachine _stateMachine;
    private readonly IInputService _inputService;
    private readonly CameraController _cameraController;

    public EquipmentState(Player player,
        StateMachine stateMachine,
        IInputService inputService,
        CameraController cameraController) : base(player)
    {
        _stateMachine = stateMachine;
        _inputService = inputService;
        _cameraController = cameraController;
    }

    public override void Enter()
    {
        base.Enter();
        
        _cameraController.SetEquipmentCamera();
    }

    public override void Update()
    {
        base.Update();
        
        if (_inputService.IsInventoryButtonDown)
            _stateMachine.ChangeState(_player.ActiveState);
    }
}