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
                    ""name"": ""MoveDrone"",
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
                },
                {
                    ""name"": ""Liquid"",
                    ""type"": ""Button"",
                    ""id"": ""d4b1a5ba-dd1e-4bf3-9dfe-a08235d7bf56"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Temp"",
                    ""type"": ""Button"",
                    ""id"": ""c6470955-2aa3-4f6f-848b-dc43c211cc0b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""OpenModes"",
                    ""type"": ""Button"",
                    ""id"": ""98664405-4f95-4592-9b18-01675e293ad1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""5a78ffc7-20ee-4f37-b383-0c5d23e0076b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Navigate"",
                    ""type"": ""Value"",
                    ""id"": ""564d2ba8-c612-4e38-99c8-7a08eb5e7180"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a8c9079a-3d99-4d55-9372-e9f56b8c9544"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""RotateCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""feb75385-be04-4cdd-8778-4b1edd321279"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": ""Normalize(max=1)"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Vertical Move Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4c282a72-b7f6-4033-bf26-2b516ccc6c3e"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyMouse"",
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
                    ""groups"": ""Gamepad"",
                    ""action"": ""Vertical Move Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4541e031-5f5e-49ee-8280-709c897641b3"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyMouse"",
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
                    ""groups"": ""Gamepad"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""919060d9-65a1-4909-87dc-e4bba541161a"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyMouse"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Switch Liquid"",
                    ""id"": ""faf0ba83-a8ca-4fb1-a1b9-3b6924a8de5a"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Liquid"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Negative"",
                    ""id"": ""6172a23d-56bf-4ffe-862e-6b3cb1530850"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Liquid"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Positive"",
                    ""id"": ""900ed522-8e5a-43f4-a35a-443254e2e7e5"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Liquid"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""AdjustTemp"",
                    ""id"": ""fef69de5-f7c6-43e5-899c-ca78f63fa63f"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Temp"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""81136f9b-ab27-4b0e-8cd0-841a05d9d54d"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Temp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""723e01eb-50be-46fd-ad6e-b6a547b2c566"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Temp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""86e3c1f4-9660-4911-a92c-ad248be241e4"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": ""Clamp(min=-2,max=2)"",
                    ""groups"": ""KeyMouse"",
                    ""action"": ""Temp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9aba5827-427a-478d-8f3a-cb0e39ab6c8a"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""OpenModes"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2e55ca85-6757-4bb0-b56b-8bc004fb2b1b"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyMouse"",
                    ""action"": ""OpenModes"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a68b653c-c7dc-4484-91e8-2893e4e1ad23"",
                    ""path"": ""*/{Submit}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad;KeyMouse"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ddfe036e-b6b3-47c6-a680-0883d500d630"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5e6ac1e5-8c04-4ae5-a157-c1fb21c1bd3e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e46179b5-d359-4c7b-8097-d4785f12f3e6"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""MoveDrone"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""21b29d85-f89b-445a-92bf-3320a5a315eb"",
                    ""path"": ""Dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveDrone"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""def18859-ad47-45b3-9b22-b9e1d9080548"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""MoveDrone"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""up"",
                    ""id"": ""df4015c2-6758-407a-a062-bbb765f13ea2"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""MoveDrone"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""19102914-d1d2-464d-9773-07e3fc49af45"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""MoveDrone"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a1a6aa6a-385d-457c-8ff5-30fc13051c06"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""MoveDrone"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""880b06e0-b0fd-4a46-bc9a-13436b0feb21"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""MoveDrone"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""0b63360f-7923-4813-998f-67521ddcf766"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""MoveDrone"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""c16e5441-0e82-4fcc-b027-930194e692ab"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""MoveDrone"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""486a965e-0319-4233-9eb0-457ae18c1248"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""MoveDrone"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""975bd714-ed0d-43b9-8e30-81023d7b4f36"",
                    ""path"": ""<XRController>/{Primary2DAxis}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XR"",
                    ""action"": ""MoveDrone"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2a2a3553-3b42-41ee-be79-d7085c3017e6"",
                    ""path"": ""<Joystick>/stick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""MoveDrone"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""035a1725-a0b5-4d59-9cb8-e53ca6ef150c"",
                    ""path"": ""Dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MoveDrone"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""799115ed-ef6a-4c08-beb5-ee7aeec0a3c6"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyMouse"",
                    ""action"": ""MoveDrone"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""69e59cd7-d958-4280-9335-edeeaf80afda"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyMouse"",
                    ""action"": ""MoveDrone"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""9c5d6e46-1ab2-4ffb-97f0-5f53443b789d"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyMouse"",
                    ""action"": ""MoveDrone"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""d2281c6a-ab7a-4759-99be-c684f46124fb"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyMouse"",
                    ""action"": ""MoveDrone"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""16e9bf9b-a56e-47f8-911b-4e8346567945"",
                    ""path"": ""<XRController>/{Primary2DAxis}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MoveDrone"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f2a9c058-748b-49de-aba9-4835234b6665"",
                    ""path"": ""<Joystick>/stick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MoveDrone"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""80f79ba5-a347-429f-a853-02e31895c826"",
                    ""path"": ""<Pointer>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyMouse"",
                    ""action"": ""RotateCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2b37f8f2-1323-44f6-997b-43de1b1b58cd"",
                    ""path"": ""<Pointer>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyMouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d5567058-66e7-4a42-9753-62c98f9d4db4"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""MoveDrone"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fc8bb228-0fbc-4793-9fc3-d7000fb3c1a9"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""KeyMouse"",
            ""bindingGroup"": ""KeyMouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Drone
        m_Drone = asset.FindActionMap("Drone", throwIfNotFound: true);
        m_Drone_MoveDrone = m_Drone.FindAction("MoveDrone", throwIfNotFound: true);
        m_Drone_RotateCamera = m_Drone.FindAction("RotateCamera", throwIfNotFound: true);
        m_Drone_VerticalMoveUp = m_Drone.FindAction("Vertical Move Up", throwIfNotFound: true);
        m_Drone_VerticalMoveDown = m_Drone.FindAction("Vertical Move Down", throwIfNotFound: true);
        m_Drone_Shoot = m_Drone.FindAction("Shoot", throwIfNotFound: true);
        m_Drone_Liquid = m_Drone.FindAction("Liquid", throwIfNotFound: true);
        m_Drone_Temp = m_Drone.FindAction("Temp", throwIfNotFound: true);
        m_Drone_OpenModes = m_Drone.FindAction("OpenModes", throwIfNotFound: true);
        m_Drone_Select = m_Drone.FindAction("Select", throwIfNotFound: true);
        m_Drone_Navigate = m_Drone.FindAction("Navigate", throwIfNotFound: true);
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
    private readonly InputAction m_Drone_MoveDrone;
    private readonly InputAction m_Drone_RotateCamera;
    private readonly InputAction m_Drone_VerticalMoveUp;
    private readonly InputAction m_Drone_VerticalMoveDown;
    private readonly InputAction m_Drone_Shoot;
    private readonly InputAction m_Drone_Liquid;
    private readonly InputAction m_Drone_Temp;
    private readonly InputAction m_Drone_OpenModes;
    private readonly InputAction m_Drone_Select;
    private readonly InputAction m_Drone_Navigate;
    public struct DroneActions
    {
        private @InputActions m_Wrapper;
        public DroneActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveDrone => m_Wrapper.m_Drone_MoveDrone;
        public InputAction @RotateCamera => m_Wrapper.m_Drone_RotateCamera;
        public InputAction @VerticalMoveUp => m_Wrapper.m_Drone_VerticalMoveUp;
        public InputAction @VerticalMoveDown => m_Wrapper.m_Drone_VerticalMoveDown;
        public InputAction @Shoot => m_Wrapper.m_Drone_Shoot;
        public InputAction @Liquid => m_Wrapper.m_Drone_Liquid;
        public InputAction @Temp => m_Wrapper.m_Drone_Temp;
        public InputAction @OpenModes => m_Wrapper.m_Drone_OpenModes;
        public InputAction @Select => m_Wrapper.m_Drone_Select;
        public InputAction @Navigate => m_Wrapper.m_Drone_Navigate;
        public InputActionMap Get() { return m_Wrapper.m_Drone; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DroneActions set) { return set.Get(); }
        public void SetCallbacks(IDroneActions instance)
        {
            if (m_Wrapper.m_DroneActionsCallbackInterface != null)
            {
                @MoveDrone.started -= m_Wrapper.m_DroneActionsCallbackInterface.OnMoveDrone;
                @MoveDrone.performed -= m_Wrapper.m_DroneActionsCallbackInterface.OnMoveDrone;
                @MoveDrone.canceled -= m_Wrapper.m_DroneActionsCallbackInterface.OnMoveDrone;
                @RotateCamera.started -= m_Wrapper.m_DroneActionsCallbackInterface.OnRotateCamera;
                @RotateCamera.performed -= m_Wrapper.m_DroneActionsCallbackInterface.OnRotateCamera;
                @RotateCamera.canceled -= m_Wrapper.m_DroneActionsCallbackInterface.OnRotateCamera;
                @VerticalMoveUp.started -= m_Wrapper.m_DroneActionsCallbackInterface.OnVerticalMoveUp;
                @VerticalMoveUp.performed -= m_Wrapper.m_DroneActionsCallbackInterface.OnVerticalMoveUp;
                @VerticalMoveUp.canceled -= m_Wrapper.m_DroneActionsCallbackInterface.OnVerticalMoveUp;
                @VerticalMoveDown.started -= m_Wrapper.m_DroneActionsCallbackInterface.OnVerticalMoveDown;
                @VerticalMoveDown.performed -= m_Wrapper.m_DroneActionsCallbackInterface.OnVerticalMoveDown;
                @VerticalMoveDown.canceled -= m_Wrapper.m_DroneActionsCallbackInterface.OnVerticalMoveDown;
                @Shoot.started -= m_Wrapper.m_DroneActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_DroneActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_DroneActionsCallbackInterface.OnShoot;
                @Liquid.started -= m_Wrapper.m_DroneActionsCallbackInterface.OnLiquid;
                @Liquid.performed -= m_Wrapper.m_DroneActionsCallbackInterface.OnLiquid;
                @Liquid.canceled -= m_Wrapper.m_DroneActionsCallbackInterface.OnLiquid;
                @Temp.started -= m_Wrapper.m_DroneActionsCallbackInterface.OnTemp;
                @Temp.performed -= m_Wrapper.m_DroneActionsCallbackInterface.OnTemp;
                @Temp.canceled -= m_Wrapper.m_DroneActionsCallbackInterface.OnTemp;
                @OpenModes.started -= m_Wrapper.m_DroneActionsCallbackInterface.OnOpenModes;
                @OpenModes.performed -= m_Wrapper.m_DroneActionsCallbackInterface.OnOpenModes;
                @OpenModes.canceled -= m_Wrapper.m_DroneActionsCallbackInterface.OnOpenModes;
                @Select.started -= m_Wrapper.m_DroneActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_DroneActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_DroneActionsCallbackInterface.OnSelect;
                @Navigate.started -= m_Wrapper.m_DroneActionsCallbackInterface.OnNavigate;
                @Navigate.performed -= m_Wrapper.m_DroneActionsCallbackInterface.OnNavigate;
                @Navigate.canceled -= m_Wrapper.m_DroneActionsCallbackInterface.OnNavigate;
            }
            m_Wrapper.m_DroneActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveDrone.started += instance.OnMoveDrone;
                @MoveDrone.performed += instance.OnMoveDrone;
                @MoveDrone.canceled += instance.OnMoveDrone;
                @RotateCamera.started += instance.OnRotateCamera;
                @RotateCamera.performed += instance.OnRotateCamera;
                @RotateCamera.canceled += instance.OnRotateCamera;
                @VerticalMoveUp.started += instance.OnVerticalMoveUp;
                @VerticalMoveUp.performed += instance.OnVerticalMoveUp;
                @VerticalMoveUp.canceled += instance.OnVerticalMoveUp;
                @VerticalMoveDown.started += instance.OnVerticalMoveDown;
                @VerticalMoveDown.performed += instance.OnVerticalMoveDown;
                @VerticalMoveDown.canceled += instance.OnVerticalMoveDown;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @Liquid.started += instance.OnLiquid;
                @Liquid.performed += instance.OnLiquid;
                @Liquid.canceled += instance.OnLiquid;
                @Temp.started += instance.OnTemp;
                @Temp.performed += instance.OnTemp;
                @Temp.canceled += instance.OnTemp;
                @OpenModes.started += instance.OnOpenModes;
                @OpenModes.performed += instance.OnOpenModes;
                @OpenModes.canceled += instance.OnOpenModes;
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
                @Navigate.started += instance.OnNavigate;
                @Navigate.performed += instance.OnNavigate;
                @Navigate.canceled += instance.OnNavigate;
            }
        }
    }
    public DroneActions @Drone => new DroneActions(this);
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_KeyMouseSchemeIndex = -1;
    public InputControlScheme KeyMouseScheme
    {
        get
        {
            if (m_KeyMouseSchemeIndex == -1) m_KeyMouseSchemeIndex = asset.FindControlSchemeIndex("KeyMouse");
            return asset.controlSchemes[m_KeyMouseSchemeIndex];
        }
    }
    public interface IDroneActions
    {
        void OnMoveDrone(InputAction.CallbackContext context);
        void OnRotateCamera(InputAction.CallbackContext context);
        void OnVerticalMoveUp(InputAction.CallbackContext context);
        void OnVerticalMoveDown(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnLiquid(InputAction.CallbackContext context);
        void OnTemp(InputAction.CallbackContext context);
        void OnOpenModes(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
        void OnNavigate(InputAction.CallbackContext context);
    }
}
