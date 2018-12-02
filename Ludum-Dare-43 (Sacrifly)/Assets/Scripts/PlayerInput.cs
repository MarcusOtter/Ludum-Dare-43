using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    internal static event EventHandler OnWDown;
    internal static event EventHandler OnADown;
    internal static event EventHandler OnSDown;
    internal static event EventHandler OnDDown;
    internal static event EventHandler OnSpaceDown;
    internal static float HorizontalInput { get; private set; }
    
    private void Update()
    {
        HorizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.W)) OnWDown?.Invoke(this, EventArgs.Empty);
        if (Input.GetKeyDown(KeyCode.A)) OnADown?.Invoke(this, EventArgs.Empty);
        if (Input.GetKeyDown(KeyCode.S)) OnSDown?.Invoke(this, EventArgs.Empty);
        if (Input.GetKeyDown(KeyCode.D)) OnDDown?.Invoke(this, EventArgs.Empty);
        if (Input.GetKeyDown(KeyCode.Space)) OnSpaceDown?.Invoke(this, EventArgs.Empty);
    }
}
