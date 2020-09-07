using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using UnityEngine;
    public class InputMapper:MonoBehaviour
    {
        public const string UP = "Up";
        public const string DOWN = "Down";
        public const string LEFT = "Left";
        public const string RIGHT = "Right";

        static Dictionary<string, InputDetector> _defaultKeyboardMap;
        static Dictionary<string, InputDetector> _defaultJoystickMap;

        public delegate void OnPressed();

        public delegate void OnHeld();

        public delegate void OnReleased();

        bool _isInControl=true;
        Dictionary<string, OnPressed> _onPressedBindings;
        Dictionary<string, OnHeld> _onHeldBindings;
        Dictionary<string, OnReleased> _onReleasedBindings;
        Dictionary<string, InputDetector> _inputMap;

        public int Count => _inputMap.Count;

        void Awake()
        {
            _defaultKeyboardMap = new Dictionary<string, InputDetector>(4)
            {
                [UP] = KeyDetector.W, [DOWN] = KeyDetector.S, [LEFT] = KeyDetector.A, [RIGHT] = KeyDetector.D
            };

            //defaultJoystickMap=new Dictionary<string, InputDetector>(4);
            //defaultJoystickMap[THROW_UP] = AxisDetector.Axis2ndPositive;
            //defaultJoystickMap[THROW_DOWN]=AxisDetector.Axis2ndNegative;
            //defaultKeyboardMap[THROW_LEFT]=AxisDetector.Axis1stPositive;
            //defaultKeyboardMap[THROW_RIGHT]=AxisDetector.Axis1stNegative;
            
            _inputMap=new Dictionary<string, InputDetector>(4);
            _onPressedBindings=new Dictionary<string, OnPressed>(4);
            _onHeldBindings=new Dictionary<string, OnHeld>(4);
            _onReleasedBindings=new Dictionary<string, OnReleased>(4);

            Reset();
        }

        void Update()
        {
            Refresh();
            
            //可以以后打开UI时禁止部分操作
            if (!_isInControl) return;
            
            if (_onPressedBindings.Count > 0)
            {
                foreach (var pressBindingName in _onPressedBindings.Keys.Where(pressBindingName => _inputMap[pressBindingName].IsPressed))
                {
                    _onPressedBindings[pressBindingName]();
                }
            }

            if (_onHeldBindings.Count > 0)
            {
                foreach (var heldBindingName in _onHeldBindings.Keys.Where(heldBindingName => _inputMap[heldBindingName].IsHeld))
                {
                    _onHeldBindings[heldBindingName]();
                }
            }

            if (_onReleasedBindings.Count > 0)
            {
                foreach (var releaseBindingName in _onReleasedBindings.Keys)
                {
                    if (_inputMap[releaseBindingName].IsReleased)
                    {
                        _onReleasedBindings[releaseBindingName]();
                    }
                }
            }
        }

        void Refresh()
        {
            foreach (var detector in _inputMap.Values.TakeWhile(detector => detector != null))
            {
                detector.Refresh();
            }
        }
        /// <summary>
        /// 重置为
        /// </summary>
        public void Reset()
        {
            _inputMap.Clear();
            foreach (var pair in _defaultKeyboardMap)
            {
                Remap(pair.Key, pair.Value);
            }
        }

        /// <summary>
        /// 重新映射
        /// </summary>
        /// <param name="detectorName"></param>
        /// <param name="detector"></param>
        void Remap(string detectorName, InputDetector detector)
        {
            if (_inputMap.ContainsKey(detectorName))
            {
                //var unityException = new UnityException("Already Contains Input Named["+detectorName+"]");
                Debug.Log("Already Contains Input Named["+detectorName+"]");
                return;
            }
            
            _inputMap[detectorName] = detector;
        }

        public bool BindPressEvent(string pressName, OnPressed e)
        {
            var bound = _onPressedBindings.ContainsKey(pressName);
            if(!bound)
                _onPressedBindings[pressName] = e;
            return bound;
        }

        public bool BindHoldEvent(string holdName, OnHeld e)
        {
            var bound = _onHeldBindings.ContainsKey(holdName);
            if(!bound)
                _onHeldBindings[holdName] = e;
            return bound;
        }

        public bool BindReleaseEvent(string releaseName, OnReleased e)
        {
            var bound = _onReleasedBindings.ContainsKey(releaseName);
            if(!bound)
                _onReleasedBindings[releaseName] = e;
            return bound;
        }

        public bool UnbindPressEvent(string pressName)
        {
            if (!_onPressedBindings.ContainsKey(pressName))
            {
                return false;
            }

            _onPressedBindings.Remove(pressName);
            return true;
        }

        public bool UnbindHoldEvent(string holdName)
        {
            if (!_onHeldBindings.ContainsKey(holdName))
            {
                return false;
            }

            _onHeldBindings.Remove(holdName);
            return true;
        }

        public bool UnbindReleaseEvent(string releaseName)
        {
            if (!_onReleasedBindings.ContainsKey(releaseName))
            {
                return false;
            }

            _onReleasedBindings.Remove(releaseName);
            return true;
        }

        public OnPressed GetPressEvent(string pressName)
        {
            return _onPressedBindings.ContainsKey(pressName) ? _onPressedBindings[pressName] : null;
        }

        public OnHeld GetHeldEvent(string heldName)
        {
            return _onHeldBindings.ContainsKey(heldName) ? _onHeldBindings[heldName] : null;
        }

        public OnReleased GetReleasedEvent(string releaseName)
        {
            return _onReleasedBindings.ContainsKey(releaseName) ? _onReleasedBindings[releaseName] : null;
        }

        public InputDetector this[string detectorName]
        {
            get
            {
                if (_inputMap.ContainsKey(detectorName)) return _inputMap[detectorName];
                //var unityException = new UnityException("Cannot Find input named [" + detectorName + "]");
                Debug.Log("Cannot Find input named [" + detectorName + "]");
                return null;

            }
            set => Remap(detectorName,value);
        }
        
    }
