using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerInput activePlayerInput;
    public GameInput InputAsset { get; private set; }

    private void Awake()
    {
        ConfigureInput();
    }

    private void ConfigureInput()
    {
        InputAsset = new GameInput();
        activePlayerInput.actions = InputAsset.asset;
        InputAsset.Enable();
        activePlayerInput.ActivateInput();
    }

    public void ConfigurePlayerBindings(GameInput.IMechInputActions actions)
    {
        InputAsset.MechInput.SetCallbacks(actions);
        if (actions != null)
        {
            InputAsset.MechInput.Enable();
        }
        else
        {
            InputAsset.MechInput.Disable();
        }
    }
}
