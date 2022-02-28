// GENERATED AUTOMATICALLY FROM 'Assets/InputManager.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputManager : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputManager()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputManager"",
    ""maps"": [
        {
            ""name"": ""Xbox Control Scheme"",
            ""id"": ""e8658190-5594-4481-8420-4bfd33191374"",
            ""actions"": [
                {
                    ""name"": ""Buttons"",
                    ""type"": ""Button"",
                    ""id"": ""bcdcdda6-e8f2-4850-9295-51277c840125"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sticks"",
                    ""type"": ""Value"",
                    ""id"": ""72b46b2d-0a38-48e1-940f-dd9205ded6a4"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""D-Pad"",
                    ""id"": ""afa884e9-93ee-4923-bec1-3a3a699023df"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Buttons"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""2e2f5b27-612f-4d5a-ad88-3b0e393773bc"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox Controller Scheme"",
                    ""action"": ""Buttons"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""8bc2b283-a8b2-4a8c-bb28-20cf52b24a61"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox Controller Scheme"",
                    ""action"": ""Buttons"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""ddb1c6f8-a273-4ffb-b2bb-ddb3af1c5595"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox Controller Scheme"",
                    ""action"": ""Buttons"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""d2d65658-0ce2-4478-aca3-b46f2290660b"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox Controller Scheme"",
                    ""action"": ""Buttons"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""9337ef82-686a-495c-a4ac-7a393930f596"",
                    ""path"": ""<Gamepad>/leftStick/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox Controller Scheme"",
                    ""action"": ""Sticks"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""215d9d41-59bc-44fc-93f8-276f31a7699d"",
                    ""path"": ""<Gamepad>/leftStick/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox Controller Scheme"",
                    ""action"": ""Sticks"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""71381004-6e78-4766-a0fb-6264f87f7904"",
                    ""path"": ""<Gamepad>/rightStick/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox Controller Scheme"",
                    ""action"": ""Sticks"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1b9bc3b9-4530-48bd-bf18-a3de55616d3f"",
                    ""path"": ""<Gamepad>/rightStick/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox Controller Scheme"",
                    ""action"": ""Sticks"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Xbox Controller Scheme"",
            ""bindingGroup"": ""Xbox Controller Scheme"",
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
        // Xbox Control Scheme
        m_XboxControlScheme = asset.FindActionMap("Xbox Control Scheme", throwIfNotFound: true);
        m_XboxControlScheme_Buttons = m_XboxControlScheme.FindAction("Buttons", throwIfNotFound: true);
        m_XboxControlScheme_Sticks = m_XboxControlScheme.FindAction("Sticks", throwIfNotFound: true);
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

    // Xbox Control Scheme
    private readonly InputActionMap m_XboxControlScheme;
    private IXboxControlSchemeActions m_XboxControlSchemeActionsCallbackInterface;
    private readonly InputAction m_XboxControlScheme_Buttons;
    private readonly InputAction m_XboxControlScheme_Sticks;
    public struct XboxControlSchemeActions
    {
        private @InputManager m_Wrapper;
        public XboxControlSchemeActions(@InputManager wrapper) { m_Wrapper = wrapper; }
        public InputAction @Buttons => m_Wrapper.m_XboxControlScheme_Buttons;
        public InputAction @Sticks => m_Wrapper.m_XboxControlScheme_Sticks;
        public InputActionMap Get() { return m_Wrapper.m_XboxControlScheme; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(XboxControlSchemeActions set) { return set.Get(); }
        public void SetCallbacks(IXboxControlSchemeActions instance)
        {
            if (m_Wrapper.m_XboxControlSchemeActionsCallbackInterface != null)
            {
                @Buttons.started -= m_Wrapper.m_XboxControlSchemeActionsCallbackInterface.OnButtons;
                @Buttons.performed -= m_Wrapper.m_XboxControlSchemeActionsCallbackInterface.OnButtons;
                @Buttons.canceled -= m_Wrapper.m_XboxControlSchemeActionsCallbackInterface.OnButtons;
                @Sticks.started -= m_Wrapper.m_XboxControlSchemeActionsCallbackInterface.OnSticks;
                @Sticks.performed -= m_Wrapper.m_XboxControlSchemeActionsCallbackInterface.OnSticks;
                @Sticks.canceled -= m_Wrapper.m_XboxControlSchemeActionsCallbackInterface.OnSticks;
            }
            m_Wrapper.m_XboxControlSchemeActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Buttons.started += instance.OnButtons;
                @Buttons.performed += instance.OnButtons;
                @Buttons.canceled += instance.OnButtons;
                @Sticks.started += instance.OnSticks;
                @Sticks.performed += instance.OnSticks;
                @Sticks.canceled += instance.OnSticks;
            }
        }
    }
    public XboxControlSchemeActions @XboxControlScheme => new XboxControlSchemeActions(this);
    private int m_XboxControllerSchemeSchemeIndex = -1;
    public InputControlScheme XboxControllerSchemeScheme
    {
        get
        {
            if (m_XboxControllerSchemeSchemeIndex == -1) m_XboxControllerSchemeSchemeIndex = asset.FindControlSchemeIndex("Xbox Controller Scheme");
            return asset.controlSchemes[m_XboxControllerSchemeSchemeIndex];
        }
    }
    public interface IXboxControlSchemeActions
    {
        void OnButtons(InputAction.CallbackContext context);
        void OnSticks(InputAction.CallbackContext context);
    }
}
