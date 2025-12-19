using Unity.Entities;
using UnityEngine.InputSystem;

public partial class PaddleInputSystem : SystemBase
{
    private InputAction moveAction;

    protected override void OnCreate()
    {
        var input = new InputSystem_Actions();
        moveAction = input.gameplay.Move;
        moveAction.Enable();

        EntityManager.CreateSingleton<PaddleInput>();
    }

    protected override void OnDestroy()
    {
       
        moveAction.Disable();
        base.OnDestroy();
    }

    protected override void OnUpdate()
    {
        SystemAPI.SetSingleton(new PaddleInput
        {
            Move = moveAction.ReadValue<float>()
        });
    }
}
