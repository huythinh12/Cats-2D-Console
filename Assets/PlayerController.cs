//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/PlayerController.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerController : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerController()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerController"",
    ""maps"": [
        {
            ""name"": ""PlayerControl"",
            ""id"": ""9a1bbfff-d5cd-4f90-ac68-0a3c2a44e480"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""183a9d2a-71f0-49ad-a6b8-14cb1c9d8ed2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""550b37e9-4268-4097-a257-f6390a2703e6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Push"",
                    ""type"": ""PassThrough"",
                    ""id"": ""dbff1a41-5064-4e23-92fe-732c6c43e499"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Hold"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Start"",
                    ""type"": ""Button"",
                    ""id"": ""2c7531ba-0e21-4b91-9c6e-50cc304c619a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SelectRight"",
                    ""type"": ""Button"",
                    ""id"": ""19a56f24-a1be-4cbc-b095-8f2d94d0baf8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SelectLeft"",
                    ""type"": ""Button"",
                    ""id"": ""f8540a87-0e20-48f2-b531-a1872fd35e16"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5bf06ab6-da84-4a90-85e7-43c0c0a8936f"",
                    ""path"": ""<Gamepad>/leftStick/"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""98196a05-13f1-4d6b-8c66-0eecf667caca"",
                    ""path"": ""<Gamepad>/dpad/"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""69ab2bb4-6408-4654-88ad-89a490c782a2"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f7be297d-1e80-4431-a0dc-a1a8970d367b"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ddf1496b-328e-4fed-a8bf-87c70defe97b"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""c2ca346f-4f47-4548-bc40-1bb5db96ab09"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""85a88d23-164b-4b58-a21a-28099cd82ebe"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""2784d3c7-917d-4e5f-970a-e3904d862fde"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""734416ea-42f2-4cb6-a43d-2321a036a397"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4a7ea38a-1eb8-42b0-8a18-7eff1bfc1676"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Push"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b0ae28fd-6f4e-41c5-b20a-7581dcd4c218"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Push"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aa480b6b-a32c-4913-9b6f-a54045d8cf29"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Start"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ef44305b-a9bc-471c-8e08-91c379d4c9f7"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Start"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ec075be4-4de2-46c5-844a-5ee795538e03"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SelectRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5af0018c-eaec-43ca-95be-4fab26d91082"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""SelectRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""639615e8-1e8b-4bbd-baad-ec8a359d92b7"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""SelectRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""779de891-4a18-45ad-88be-1f8ee56175e0"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SelectLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2cb4cd0b-9ccc-4079-bb50-bdb3021ed310"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""SelectLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""07cbb47a-3ea5-4728-82b2-596dce7bdc53"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""SelectLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""GamePad"",
            ""bindingGroup"": ""GamePad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // PlayerControl
        m_PlayerControl = asset.FindActionMap("PlayerControl", throwIfNotFound: true);
        m_PlayerControl_Move = m_PlayerControl.FindAction("Move", throwIfNotFound: true);
        m_PlayerControl_Jump = m_PlayerControl.FindAction("Jump", throwIfNotFound: true);
        m_PlayerControl_Push = m_PlayerControl.FindAction("Push", throwIfNotFound: true);
        m_PlayerControl_Start = m_PlayerControl.FindAction("Start", throwIfNotFound: true);
        m_PlayerControl_SelectRight = m_PlayerControl.FindAction("SelectRight", throwIfNotFound: true);
        m_PlayerControl_SelectLeft = m_PlayerControl.FindAction("SelectLeft", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // PlayerControl
    private readonly InputActionMap m_PlayerControl;
    private IPlayerControlActions m_PlayerControlActionsCallbackInterface;
    private readonly InputAction m_PlayerControl_Move;
    private readonly InputAction m_PlayerControl_Jump;
    private readonly InputAction m_PlayerControl_Push;
    private readonly InputAction m_PlayerControl_Start;
    private readonly InputAction m_PlayerControl_SelectRight;
    private readonly InputAction m_PlayerControl_SelectLeft;
    public struct PlayerControlActions
    {
        private @PlayerController m_Wrapper;
        public PlayerControlActions(@PlayerController wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_PlayerControl_Move;
        public InputAction @Jump => m_Wrapper.m_PlayerControl_Jump;
        public InputAction @Push => m_Wrapper.m_PlayerControl_Push;
        public InputAction @Start => m_Wrapper.m_PlayerControl_Start;
        public InputAction @SelectRight => m_Wrapper.m_PlayerControl_SelectRight;
        public InputAction @SelectLeft => m_Wrapper.m_PlayerControl_SelectLeft;
        public InputActionMap Get() { return m_Wrapper.m_PlayerControl; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerControlActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerControlActions instance)
        {
            if (m_Wrapper.m_PlayerControlActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerControlActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerControlActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerControlActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_PlayerControlActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerControlActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerControlActionsCallbackInterface.OnJump;
                @Push.started -= m_Wrapper.m_PlayerControlActionsCallbackInterface.OnPush;
                @Push.performed -= m_Wrapper.m_PlayerControlActionsCallbackInterface.OnPush;
                @Push.canceled -= m_Wrapper.m_PlayerControlActionsCallbackInterface.OnPush;
                @Start.started -= m_Wrapper.m_PlayerControlActionsCallbackInterface.OnStart;
                @Start.performed -= m_Wrapper.m_PlayerControlActionsCallbackInterface.OnStart;
                @Start.canceled -= m_Wrapper.m_PlayerControlActionsCallbackInterface.OnStart;
                @SelectRight.started -= m_Wrapper.m_PlayerControlActionsCallbackInterface.OnSelectRight;
                @SelectRight.performed -= m_Wrapper.m_PlayerControlActionsCallbackInterface.OnSelectRight;
                @SelectRight.canceled -= m_Wrapper.m_PlayerControlActionsCallbackInterface.OnSelectRight;
                @SelectLeft.started -= m_Wrapper.m_PlayerControlActionsCallbackInterface.OnSelectLeft;
                @SelectLeft.performed -= m_Wrapper.m_PlayerControlActionsCallbackInterface.OnSelectLeft;
                @SelectLeft.canceled -= m_Wrapper.m_PlayerControlActionsCallbackInterface.OnSelectLeft;
            }
            m_Wrapper.m_PlayerControlActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Push.started += instance.OnPush;
                @Push.performed += instance.OnPush;
                @Push.canceled += instance.OnPush;
                @Start.started += instance.OnStart;
                @Start.performed += instance.OnStart;
                @Start.canceled += instance.OnStart;
                @SelectRight.started += instance.OnSelectRight;
                @SelectRight.performed += instance.OnSelectRight;
                @SelectRight.canceled += instance.OnSelectRight;
                @SelectLeft.started += instance.OnSelectLeft;
                @SelectLeft.performed += instance.OnSelectLeft;
                @SelectLeft.canceled += instance.OnSelectLeft;
            }
        }
    }
    public PlayerControlActions @PlayerControl => new PlayerControlActions(this);
    private int m_GamePadSchemeIndex = -1;
    public InputControlScheme GamePadScheme
    {
        get
        {
            if (m_GamePadSchemeIndex == -1) m_GamePadSchemeIndex = asset.FindControlSchemeIndex("GamePad");
            return asset.controlSchemes[m_GamePadSchemeIndex];
        }
    }
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IPlayerControlActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnPush(InputAction.CallbackContext context);
        void OnStart(InputAction.CallbackContext context);
        void OnSelectRight(InputAction.CallbackContext context);
        void OnSelectLeft(InputAction.CallbackContext context);
    }
}