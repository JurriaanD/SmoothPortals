// GENERATED AUTOMATICALLY FROM 'Assets/portalgunshoot.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Portalgunshoot : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Portalgunshoot()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""portalgunshoot"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""d20e90e9-d982-440f-98be-47ef27059f3d"",
            ""actions"": [
                {
                    ""name"": ""ShootClosePortal"",
                    ""type"": ""Button"",
                    ""id"": ""3ad348a8-31d4-4ea5-bb72-5f9e18671c52"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ShootFarPortal"",
                    ""type"": ""Button"",
                    ""id"": ""75ee5a0b-159e-42c6-8ff9-b245dd9647e5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwitchControls"",
                    ""type"": ""Button"",
                    ""id"": ""fb313d78-7490-48ef-9513-e791fc0966f8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PointToTeleport"",
                    ""type"": ""Button"",
                    ""id"": ""0b800262-59d1-482b-b092-07851d4a2636"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c25b039d-f69e-4afc-bfd1-e8c507391d41"",
                    ""path"": ""<XRController>{RightHand}/primaryButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShootClosePortal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3f5aae54-decf-4fd7-aa52-08b8298f7e02"",
                    ""path"": ""<XRController>{RightHand}/secondaryButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShootFarPortal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""68368a47-237c-48f2-984e-effe51a977c7"",
                    ""path"": ""<XRController>{LeftHand}/primaryButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchControls"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4bb2e300-3765-466d-be05-5af01b52d9ea"",
                    ""path"": ""<XRController>{RightHand}/primaryButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PointToTeleport"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_ShootClosePortal = m_Player.FindAction("ShootClosePortal", throwIfNotFound: true);
        m_Player_ShootFarPortal = m_Player.FindAction("ShootFarPortal", throwIfNotFound: true);
        m_Player_SwitchControls = m_Player.FindAction("SwitchControls", throwIfNotFound: true);
        m_Player_PointToTeleport = m_Player.FindAction("PointToTeleport", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_ShootClosePortal;
    private readonly InputAction m_Player_ShootFarPortal;
    private readonly InputAction m_Player_SwitchControls;
    private readonly InputAction m_Player_PointToTeleport;
    public struct PlayerActions
    {
        private @Portalgunshoot m_Wrapper;
        public PlayerActions(@Portalgunshoot wrapper) { m_Wrapper = wrapper; }
        public InputAction @ShootClosePortal => m_Wrapper.m_Player_ShootClosePortal;
        public InputAction @ShootFarPortal => m_Wrapper.m_Player_ShootFarPortal;
        public InputAction @SwitchControls => m_Wrapper.m_Player_SwitchControls;
        public InputAction @PointToTeleport => m_Wrapper.m_Player_PointToTeleport;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @ShootClosePortal.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShootClosePortal;
                @ShootClosePortal.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShootClosePortal;
                @ShootClosePortal.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShootClosePortal;
                @ShootFarPortal.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShootFarPortal;
                @ShootFarPortal.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShootFarPortal;
                @ShootFarPortal.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShootFarPortal;
                @SwitchControls.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitchControls;
                @SwitchControls.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitchControls;
                @SwitchControls.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitchControls;
                @PointToTeleport.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPointToTeleport;
                @PointToTeleport.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPointToTeleport;
                @PointToTeleport.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPointToTeleport;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ShootClosePortal.started += instance.OnShootClosePortal;
                @ShootClosePortal.performed += instance.OnShootClosePortal;
                @ShootClosePortal.canceled += instance.OnShootClosePortal;
                @ShootFarPortal.started += instance.OnShootFarPortal;
                @ShootFarPortal.performed += instance.OnShootFarPortal;
                @ShootFarPortal.canceled += instance.OnShootFarPortal;
                @SwitchControls.started += instance.OnSwitchControls;
                @SwitchControls.performed += instance.OnSwitchControls;
                @SwitchControls.canceled += instance.OnSwitchControls;
                @PointToTeleport.started += instance.OnPointToTeleport;
                @PointToTeleport.performed += instance.OnPointToTeleport;
                @PointToTeleport.canceled += instance.OnPointToTeleport;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnShootClosePortal(InputAction.CallbackContext context);
        void OnShootFarPortal(InputAction.CallbackContext context);
        void OnSwitchControls(InputAction.CallbackContext context);
        void OnPointToTeleport(InputAction.CallbackContext context);
    }
}
