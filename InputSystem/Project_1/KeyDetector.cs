﻿    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public sealed class KeyDetector:InputDetector
    {
	    
	    

	    public override int GetHashCode()
	    {
		    return (int) _keyCode;
	    }

	    static readonly Dictionary<string, KeyDetector> _keyDetectors;
        readonly KeyCode _keyCode;

        KeyDetector(string name)
        {
            _name = name;
            _keyCode = (KeyCode) Enum.Parse(typeof(KeyCode),name);
        }

        KeyDetector(KeyCode keyCode)
        {
	        
            _name = keyCode.ToString();
            _keyCode = keyCode;
        }
        
        public override void Refresh()
        {
            _isPressed = false;
            _isReleased = false;
            if (Input.GetKeyDown(_keyCode))
            {
                _isPressed = true;
                _isHeld = true;
                _lastPressTime = Time.time;
            }
            else if (Input.GetKeyUp(_keyCode))
            {
                _isReleased = true;
                _isHeld = false;
            }
        }

        public static KeyDetector ToKeyDetector(string name)
        {
            return _keyDetectors.ContainsKey(name) ? _keyDetectors[name] : null;
        }

        public static bool operator ==(KeyDetector lhs, KeyDetector rhs)
        {
            Debug.Assert(lhs != null, nameof(lhs) + " != null");
            Debug.Assert(rhs != null, nameof(rhs) + " != null");
            return lhs._keyCode == rhs._keyCode;
        }

        public static bool operator !=(KeyDetector lhs, KeyDetector rhs)
        {
            Debug.Assert(lhs != null, nameof(lhs) + " != null");
            Debug.Assert(rhs != null, nameof(rhs) + " != null");
            return lhs._keyCode != rhs._keyCode;
        }

        #region 
			public static readonly KeyDetector A = new KeyDetector(KeyCode.A);
			public static readonly KeyDetector B = new KeyDetector(KeyCode.B);
			public static readonly KeyDetector C = new KeyDetector(KeyCode.C);
			public static readonly KeyDetector D = new KeyDetector(KeyCode.D);
			public static readonly KeyDetector E = new KeyDetector(KeyCode.E);
			public static readonly KeyDetector F = new KeyDetector(KeyCode.F);
			public static readonly KeyDetector G = new KeyDetector(KeyCode.G);
			public static readonly KeyDetector H = new KeyDetector(KeyCode.H);
			public static readonly KeyDetector I = new KeyDetector(KeyCode.I);
			public static readonly KeyDetector J = new KeyDetector(KeyCode.J);
			public static readonly KeyDetector K = new KeyDetector(KeyCode.K);
			public static readonly KeyDetector L = new KeyDetector(KeyCode.L);
			public static readonly KeyDetector M = new KeyDetector(KeyCode.M);
			public static readonly KeyDetector N = new KeyDetector(KeyCode.N);
			public static readonly KeyDetector O = new KeyDetector(KeyCode.O);
			public static readonly KeyDetector P = new KeyDetector(KeyCode.P);
			public static readonly KeyDetector Q = new KeyDetector(KeyCode.Q);
			public static readonly KeyDetector R = new KeyDetector(KeyCode.R);
			public static readonly KeyDetector S = new KeyDetector(KeyCode.S);
			public static readonly KeyDetector T = new KeyDetector(KeyCode.T);
			public static readonly KeyDetector U = new KeyDetector(KeyCode.U);
			public static readonly KeyDetector V = new KeyDetector(KeyCode.V);
			public static readonly KeyDetector W = new KeyDetector(KeyCode.W);
			public static readonly KeyDetector X = new KeyDetector(KeyCode.X);
			public static readonly KeyDetector Y = new KeyDetector(KeyCode.Y);
			public static readonly KeyDetector Z = new KeyDetector(KeyCode.Z);
			public static readonly KeyDetector F1 = new KeyDetector(KeyCode.F1);
			public static readonly KeyDetector F2 = new KeyDetector(KeyCode.F2);
			public static readonly KeyDetector F3 = new KeyDetector(KeyCode.F3);
			public static readonly KeyDetector F4 = new KeyDetector(KeyCode.F4);
			public static readonly KeyDetector F5 = new KeyDetector(KeyCode.F5);
			public static readonly KeyDetector F6 = new KeyDetector(KeyCode.F6);
			public static readonly KeyDetector F7 = new KeyDetector(KeyCode.F7);
			public static readonly KeyDetector F8 = new KeyDetector(KeyCode.F8);
			public static readonly KeyDetector F9 = new KeyDetector(KeyCode.F9);
			public static readonly KeyDetector F10 = new KeyDetector(KeyCode.F10);
			public static readonly KeyDetector F11 = new KeyDetector(KeyCode.F11);
			public static readonly KeyDetector F12 = new KeyDetector(KeyCode.F12);
			public static readonly KeyDetector F13 = new KeyDetector(KeyCode.F13);
			public static readonly KeyDetector F14 = new KeyDetector(KeyCode.F14);
			public static readonly KeyDetector F15 = new KeyDetector(KeyCode.F15);
			public static readonly KeyDetector Alpha0 = new KeyDetector(KeyCode.Alpha0);
			public static readonly KeyDetector Alpha1 = new KeyDetector(KeyCode.Alpha1);
			public static readonly KeyDetector Alpha2 = new KeyDetector(KeyCode.Alpha2);
			public static readonly KeyDetector Alpha3 = new KeyDetector(KeyCode.Alpha3);
			public static readonly KeyDetector Alpha4 = new KeyDetector(KeyCode.Alpha4);
			public static readonly KeyDetector Alpha5 = new KeyDetector(KeyCode.Alpha5);
			public static readonly KeyDetector Alpha6 = new KeyDetector(KeyCode.Alpha6);
			public static readonly KeyDetector Alpha7 = new KeyDetector(KeyCode.Alpha7);
			public static readonly KeyDetector Alpha8 = new KeyDetector(KeyCode.Alpha8);
			public static readonly KeyDetector Alpha9 = new KeyDetector(KeyCode.Alpha9);
			public static readonly KeyDetector UpArrow = new KeyDetector(KeyCode.UpArrow);
			public static readonly KeyDetector DownArrow = new KeyDetector(KeyCode.DownArrow);
			public static readonly KeyDetector LeftArrow = new KeyDetector(KeyCode.LeftArrow);
			public static readonly KeyDetector RightArrow = new KeyDetector(KeyCode.RightArrow);
			public static readonly KeyDetector LeftControl = new KeyDetector(KeyCode.LeftControl);
			public static readonly KeyDetector RightControl = new KeyDetector(KeyCode.RightControl);
			public static readonly KeyDetector LeftShift = new KeyDetector(KeyCode.LeftShift);
			public static readonly KeyDetector RightShift = new KeyDetector(KeyCode.RightShift);
			public static readonly KeyDetector LeftCommand = new KeyDetector(KeyCode.LeftCommand);
			public static readonly KeyDetector RightCommand = new KeyDetector(KeyCode.RightCommand);
			public static readonly KeyDetector LeftApple = new KeyDetector(KeyCode.LeftApple);
			public static readonly KeyDetector RightApple = new KeyDetector(KeyCode.RightApple);
			public static readonly KeyDetector LeftWindows = new KeyDetector(KeyCode.LeftWindows);
			public static readonly KeyDetector RightWindows = new KeyDetector(KeyCode.RightWindows);
			public static readonly KeyDetector LeftAlt = new KeyDetector(KeyCode.LeftAlt);
			public static readonly KeyDetector RightAlt = new KeyDetector(KeyCode.RightAlt);
			public static readonly KeyDetector LeftBracket = new KeyDetector(KeyCode.LeftBracket);
			public static readonly KeyDetector RightBracket = new KeyDetector(KeyCode.RightBracket);
			public static readonly KeyDetector Escape = new KeyDetector(KeyCode.Escape);
			public static readonly KeyDetector BackQuote = new KeyDetector(KeyCode.BackQuote);
			public static readonly KeyDetector Backslash = new KeyDetector(KeyCode.Backslash);
			public static readonly KeyDetector Minus = new KeyDetector(KeyCode.Minus);
			public static readonly KeyDetector Equals = new KeyDetector(KeyCode.Equals);
			public static readonly KeyDetector Comma = new KeyDetector(KeyCode.Comma);
			public static readonly KeyDetector Period = new KeyDetector(KeyCode.Period);
			public static readonly KeyDetector Slash = new KeyDetector(KeyCode.Slash);
			public static readonly KeyDetector Backspace = new KeyDetector(KeyCode.Backspace);
			public static readonly KeyDetector Tab = new KeyDetector(KeyCode.Tab);
			public static readonly KeyDetector Space = new KeyDetector(KeyCode.Space);
			public static readonly KeyDetector Semicolon = new KeyDetector(KeyCode.Semicolon);
			public static readonly KeyDetector Quote = new KeyDetector(KeyCode.Quote);
			public static readonly KeyDetector Return = new KeyDetector(KeyCode.Return);
			public static readonly KeyDetector CapsLock = new KeyDetector(KeyCode.CapsLock);
			public static readonly KeyDetector JoystickButton0 = new KeyDetector(KeyCode.JoystickButton0);
			public static readonly KeyDetector JoystickButton1 = new KeyDetector(KeyCode.JoystickButton1);
			public static readonly KeyDetector JoystickButton2 = new KeyDetector(KeyCode.JoystickButton2);
			public static readonly KeyDetector JoystickButton3 = new KeyDetector(KeyCode.JoystickButton3);
			public static readonly KeyDetector JoystickButton4 = new KeyDetector(KeyCode.JoystickButton4);
			public static readonly KeyDetector JoystickButton5 = new KeyDetector(KeyCode.JoystickButton5);
			public static readonly KeyDetector JoystickButton6 = new KeyDetector(KeyCode.JoystickButton6);
			public static readonly KeyDetector JoystickButton7 = new KeyDetector(KeyCode.JoystickButton7);
			public static readonly KeyDetector JoystickButton8 = new KeyDetector(KeyCode.JoystickButton8);
			public static readonly KeyDetector JoystickButton9 = new KeyDetector(KeyCode.JoystickButton9);
			public static readonly KeyDetector JoystickButton10 = new KeyDetector(KeyCode.JoystickButton10);
			public static readonly KeyDetector JoystickButton11 = new KeyDetector(KeyCode.JoystickButton11);
			public static readonly KeyDetector JoystickButton12 = new KeyDetector(KeyCode.JoystickButton12);
			public static readonly KeyDetector JoystickButton13 = new KeyDetector(KeyCode.JoystickButton13);
			public static readonly KeyDetector JoystickButton14 = new KeyDetector(KeyCode.JoystickButton14);
			public static readonly KeyDetector JoystickButton15 = new KeyDetector(KeyCode.JoystickButton15);
			public static readonly KeyDetector JoystickButton16 = new KeyDetector(KeyCode.JoystickButton16);
			public static readonly KeyDetector JoystickButton17 = new KeyDetector(KeyCode.JoystickButton17);
			public static readonly KeyDetector JoystickButton18 = new KeyDetector(KeyCode.JoystickButton18);
			public static readonly KeyDetector JoystickButton19 = new KeyDetector(KeyCode.JoystickButton19);
			public static readonly KeyDetector Joystick1Button0 = new KeyDetector(KeyCode.Joystick1Button0);
			public static readonly KeyDetector Joystick1Button1 = new KeyDetector(KeyCode.Joystick1Button1);
			public static readonly KeyDetector Joystick1Button2 = new KeyDetector(KeyCode.Joystick1Button2);
			public static readonly KeyDetector Joystick1Button3 = new KeyDetector(KeyCode.Joystick1Button3);
			public static readonly KeyDetector Joystick1Button4 = new KeyDetector(KeyCode.Joystick1Button4);
			public static readonly KeyDetector Joystick1Button5 = new KeyDetector(KeyCode.Joystick1Button5);
			public static readonly KeyDetector Joystick1Button6 = new KeyDetector(KeyCode.Joystick1Button6);
			public static readonly KeyDetector Joystick1Button7 = new KeyDetector(KeyCode.Joystick1Button7);
			public static readonly KeyDetector Joystick1Button8 = new KeyDetector(KeyCode.Joystick1Button8);
			public static readonly KeyDetector Joystick1Button9 = new KeyDetector(KeyCode.Joystick1Button9);
			public static readonly KeyDetector Joystick1Button10 = new KeyDetector(KeyCode.Joystick1Button10);
			public static readonly KeyDetector Joystick1Button11 = new KeyDetector(KeyCode.Joystick1Button11);
			public static readonly KeyDetector Joystick1Button12 = new KeyDetector(KeyCode.Joystick1Button12);
			public static readonly KeyDetector Joystick1Button13 = new KeyDetector(KeyCode.Joystick1Button13);
			public static readonly KeyDetector Joystick1Button14 = new KeyDetector(KeyCode.Joystick1Button14);
			public static readonly KeyDetector Joystick1Button15 = new KeyDetector(KeyCode.Joystick1Button15);
			public static readonly KeyDetector Joystick1Button16 = new KeyDetector(KeyCode.Joystick1Button16);
			public static readonly KeyDetector Joystick1Button17 = new KeyDetector(KeyCode.Joystick1Button17);
			public static readonly KeyDetector Joystick1Button18 = new KeyDetector(KeyCode.Joystick1Button18);
			public static readonly KeyDetector Joystick1Button19 = new KeyDetector(KeyCode.Joystick1Button19);
			public static readonly KeyDetector Joystick2Button0 = new KeyDetector(KeyCode.Joystick2Button0);
			public static readonly KeyDetector Joystick2Button1 = new KeyDetector(KeyCode.Joystick2Button1);
			public static readonly KeyDetector Joystick2Button2 = new KeyDetector(KeyCode.Joystick2Button2);
			public static readonly KeyDetector Joystick2Button3 = new KeyDetector(KeyCode.Joystick2Button3);
			public static readonly KeyDetector Joystick2Button4 = new KeyDetector(KeyCode.Joystick2Button4);
			public static readonly KeyDetector Joystick2Button5 = new KeyDetector(KeyCode.Joystick2Button5);
			public static readonly KeyDetector Joystick2Button6 = new KeyDetector(KeyCode.Joystick2Button6);
			public static readonly KeyDetector Joystick2Button7 = new KeyDetector(KeyCode.Joystick2Button7);
			public static readonly KeyDetector Joystick2Button8 = new KeyDetector(KeyCode.Joystick2Button8);
			public static readonly KeyDetector Joystick2Button9 = new KeyDetector(KeyCode.Joystick2Button9);
			public static readonly KeyDetector Joystick2Button10 = new KeyDetector(KeyCode.Joystick2Button10);
			public static readonly KeyDetector Joystick2Button11 = new KeyDetector(KeyCode.Joystick2Button11);
			public static readonly KeyDetector Joystick2Button12 = new KeyDetector(KeyCode.Joystick2Button12);
			public static readonly KeyDetector Joystick2Button13 = new KeyDetector(KeyCode.Joystick2Button13);
			public static readonly KeyDetector Joystick2Button14 = new KeyDetector(KeyCode.Joystick2Button14);
			public static readonly KeyDetector Joystick2Button15 = new KeyDetector(KeyCode.Joystick2Button15);
			public static readonly KeyDetector Joystick2Button16 = new KeyDetector(KeyCode.Joystick2Button16);
			public static readonly KeyDetector Joystick2Button17 = new KeyDetector(KeyCode.Joystick2Button17);
			public static readonly KeyDetector Joystick2Button18 = new KeyDetector(KeyCode.Joystick2Button18);
			public static readonly KeyDetector Joystick2Button19 = new KeyDetector(KeyCode.Joystick2Button19);
			public static readonly KeyDetector Joystick3Button0 = new KeyDetector(KeyCode.Joystick3Button0);
			public static readonly KeyDetector Joystick3Button1 = new KeyDetector(KeyCode.Joystick3Button1);
			public static readonly KeyDetector Joystick3Button2 = new KeyDetector(KeyCode.Joystick3Button2);
			public static readonly KeyDetector Joystick3Button3 = new KeyDetector(KeyCode.Joystick3Button3);
			public static readonly KeyDetector Joystick3Button4 = new KeyDetector(KeyCode.Joystick3Button4);
			public static readonly KeyDetector Joystick3Button5 = new KeyDetector(KeyCode.Joystick3Button5);
			public static readonly KeyDetector Joystick3Button6 = new KeyDetector(KeyCode.Joystick3Button6);
			public static readonly KeyDetector Joystick3Button7 = new KeyDetector(KeyCode.Joystick3Button7);
			public static readonly KeyDetector Joystick3Button8 = new KeyDetector(KeyCode.Joystick3Button8);
			public static readonly KeyDetector Joystick3Button9 = new KeyDetector(KeyCode.Joystick3Button9);
			public static readonly KeyDetector Joystick3Button10 = new KeyDetector(KeyCode.Joystick3Button10);
			public static readonly KeyDetector Joystick3Button11 = new KeyDetector(KeyCode.Joystick3Button11);
			public static readonly KeyDetector Joystick3Button12 = new KeyDetector(KeyCode.Joystick3Button12);
			public static readonly KeyDetector Joystick3Button13 = new KeyDetector(KeyCode.Joystick3Button13);
			public static readonly KeyDetector Joystick3Button14 = new KeyDetector(KeyCode.Joystick3Button14);
			public static readonly KeyDetector Joystick3Button15 = new KeyDetector(KeyCode.Joystick3Button15);
			public static readonly KeyDetector Joystick3Button16 = new KeyDetector(KeyCode.Joystick3Button16);
			public static readonly KeyDetector Joystick3Button17 = new KeyDetector(KeyCode.Joystick3Button17);
			public static readonly KeyDetector Joystick3Button18 = new KeyDetector(KeyCode.Joystick3Button18);
			public static readonly KeyDetector Joystick3Button19 = new KeyDetector(KeyCode.Joystick3Button19);
			public static readonly KeyDetector Joystick4Button0 = new KeyDetector(KeyCode.Joystick4Button0);
			public static readonly KeyDetector Joystick4Button1 = new KeyDetector(KeyCode.Joystick4Button1);
			public static readonly KeyDetector Joystick4Button2 = new KeyDetector(KeyCode.Joystick4Button2);
			public static readonly KeyDetector Joystick4Button3 = new KeyDetector(KeyCode.Joystick4Button3);
			public static readonly KeyDetector Joystick4Button4 = new KeyDetector(KeyCode.Joystick4Button4);
			public static readonly KeyDetector Joystick4Button5 = new KeyDetector(KeyCode.Joystick4Button5);
			public static readonly KeyDetector Joystick4Button6 = new KeyDetector(KeyCode.Joystick4Button6);
			public static readonly KeyDetector Joystick4Button7 = new KeyDetector(KeyCode.Joystick4Button7);
			public static readonly KeyDetector Joystick4Button8 = new KeyDetector(KeyCode.Joystick4Button8);
			public static readonly KeyDetector Joystick4Button9 = new KeyDetector(KeyCode.Joystick4Button9);
			public static readonly KeyDetector Joystick4Button10 = new KeyDetector(KeyCode.Joystick4Button10);
			public static readonly KeyDetector Joystick4Button11 = new KeyDetector(KeyCode.Joystick4Button11);
			public static readonly KeyDetector Joystick4Button12 = new KeyDetector(KeyCode.Joystick4Button12);
			public static readonly KeyDetector Joystick4Button13 = new KeyDetector(KeyCode.Joystick4Button13);
			public static readonly KeyDetector Joystick4Button14 = new KeyDetector(KeyCode.Joystick4Button14);
			public static readonly KeyDetector Joystick4Button15 = new KeyDetector(KeyCode.Joystick4Button15);
			public static readonly KeyDetector Joystick4Button16 = new KeyDetector(KeyCode.Joystick4Button16);
			public static readonly KeyDetector Joystick4Button17 = new KeyDetector(KeyCode.Joystick4Button17);
			public static readonly KeyDetector Joystick4Button18 = new KeyDetector(KeyCode.Joystick4Button18);
			public static readonly KeyDetector Joystick4Button19 = new KeyDetector(KeyCode.Joystick4Button19);
			#endregion

			#region 
		static KeyDetector() {
		_keyDetectors = new Dictionary<string, KeyDetector>(184);
		_keyDetectors["A"] = A;
		_keyDetectors["B"] = B;
		_keyDetectors["C"] = C;
		_keyDetectors["D"] = D;
		_keyDetectors["E"] = E;
		_keyDetectors["F"] = F;
		_keyDetectors["G"] = G;
		_keyDetectors["H"] = H;
		_keyDetectors["I"] = I;
		_keyDetectors["J"] = J;
		_keyDetectors["K"] = K;
		_keyDetectors["L"] = L;
		_keyDetectors["M"] = M;
		_keyDetectors["N"] = N;
		_keyDetectors["O"] = O;
		_keyDetectors["P"] = P;
		_keyDetectors["Q"] = Q;
		_keyDetectors["R"] = R;
		_keyDetectors["S"] = S;
		_keyDetectors["T"] = T;
		_keyDetectors["U"] = U;
		_keyDetectors["V"] = V;
		_keyDetectors["W"] = W;
		_keyDetectors["X"] = X;
		_keyDetectors["Y"] = Y;
		_keyDetectors["Z"] = Z;
		_keyDetectors["F1"] = F1;
		_keyDetectors["F2"] = F2;
		_keyDetectors["F3"] = F3;
		_keyDetectors["F4"] = F4;
		_keyDetectors["F5"] = F5;
		_keyDetectors["F6"] = F6;
		_keyDetectors["F7"] = F7;
		_keyDetectors["F8"] = F8;
		_keyDetectors["F9"] = F9;
		_keyDetectors["F10"] = F10;
		_keyDetectors["F11"] = F11;
		_keyDetectors["F12"] = F12;
		_keyDetectors["F13"] = F13;
		_keyDetectors["F14"] = F14;
		_keyDetectors["F15"] = F15;
		_keyDetectors["Alpha0"] = Alpha0;
		_keyDetectors["Alpha1"] = Alpha1;
		_keyDetectors["Alpha2"] = Alpha2;
		_keyDetectors["Alpha3"] = Alpha3;
		_keyDetectors["Alpha4"] = Alpha4;
		_keyDetectors["Alpha5"] = Alpha5;
		_keyDetectors["Alpha6"] = Alpha6;
		_keyDetectors["Alpha7"] = Alpha7;
		_keyDetectors["Alpha8"] = Alpha8;
		_keyDetectors["Alpha9"] = Alpha9;
		_keyDetectors["UpArrow"] = UpArrow;
		_keyDetectors["DownArrow"] = DownArrow;
		_keyDetectors["LeftArrow"] = LeftArrow;
		_keyDetectors["RightArrow"] = RightArrow;
		_keyDetectors["LeftControl"] = LeftControl;
		_keyDetectors["RightControl"] = RightControl;
		_keyDetectors["LeftShift"] = LeftShift;
		_keyDetectors["RightShift"] = RightShift;
		_keyDetectors["LeftCommand"] = LeftCommand;
		_keyDetectors["RightCommand"] = RightCommand;
		_keyDetectors["LeftApple"] = LeftApple;
		_keyDetectors["RightApple"] = RightApple;
		_keyDetectors["LeftWindows"] = LeftWindows;
		_keyDetectors["RightWindows"] = RightWindows;
		_keyDetectors["LeftAlt"] = LeftAlt;
		_keyDetectors["RightAlt"] = RightAlt;
		_keyDetectors["LeftBracket"] = LeftBracket;
		_keyDetectors["RightBracket"] = RightBracket;
		_keyDetectors["Escape"] = Escape;
		_keyDetectors["BackQuote"] = BackQuote;
		_keyDetectors["Backslash"] = Backslash;
		_keyDetectors["Minus"] = Minus;
		_keyDetectors["Equals"] = Equals;
		_keyDetectors["Comma"] = Comma;
		_keyDetectors["Period"] = Period;
		_keyDetectors["Slash"] = Slash;
		_keyDetectors["Backspace"] = Backspace;
		_keyDetectors["Tab"] = Tab;
		_keyDetectors["Space"] = Space;
		_keyDetectors["Semicolon"] = Semicolon;
		_keyDetectors["Quote"] = Quote;
		_keyDetectors["Return"] = Return;
		_keyDetectors["CapsLock"] = CapsLock;
		_keyDetectors["JoystickButton0"] = JoystickButton0;
		_keyDetectors["JoystickButton1"] = JoystickButton1;
		_keyDetectors["JoystickButton2"] = JoystickButton2;
		_keyDetectors["JoystickButton3"] = JoystickButton3;
		_keyDetectors["JoystickButton4"] = JoystickButton4;
		_keyDetectors["JoystickButton5"] = JoystickButton5;
		_keyDetectors["JoystickButton6"] = JoystickButton6;
		_keyDetectors["JoystickButton7"] = JoystickButton7;
		_keyDetectors["JoystickButton8"] = JoystickButton8;
		_keyDetectors["JoystickButton9"] = JoystickButton9;
		_keyDetectors["JoystickButton10"] = JoystickButton10;
		_keyDetectors["JoystickButton11"] = JoystickButton11;
		_keyDetectors["JoystickButton12"] = JoystickButton12;
		_keyDetectors["JoystickButton13"] = JoystickButton13;
		_keyDetectors["JoystickButton14"] = JoystickButton14;
		_keyDetectors["JoystickButton15"] = JoystickButton15;
		_keyDetectors["JoystickButton16"] = JoystickButton16;
		_keyDetectors["JoystickButton17"] = JoystickButton17;
		_keyDetectors["JoystickButton18"] = JoystickButton18;
		_keyDetectors["JoystickButton19"] = JoystickButton19;
		_keyDetectors["Joystick1Button0"] = Joystick1Button0;
		_keyDetectors["Joystick1Button1"] = Joystick1Button1;
		_keyDetectors["Joystick1Button2"] = Joystick1Button2;
		_keyDetectors["Joystick1Button3"] = Joystick1Button3;
		_keyDetectors["Joystick1Button4"] = Joystick1Button4;
		_keyDetectors["Joystick1Button5"] = Joystick1Button5;
		_keyDetectors["Joystick1Button6"] = Joystick1Button6;
		_keyDetectors["Joystick1Button7"] = Joystick1Button7;
		_keyDetectors["Joystick1Button8"] = Joystick1Button8;
		_keyDetectors["Joystick1Button9"] = Joystick1Button9;
		_keyDetectors["Joystick1Button10"] = Joystick1Button10;
		_keyDetectors["Joystick1Button11"] = Joystick1Button11;
		_keyDetectors["Joystick1Button12"] = Joystick1Button12;
		_keyDetectors["Joystick1Button13"] = Joystick1Button13;
		_keyDetectors["Joystick1Button14"] = Joystick1Button14;
		_keyDetectors["Joystick1Button15"] = Joystick1Button15;
		_keyDetectors["Joystick1Button16"] = Joystick1Button16;
		_keyDetectors["Joystick1Button17"] = Joystick1Button17;
		_keyDetectors["Joystick1Button18"] = Joystick1Button18;
		_keyDetectors["Joystick1Button19"] = Joystick1Button19;
		_keyDetectors["Joystick2Button0"] = Joystick2Button0;
		_keyDetectors["Joystick2Button1"] = Joystick2Button1;
		_keyDetectors["Joystick2Button2"] = Joystick2Button2;
		_keyDetectors["Joystick2Button3"] = Joystick2Button3;
		_keyDetectors["Joystick2Button4"] = Joystick2Button4;
		_keyDetectors["Joystick2Button5"] = Joystick2Button5;
		_keyDetectors["Joystick2Button6"] = Joystick2Button6;
		_keyDetectors["Joystick2Button7"] = Joystick2Button7;
		_keyDetectors["Joystick2Button8"] = Joystick2Button8;
		_keyDetectors["Joystick2Button9"] = Joystick2Button9;
		_keyDetectors["Joystick2Button10"] = Joystick2Button10;
		_keyDetectors["Joystick2Button11"] = Joystick2Button11;
		_keyDetectors["Joystick2Button12"] = Joystick2Button12;
		_keyDetectors["Joystick2Button13"] = Joystick2Button13;
		_keyDetectors["Joystick2Button14"] = Joystick2Button14;
		_keyDetectors["Joystick2Button15"] = Joystick2Button15;
		_keyDetectors["Joystick2Button16"] = Joystick2Button16;
		_keyDetectors["Joystick2Button17"] = Joystick2Button17;
		_keyDetectors["Joystick2Button18"] = Joystick2Button18;
		_keyDetectors["Joystick2Button19"] = Joystick2Button19;
		_keyDetectors["Joystick3Button0"] = Joystick3Button0;
		_keyDetectors["Joystick3Button1"] = Joystick3Button1;
		_keyDetectors["Joystick3Button2"] = Joystick3Button2;
		_keyDetectors["Joystick3Button3"] = Joystick3Button3;
		_keyDetectors["Joystick3Button4"] = Joystick3Button4;
		_keyDetectors["Joystick3Button5"] = Joystick3Button5;
		_keyDetectors["Joystick3Button6"] = Joystick3Button6;
		_keyDetectors["Joystick3Button7"] = Joystick3Button7;
		_keyDetectors["Joystick3Button8"] = Joystick3Button8;
		_keyDetectors["Joystick3Button9"] = Joystick3Button9;
		_keyDetectors["Joystick3Button10"] = Joystick3Button10;
		_keyDetectors["Joystick3Button11"] = Joystick3Button11;
		_keyDetectors["Joystick3Button12"] = Joystick3Button12;
		_keyDetectors["Joystick3Button13"] = Joystick3Button13;
		_keyDetectors["Joystick3Button14"] = Joystick3Button14;
		_keyDetectors["Joystick3Button15"] = Joystick3Button15;
		_keyDetectors["Joystick3Button16"] = Joystick3Button16;
		_keyDetectors["Joystick3Button17"] = Joystick3Button17;
		_keyDetectors["Joystick3Button18"] = Joystick3Button18;
		_keyDetectors["Joystick3Button19"] = Joystick3Button19;
		_keyDetectors["Joystick4Button0"] = Joystick4Button0;
		_keyDetectors["Joystick4Button1"] = Joystick4Button1;
		_keyDetectors["Joystick4Button2"] = Joystick4Button2;
		_keyDetectors["Joystick4Button3"] = Joystick4Button3;
		_keyDetectors["Joystick4Button4"] = Joystick4Button4;
		_keyDetectors["Joystick4Button5"] = Joystick4Button5;
		_keyDetectors["Joystick4Button6"] = Joystick4Button6;
		_keyDetectors["Joystick4Button7"] = Joystick4Button7;
		_keyDetectors["Joystick4Button8"] = Joystick4Button8;
		_keyDetectors["Joystick4Button9"] = Joystick4Button9;
		_keyDetectors["Joystick4Button10"] = Joystick4Button10;
		_keyDetectors["Joystick4Button11"] = Joystick4Button11;
		_keyDetectors["Joystick4Button12"] = Joystick4Button12;
		_keyDetectors["Joystick4Button13"] = Joystick4Button13;
		_keyDetectors["Joystick4Button14"] = Joystick4Button14;
		_keyDetectors["Joystick4Button15"] = Joystick4Button15;
		_keyDetectors["Joystick4Button16"] = Joystick4Button16;
		_keyDetectors["Joystick4Button17"] = Joystick4Button17;
		_keyDetectors["Joystick4Button18"] = Joystick4Button18;
		_keyDetectors["Joystick4Button19"] = Joystick4Button19;
    }
			

			#endregion
    }
