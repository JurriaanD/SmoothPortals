// GENERATED AUTOMATICALLY FROM 'Assets/portalgunshootleft.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Portalgunshootleft : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Portalgunshootleft()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""portalgunshootleft"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""c69dc0ed-a7a8-4d64-826b-ea83f677f7c7"",
            ""actions"": [
                {
                    ""name"": ""ShootPortal"",
                    ""type"": ""Button"",
                    ""id"": ""99dcc339-fb38-4d3d-b394-221ac941f7ca"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""4c1a1ef9-9d76-44d3-b274-57e6058edbf7"",
                    ""path"": ""<XRController>{LeftHand}/primaryButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShootPortal"",
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
        m_Player_ShootPortal = m_Player.FindAction("ShootPortal", throwIfNotFound: true);
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
    private readonly InputAction m_Player_ShootPortal;
    public struct PlayerActions
    {
        private @Portalgunshootleft m_Wrapper;
        public PlayerActions(@Portalgunshootleft wrapper) { m_Wrapper = wrapper; }
        public InputAction @ShootPortal => m_Wrapper.m_Player_ShootPortal;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @ShootPortal.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShootPortal;
                @ShootPortal.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShootPortal;
                @ShootPortal.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShootPortal;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ShootPortal.started += instance.OnShootPortal;
                @ShootPortal.performed += instance.OnShootPortal;
                @ShootPortal.canceled += instance.OnShootPortal;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnShootPortal(InputAction.CallbackContext context);
    }
}
