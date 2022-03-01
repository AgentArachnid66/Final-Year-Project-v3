// GENERATED AUTOMATICALLY FROM 'Assets/InputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActions"",
    ""maps"": [
        {
            ""name"": ""Drone"",
            ""id"": ""26ddc2d0-cf94-4c3a-82da-1b9fecc14794"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""cb37d42c-4de5-4fcf-bdf6-38f16bd3e17a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotateCamera"",
                    ""type"": ""Button"",
                    ""id"": ""42fa9bd6-afc0-48b2-ba76-d9adfc70b25c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""59d1b0ed-5bb5-49e6-9344-bfcf729a7c23"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Vertical Move Up"",
                    ""type"": ""Button"",
                    ""id"": ""2fd8618a-cb98-42c3-b816-75a6799328b5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Vertical Move Down"",
                    ""type"": ""Button"",
                    ""id"": ""9b980405-909c-41c3-a7e8-f375524a7572"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""1bb462d9-4c30-4ad4-ba65-503044b14831"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""38f130b4-cade-483d-a172-db81c0ee3ef7"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": ""NormalizeVector2"",
                    ""groups"": ""Xbox Control"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a8c9079a-3d99-4d55-9372-e9f56b8c9544"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox Control"",
                    ""action"": ""RotateCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a6368986-19fb-4ae6-a494-b2a7e2e31c10"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox Control"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""feb75385-be04-4cdd-8778-4b1edd321279"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": ""Normalize(max=1)"",
                    ""groups"": ""Xbox Control"",
                    ""action"": ""Vertical Move Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ceda1df7-1755-4c91-8397-446f4d86c72f"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox Control"",
                    ""action"": ""Vertical Move Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8267d1d6-046d-45b9-97dc-f6581c58bddd"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox Control"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Xbox Control"",
            ""bindingGroup"": ""Xbox Control"",
            ""devices"": [
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Drone
        m_Drone = asset.FindActionMap("Drone", throwIfNotFound: true);
        m_Drone_Move = m_Drone.FindAction("Move", throwIfNotFound: true);
        m_Drone_RotateCamera = m_Drone.FindAction("RotateCamera", throwIfNotFound: true);
        m_Drone_Select = m_Drone.FindAction("Select", throwIfNotFound: true);
        m_Drone_VerticalMoveUp = m_Drone.FindAction("Vertical Move Up", throwIfNotFound: true);
        m_Drone_VerticalMoveDown = m_Drone.FindAction("Vertical Move Down", throwIfNotFound: true);
        m_Drone_Shoot = m_Drone.FindAction("Shoot", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Drone
    private readonly InputActionMap m_Drone;
    private IDroneActions m_DroneActionsCallbackInterface;
    private readonly InputAction m_Drone_Move;
    private readonly InputAction m_Drone_RotateCamera;
    private readonly InputAction m_Drone_Select;
    private readonly InputAction m_Drone_VerticalMoveUp;
    private readonly InputAction m_Drone_VerticalMoveDown;
    private readonly InputAction m_Drone_Shoot;
    public struct DroneActions
    {
        private @InputActions m_Wrapper;
        public DroneActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Drone_Move;
        public InputAction @RotateCamera => m_Wrapper.m_Drone_RotateCamera;
        public InputAction @Select => m_Wrapper.m_Drone_Select;
        public InputAction @VerticalMoveUp => m_Wrapper.m_Drone_VerticalMoveUp;
        public InputAction @VerticalMoveDown => m_Wrapper.m_Drone_VerticalMoveDown;
        public InputAction @Shoot => m_Wrapper.m_Drone_Shoot;
        public InputActionMap Get() { return m_Wrapper.m_Drone; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DroneActions set) { return set.Get(); }
        public void SetCallbacks(IDroneActions instance)
        {
            if (m_Wrapper.m_DroneActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_DroneActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_DroneActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_DroneActionsCallbackInterface.OnMove;
                @RotateCamera.started -= m_Wrapper.m_DroneActionsCallbackInterface.OnRotateCamera;
                @RotateCamera.performed -= m_Wrapper.m_DroneActionsCallbackInterface.OnRotateCamera;
                @RotateCamera.canceled -= m_Wrapper.m_DroneActionsCallbackInterface.OnRotateCamera;
                @Select.started -= m_Wrapper.m_DroneActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_DroneActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_DroneActionsCallbackInterface.OnSelect;
                @VerticalMoveUp.started -= m_Wrapper.m_DroneActionsCallbackInterface.OnVerticalMoveUp;
                @VerticalMoveUp.performed -= m_Wrapper.m_DroneActionsCallbackInterface.OnVerticalMoveUp;
                @VerticalMoveUp.canceled -= m_Wrapper.m_DroneActionsCallbackInterface.OnVerticalMoveUp;
                @VerticalMoveDown.started -= m_Wrapper.m_DroneActionsCallbackInterface.OnVerticalMoveDown;
                @VerticalMoveDown.performed -= m_Wrapper.m_DroneActionsCallbackInterface.OnVerticalMoveDown;
                @VerticalMoveDown.canceled -= m_Wrapper.m_DroneActionsCallbackInterface.OnVerticalMoveDown;
                @Shoot.started -= m_Wrapper.m_DroneActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_DroneActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_DroneActionsCallbackInterface.OnShoot;
            }
            m_Wrapper.m_DroneActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @RotateCamera.started += instance.OnRotateCamera;
                @RotateCamera.performed += instance.OnRotateCamera;
                @RotateCamera.canceled += instance.OnRotateCamera;
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
                @VerticalMoveUp.started += instance.OnVerticalMoveUp;
                @VerticalMoveUp.performed += instance.OnVerticalMoveUp;
                @VerticalMoveUp.canceled += instance.OnVerticalMoveUp;
                @VerticalMoveDown.started += instance.OnVerticalMoveDown;
                @VerticalMoveDown.performed += instance.OnVerticalMoveDown;
                @VerticalMoveDown.canceled += instance.OnVerticalMoveDown;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
            }
        }
    }
    public DroneActions @Drone => new DroneActions(this);
    private int m_XboxControlSchemeIndex = -1;
    public InputControlScheme XboxControlScheme
    {
        get
        {
            if (m_XboxControlSchemeIndex == -1) m_XboxControlSchemeIndex = asset.FindControlSchemeIndex("Xbox Control");
            return asset.controlSchemes[m_XboxControlSchemeIndex];
        }
    }
    public interface IDroneActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnRotateCamera(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
        void OnVerticalMoveUp(InputAction.CallbackContext context);
        void OnVerticalMoveDown(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
    }
}
