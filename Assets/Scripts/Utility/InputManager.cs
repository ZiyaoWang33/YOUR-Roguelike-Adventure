using UnityEngine;
using System.Collections.Generic;
using System;

public class InputManager : Singleton<InputManager>
{
    private Dictionary<string, KeyCode> Buttons = new Dictionary<string, KeyCode>
    {
        ["Forward"] = KeyCode.W,
        ["Back"] = KeyCode.S,
        ["Left"] = KeyCode.A,
        ["Right"] = KeyCode.D,
        ["Shoot"] = KeyCode.Mouse0,
        ["Special"] = KeyCode.Space
    };
    [SerializeField] private float axisIncrement = 0.1f;
    private float horizontal = 0;
    private float vertical = 1;

    public void ChangeBinding(string button, KeyCode value, InputChanger changer)
    {
        if (changer)
        {
            Buttons[button] = value;
        }
    }

    private void TestKey(string key)
    {
        if (!Buttons.ContainsKey(key))
        {
            throw new Exception("Button not found.");
        }
    }

    public bool GetButtonDown(string button)
    {
        TestKey(button);
        return Input.GetKeyDown(Buttons[button]);
    }

    public bool GetButton(string button)
    {
        TestKey(button);
        return Input.GetKey(Buttons[button]);
    }

    public bool GetButtonUp(string button)
    {
        TestKey(button);
        return Input.GetKeyUp(Buttons[button]);
    }

    public float GetAxis(string axis)
    {
        if (axis.Equals("Horizontal"))
        {
            if (Input.GetKey(Buttons["Left"]))
            {
                horizontal = horizontal <= -1 ? horizontal : horizontal - axisIncrement;
            }
            else if (Input.GetKey(Buttons["Right"]))
            {
                horizontal = horizontal >= 1 ? horizontal : horizontal + axisIncrement;
            }
            else
            {
                horizontal = 0;
            }
        }
        else if (axis.Equals("Vertical"))
        {
            if (Input.GetKey(Buttons["Back"]))
            {
                vertical = vertical <= -1 ? vertical : vertical - axisIncrement;
            }
            else if (Input.GetKey(Buttons["Forward"]))
            {
                vertical = vertical >= 1 ? vertical : vertical + axisIncrement;
            }
            else
            {
                vertical = 0;
            }
        }

        return axis.Equals("Horizontal") ? horizontal : vertical;
    }
}
