using UnityEngine;
using UnityEngine.InputSystem;
//[RequireComponent]
public class Player_Controller : MonoBehaviour
{
    private InputAction _moveAction;

    private Vector2 _move_Input;

    public void SetAction(PlayerInput input, string moveInputName)
    {
        if (input == null) return;
        if (string.IsNullOrEmpty(moveInputName))
        {
            Debug.LogError("Move Input Action Name is Null");
            return;
        }

        InputActionAsset actions = input.actions;

        _moveAction = actions[moveInputName];

        _moveAction.performed += SetMove;
        _moveAction.canceled += SetMove;
    }

    private void SetMove(InputAction.CallbackContext context)
    {
        _move_Input = context.ReadValue<Vector2>();
    }
    public Vector2 GetMove()
    {
        return _move_Input;
    }
}
