using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameInputSystem
{
    /// <summary>
    /// Base input class for handle axis
    /// </summary>
    [Serializable]
    public class InputAxis
    {
        [SerializeField]
        KeyCode positive;
        [SerializeField]
        KeyCode negative;
        [SerializeField]
        XboxControllerAxes controllerAxis;
        
        /// <value>
        /// Input direction
        ///  1 : positive
        /// -1 : negative
        /// </value>
        internal float Value { get; private set; }
        
        /// <value>
        /// Check if Input is being received
        /// </value>
        internal bool ReceivingInput { get; private set; }
        [SerializeField]
        bool enabled = true;

        /// <value>
        /// whether can detect Input
        /// </value>
        public bool Enabled => enabled;

        bool _gettingInput = true;

        static readonly Dictionary<int,string> _axisToName=new Dictionary<int,string>
        {
            {(int)XboxControllerAxes.LeftStickHorizontal,"Leftstick Horizontal"},
            {(int)XboxControllerAxes.LeftStickVertical,"Leftstick Vertical"},
            {(int)XboxControllerAxes.DpadHorizontal,"Dpad Horizontal"},
            {(int)XboxControllerAxes.DpadVertical,"Dpad Vertical"},
            {(int)XboxControllerAxes.RightStickHorizontal,"Rightstick Horizontal"},
            {(int)XboxControllerAxes.RightStickVertical,"Rightstick Vertical"},
            {(int)XboxControllerAxes.LeftTrigger,"Left Trigger"},
            {(int)XboxControllerAxes.RightTrigger,"Right Trigger"}
        };

        internal InputAxis(KeyCode positive, KeyCode negative, XboxControllerAxes controllerAxis)
        {
            this.positive = positive;
            this.negative = negative;
            this.controllerAxis = controllerAxis;
        }

        /// <summary>
        /// Check whether the corresponding key is pressed
        /// According to the input type
        /// </summary>
        /// <see cref="InputType"/>
        /// <param name="inputType">Input type</param>
        internal void Get(InputType inputType)
        {
            if (!enabled)
            {
                Value = 0f;
                return;
            }

            if (!_gettingInput)
                return;
            bool positiveHeld = false;
            bool negativeHeld = false;
            if (inputType == InputType.Controller)
            {
                float value = Input.GetAxisRaw(_axisToName[(int) controllerAxis]);
                positiveHeld = value > Single.Epsilon;
                negativeHeld = value < -Single.Epsilon;
            }
            else if (inputType == InputType.MouseAndKeyboard)
            {
                positiveHeld = Input.GetKey(positive);
                negativeHeld = Input.GetKey(negative);
            }

            if (positiveHeld == negativeHeld)
                Value = 0f;
            else if (positiveHeld)
                Value = 1f;
            else
            {
                Value = -1f;
            }
            ReceivingInput = positiveHeld || negativeHeld;
        }

        /// <summary>
        /// enable to check whether the key is pressed
        /// </summary>
        public void Enable()
        {
            enabled = true;
        }

        /// <summary>
        /// disable to check whether the key is pressed
        /// </summary>
        public void Disable()
        {
            enabled = false;
        }

        /// <summary>
        /// user gain control, system will not check user Input
        /// </summary>
        /// <example>Monsters use skills to control users,
        /// user Input can not be check and response</example>
        public void GainControl()
        {
            _gettingInput = true;
        }

        /// <summary>
        /// release control, user input can be recheck 
        /// </summary>
        /// <example>control time passed, user input can be recheck and response</example>
        /// <param name="resetValues"></param>
        public void ReleaseControl(bool resetValues)
        {
            _gettingInput = false;
            if (resetValues)
            {
                Value = 0f;
                ReceivingInput = false;
            }
        }
    }
}