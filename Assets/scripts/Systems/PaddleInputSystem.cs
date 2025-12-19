using Unity.Entities;
using UnityEngine.InputSystem;

public partial class PaddleInputSystem : SystemBase
{
    private InputAction moveAction;
    InputSystem_Actions input;

    protected override void OnCreate()
    {
         input = new InputSystem_Actions();
        moveAction = input.gameplay.Move;
        moveAction.Enable();

        EntityManager.CreateSingleton<PaddleInput>();
    }

    protected override void OnDestroy()
    {
      
        moveAction.Disable();
        input.Disable();
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
