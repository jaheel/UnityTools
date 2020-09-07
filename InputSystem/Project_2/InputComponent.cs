using UnityEngine;

namespace GameInputSystem
{
    public abstract class InputComponent : MonoBehaviour
    {
        InputType _userInputType = InputType.MouseAndKeyboard;
        protected InputType UserInputType => _userInputType;
        
        bool _fixedUpdateHappened;
        
        
        void Update()
        {
            GetInputs(_fixedUpdateHappened||Mathf.Approximately(Time.timeScale,0));
            _fixedUpdateHappened = false;
        }

        void FixedUpdate()
        {
            _fixedUpdateHappened = true;
        }


        protected abstract void GetInputs(bool fixedUpdateHappened);

        public abstract void GainControl();
        public abstract void ReleaseControl(bool resetValues = true);

        protected void GainControl(InputButton inputButton)
        {
            inputButton.GainControl();
        }

        protected void GainControl(InputAxis inputAxis)
        {
            inputAxis.GainControl();
        }

        protected void ReleaseControl(InputButton inputButton, bool resetValues)
        {
            StartCoroutine(inputButton.ReleaseControl(resetValues));
        }

        protected void ReleaseControl(InputAxis inputAxis, bool resetValues)
        {
            inputAxis.ReleaseControl(resetValues);
        }
    }

}

