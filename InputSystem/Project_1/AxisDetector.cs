    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Debug = System.Diagnostics.Debug;

    public sealed class AxisDetector:InputDetector
    {
	    bool Equals(AxisDetector other)
	    {
		    return _deadZone.Equals(other._deadZone) && _direction == other._direction && _offset.Equals(other._offset);
	    }

	    public override bool Equals(object obj)
	    {
		    return ReferenceEquals(this, obj) || obj is AxisDetector other && Equals(other);
	    }

	    public override int GetHashCode()
	    {
		    unchecked
		    {
			    var hashCode = _deadZone.GetHashCode();
			    hashCode = (hashCode * 397) ^ (int) _direction;
			    hashCode = (hashCode * 397) ^ _offset.GetHashCode();
			    return hashCode;
		    }
	    }

	    static readonly Dictionary<string, AxisDetector> _axisDetectors;

        #region AxisDetector
			public static readonly AxisDetector Axis1stPositive = new AxisDetector("1st axis", AxisDirection.Positive);
			public static readonly AxisDetector Axis2ndPositive = new AxisDetector("2nd axis", AxisDirection.Positive);
			public static readonly AxisDetector Axis3rdPositive = new AxisDetector("3rd axis", AxisDirection.Positive);
			public static readonly AxisDetector Axis4thPositive = new AxisDetector("4th axis", AxisDirection.Positive);
			public static readonly AxisDetector Axis5thPositive = new AxisDetector("5th axis", AxisDirection.Positive);
			public static readonly AxisDetector Axis6thPositive = new AxisDetector("6th axis", AxisDirection.Positive);
			public static readonly AxisDetector Axis7thPositive = new AxisDetector("7th axis", AxisDirection.Positive);
			public static readonly AxisDetector Axis8thPositive = new AxisDetector("8th axis", AxisDirection.Positive);
			public static readonly AxisDetector Axis9thPositive = new AxisDetector("9th axis", AxisDirection.Positive);
			public static readonly AxisDetector Axis10thPositive = new AxisDetector("10th axis", AxisDirection.Positive);
			public static readonly AxisDetector Axis11thPositive = new AxisDetector("11th axis", AxisDirection.Positive);
			public static readonly AxisDetector Axis12thPositive = new AxisDetector("12th axis", AxisDirection.Positive);
			public static readonly AxisDetector Axis13thPositive = new AxisDetector("13th axis", AxisDirection.Positive);
			public static readonly AxisDetector Axis14thPositive = new AxisDetector("14th axis", AxisDirection.Positive);
			public static readonly AxisDetector Axis15thPositive = new AxisDetector("15th axis", AxisDirection.Positive);
			public static readonly AxisDetector Axis16thPositive = new AxisDetector("16th axis", AxisDirection.Positive);
			public static readonly AxisDetector Axis17thPositive = new AxisDetector("17th axis", AxisDirection.Positive);
			public static readonly AxisDetector Axis18thPositive = new AxisDetector("18th axis", AxisDirection.Positive);
			public static readonly AxisDetector Axis19thPositive = new AxisDetector("19th axis", AxisDirection.Positive);
			public static readonly AxisDetector Axis20thPositive = new AxisDetector("20th axis", AxisDirection.Positive);
			public static readonly AxisDetector Axis21stPositive = new AxisDetector("21st axis", AxisDirection.Positive);
			public static readonly AxisDetector Axis22ndPositive = new AxisDetector("22nd axis", AxisDirection.Positive);
			public static readonly AxisDetector Axis23rdPositive = new AxisDetector("23rd axis", AxisDirection.Positive);
			public static readonly AxisDetector Axis24thPositive = new AxisDetector("24th axis", AxisDirection.Positive);
			public static readonly AxisDetector Axis25thPositive = new AxisDetector("25th axis", AxisDirection.Positive);
			public static readonly AxisDetector Axis26thPositive = new AxisDetector("26th axis", AxisDirection.Positive);
			public static readonly AxisDetector Axis27thPositive = new AxisDetector("27th axis", AxisDirection.Positive);
			public static readonly AxisDetector Axis28thPositive = new AxisDetector("28th axis", AxisDirection.Positive);
			public static readonly AxisDetector Axis1stNegative = new AxisDetector("1st axis", AxisDirection.Negative);
			public static readonly AxisDetector Axis2ndNegative = new AxisDetector("2nd axis", AxisDirection.Negative);
			public static readonly AxisDetector Axis3rdNegative = new AxisDetector("3rd axis", AxisDirection.Negative);
			public static readonly AxisDetector Axis4thNegative = new AxisDetector("4th axis", AxisDirection.Negative);
			public static readonly AxisDetector Axis5thNegative = new AxisDetector("5th axis", AxisDirection.Negative);
			public static readonly AxisDetector Axis6thNegative = new AxisDetector("6th axis", AxisDirection.Negative);
			public static readonly AxisDetector Axis7thNegative = new AxisDetector("7th axis", AxisDirection.Negative);
			public static readonly AxisDetector Axis8thNegative = new AxisDetector("8th axis", AxisDirection.Negative);
			public static readonly AxisDetector Axis9thNegative = new AxisDetector("9th axis", AxisDirection.Negative);
			public static readonly AxisDetector Axis10thNegative = new AxisDetector("10th axis", AxisDirection.Negative);
			public static readonly AxisDetector Axis11thNegative = new AxisDetector("11th axis", AxisDirection.Negative);
			public static readonly AxisDetector Axis12thNegative = new AxisDetector("12th axis", AxisDirection.Negative);
			public static readonly AxisDetector Axis13thNegative = new AxisDetector("13th axis", AxisDirection.Negative);
			public static readonly AxisDetector Axis14thNegative = new AxisDetector("14th axis", AxisDirection.Negative);
			public static readonly AxisDetector Axis15thNegative = new AxisDetector("15th axis", AxisDirection.Negative);
			public static readonly AxisDetector Axis16thNegative = new AxisDetector("16th axis", AxisDirection.Negative);
			public static readonly AxisDetector Axis17thNegative = new AxisDetector("17th axis", AxisDirection.Negative);
			public static readonly AxisDetector Axis18thNegative = new AxisDetector("18th axis", AxisDirection.Negative);
			public static readonly AxisDetector Axis19thNegative = new AxisDetector("19th axis", AxisDirection.Negative);
			public static readonly AxisDetector Axis20thNegative = new AxisDetector("20th axis", AxisDirection.Negative);
			public static readonly AxisDetector Axis21stNegative = new AxisDetector("21st axis", AxisDirection.Negative);
			public static readonly AxisDetector Axis22ndNegative = new AxisDetector("22nd axis", AxisDirection.Negative);
			public static readonly AxisDetector Axis23rdNegative = new AxisDetector("23rd axis", AxisDirection.Negative);
			public static readonly AxisDetector Axis24thNegative = new AxisDetector("24th axis", AxisDirection.Negative);
			public static readonly AxisDetector Axis25thNegative = new AxisDetector("25th axis", AxisDirection.Negative);
			public static readonly AxisDetector Axis26thNegative = new AxisDetector("26th axis", AxisDirection.Negative);
			public static readonly AxisDetector Axis27thNegative = new AxisDetector("27th axis", AxisDirection.Negative);
			public static readonly AxisDetector Axis28thNegative = new AxisDetector("28th axis", AxisDirection.Negative);
			#endregion
        
        #region AxisDetectorInitial
		static AxisDetector() {
		_axisDetectors = new Dictionary<string, AxisDetector>(57);
		_axisDetectors["Axis1st Positive"] = Axis1stPositive;
		_axisDetectors["Axis2nd Positive"] = Axis2ndPositive;
		_axisDetectors["Axis3rd Positive"] = Axis3rdPositive;
		_axisDetectors["Axis4th Positive"] = Axis4thPositive;
		_axisDetectors["Axis5th Positive"] = Axis5thPositive;
		_axisDetectors["Axis6th Positive"] = Axis6thPositive;
		_axisDetectors["Axis7th Positive"] = Axis7thPositive;
		_axisDetectors["Axis8th Positive"] = Axis8thPositive;
		_axisDetectors["Axis9th Positive"] = Axis9thPositive;
		_axisDetectors["Axis10th Positive"] = Axis10thPositive;
		_axisDetectors["Axis11th Positive"] = Axis11thPositive;
		_axisDetectors["Axis12th Positive"] = Axis12thPositive;
		_axisDetectors["Axis13th Positive"] = Axis13thPositive;
		_axisDetectors["Axis14th Positive"] = Axis14thPositive;
		_axisDetectors["Axis15th Positive"] = Axis15thPositive;
		_axisDetectors["Axis16th Positive"] = Axis16thPositive;
		_axisDetectors["Axis17th Positive"] = Axis17thPositive;
		_axisDetectors["Axis18th Positive"] = Axis18thPositive;
		_axisDetectors["Axis19th Positive"] = Axis19thPositive;
		_axisDetectors["Axis20th Positive"] = Axis20thPositive;
		_axisDetectors["Axis21st Positive"] = Axis21stPositive;
		_axisDetectors["Axis22nd Positive"] = Axis22ndPositive;
		_axisDetectors["Axis23rd Positive"] = Axis23rdPositive;
		_axisDetectors["Axis24th Positive"] = Axis24thPositive;
		_axisDetectors["Axis25th Positive"] = Axis25thPositive;
		_axisDetectors["Axis26th Positive"] = Axis26thPositive;
		_axisDetectors["Axis27th Positive"] = Axis27thPositive;
		_axisDetectors["Axis28th Positive"] = Axis28thPositive;
		_axisDetectors["Axis1st Negative"] = Axis1stNegative;
		_axisDetectors["Axis2nd Negative"] = Axis2ndNegative;
		_axisDetectors["Axis3rd Negative"] = Axis3rdNegative;
		_axisDetectors["Axis4th Negative"] = Axis4thNegative;
		_axisDetectors["Axis5th Negative"] = Axis5thNegative;
		_axisDetectors["Axis6th Negative"] = Axis6thNegative;
		_axisDetectors["Axis7th Negative"] = Axis7thNegative;
		_axisDetectors["Axis8th Negative"] = Axis8thNegative;
		_axisDetectors["Axis9th Negative"] = Axis9thNegative;
		_axisDetectors["Axis10th Negative"] = Axis10thNegative;
		_axisDetectors["Axis11th Negative"] = Axis11thNegative;
		_axisDetectors["Axis12th Negative"] = Axis12thNegative;
		_axisDetectors["Axis13th Negative"] = Axis13thNegative;
		_axisDetectors["Axis14th Negative"] = Axis14thNegative;
		_axisDetectors["Axis15th Negative"] = Axis15thNegative;
		_axisDetectors["Axis16th Negative"] = Axis16thNegative;
		_axisDetectors["Axis17th Negative"] = Axis17thNegative;
		_axisDetectors["Axis18th Negative"] = Axis18thNegative;
		_axisDetectors["Axis19th Negative"] = Axis19thNegative;
		_axisDetectors["Axis20th Negative"] = Axis20thNegative;
		_axisDetectors["Axis21st Negative"] = Axis21stNegative;
		_axisDetectors["Axis22nd Negative"] = Axis22ndNegative;
		_axisDetectors["Axis23rd Negative"] = Axis23rdNegative;         
		_axisDetectors["Axis24th Negative"] = Axis24thNegative;
		_axisDetectors["Axis25th Negative"] = Axis25thNegative;
		_axisDetectors["Axis26th Negative"] = Axis26thNegative;
		_axisDetectors["Axis27th Negative"] = Axis27thNegative;
		_axisDetectors["Axis28th Negative"] = Axis28thNegative;
		}
		#endregion
        
		
		float _deadZone;

        public enum AxisDirection
        {
            Negative=-1,
            Positive=1
        }
        
        AxisDirection _direction;
        float _offset;

        public AxisDirection Direction => _direction;

        public float Offset => _offset;

        public AxisDetector(string name, AxisDirection direction, float deadZone = .15f)
        {
            _name = name;
            _direction = direction;
            _deadZone = deadZone;
        }

        public override void Refresh()
        {
            _offset = Input.GetAxis(_name);
            _isPressed = false;
            _isReleased = false;
            bool flag = _offset * (int) _direction - _deadZone > 0;
            if (_isHeld)
            {
                if (!flag)
                {
                    _isReleased = true;
                    _isHeld = false;
                }
            }
            else
            {
                if (flag)
                {
                    _isPressed = true;
                    _isHeld = true;
                    _lastPressTime = Time.time;
                }
            }
        }

        public static AxisDetector ToAxisDetector(string name)
        {
            return _axisDetectors.ContainsKey(name) ? _axisDetectors[name] : null;
        }

        public static bool operator ==(AxisDetector lhs, AxisDetector rhs)
        {
            Debug.Assert(lhs != null, nameof(lhs) + " != null");
            Debug.Assert(rhs != null, nameof(rhs) + " != null");
            return lhs._name.Equals(rhs._name, StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool operator !=(AxisDetector lhs, AxisDetector rhs)
        {
            Debug.Assert(lhs != null, nameof(lhs) + " != null");
            Debug.Assert(rhs != null, nameof(rhs) + " != null");
            return !lhs._name.Equals(rhs._name, StringComparison.CurrentCultureIgnoreCase);
        }
    }
