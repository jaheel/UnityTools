using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameInputSystem
{
    /// <summary>
    /// Base input class for keyboard button and controller button
    /// </summary>
    [Serializable]
    public class InputButton
    {
        [SerializeField]
        KeyCode keyCode;
        [SerializeField]
        XboxControllerButtons controllerButton;
        
        /// <value>
        /// whether the button is pressed
        /// </value>
        public bool Down { get; private set; }
        
        /// <value>
        /// whether the button is holding
        /// </value>
        public bool Held { get; private set; }
        
        /// <value>
        /// whether the button is restored
        /// </value>
        public bool Up { get; protected set; }

        bool _afterFixedUpdateDown;
        bool _afterFixedUpdateHeld;
        bool _afterFixedUpdateUp;

        [SerializeField]
        protected bool enabled = true;

        bool _gettingInput = true;

        /// <value>
        /// whether can detect Input
        /// </value>
        public bool Enabled => enabled;

        static readonly Dictionary<int,string> _buttonToName=new Dictionary<int, string>
        {
            {(int)XboxControllerButtons.A,"A"},
            {(int)XboxControllerButtons.B,"B"},
            {(int)XboxControllerButtons.X,"X"},
            {(int)XboxControllerButtons.Y,"Y"},
            {(int)XboxControllerButtons.LeftStick,"Leftstick"},
            {(int)XboxControllerButtons.RightStick,"Rightstick"},
            {(int)XboxControllerButtons.View,"View"},
            {(int)XboxControllerButtons.Menu,"Menu"},
            {(int)XboxControllerButtons.LeftBumper,"Left Bumper"},
            {(int)XboxControllerButtons.RightBumper,"Right Bumper"}
        };

        internal InputButton(KeyCode keyCode, XboxControllerButtons controllerButton)
        {
            this.keyCode = keyCode;
            this.controllerButton = controllerButton;
        }

        /// <summary>
        /// Check whether the corresponding key is pressed
        /// According to the input type and whether fixedUpdate is happened
        /// </summary>
        /// <param name="fixedUpdateHappened"></param>
        /// <param name="inputType"></param>
        internal void Get(bool fixedUpdateHappened, InputType inputType)
        {
            if (!enabled)
            {
                Down = false;
                Held = false;
                Up = false;
                return;
            }

            if (!_gettingInput)
                return;

            switch (inputType)
            {
                case InputType.Controller when fixedUpdateHappened:
                    Down = Input.GetButtonDown(_buttonToName[(int) controllerButton]);
                    Held = Input.GetButton(_buttonToName[(int) controllerButton]);
                    Up = Input.GetButtonUp(_buttonToName[(int) controllerButton]);

                    _afterFixedUpdateDown = Down;
                    _afterFixedUpdateHeld = Held;
                    _afterFixedUpdateUp = Up;
                    break;
                case InputType.Controller:
                    Down = Input.GetButtonDown(_buttonToName[(int) controllerButton]) || _afterFixedUpdateDown;
                    Held = Input.GetButton(_buttonToName[(int) controllerButton]) || _afterFixedUpdateHeld;
                    Up = Input.GetButtonUp(_buttonToName[(int) controllerButton]) || _afterFixedUpdateUp;

                    _afterFixedUpdateDown |= Down;
                    _afterFixedUpdateHeld |= Held;
                    _afterFixedUpdateUp |= Up;
                    break;
                case InputType.MouseAndKeyboard when fixedUpdateHappened:
                    Down = Input.GetKeyDown(keyCode);
                    Held = Input.GetKey(keyCode);
                    Up = Input.GetKeyUp(keyCode);

                    _afterFixedUpdateDown = Down;
                    _afterFixedUpdateHeld = Held;
                    _afterFixedUpdateUp = Up;
                    break;
                case InputType.MouseAndKeyboard:
                    Down = Input.GetKeyDown(keyCode) || _afterFixedUpdateDown;
                    Held = Input.GetKey(keyCode) || _afterFixedUpdateHeld;
                    Up = Input.GetKeyUp(keyCode) || _afterFixedUpdateUp;

                    _afterFixedUpdateDown |= Down;
                    _afterFixedUpdateHeld |= Held;
                    _afterFixedUpdateUp |= Up;
                    break;
            }
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
        /// user gain control, system will not check user Input(simple button)
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
        /// <param name="resetValues">default is true</param>
        public IEnumerator ReleaseControl(bool resetValues)
        {
            _gettingInput = false;
            if(!resetValues)
                yield break;

            if (Down)
                Up = true;
            Down = false;
            Held = false;
            _afterFixedUpdateDown = false;
            _afterFixedUpdateHeld = false;
            _afterFixedUpdateUp = false;

            yield return null;
            Up = false;
        }
    }
}