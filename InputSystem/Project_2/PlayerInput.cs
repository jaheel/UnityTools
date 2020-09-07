using System;
using GameInputSystem;
using PersisterData;
using UnityEngine;

namespace InputSystem
{
    public class PlayerInput:InputComponent,IDataPersister
    {
        static PlayerInput _instance;
        public static PlayerInput Instance => _instance;

        
        [SerializeField]
        InputButton pause=new InputButton(KeyCode.Escape,XboxControllerButtons.Menu);
        [SerializeField]
        InputButton interact=new InputButton(KeyCode.F,XboxControllerButtons.Y);
        [SerializeField]
        InputButton jump=new InputButton(KeyCode.Space,XboxControllerButtons.A);
        
        [SerializeField]
        InputAxis horizontal=new InputAxis(KeyCode.D,KeyCode.A,XboxControllerAxes.LeftStickHorizontal);
        [SerializeField]
        InputAxis vertical=new InputAxis(KeyCode.W,KeyCode.S,XboxControllerAxes.LeftStickVertical);


        public InputButton Pause => pause;
        public InputButton Jump => jump;
        public InputButton Interact => interact;

        public InputAxis Horizontal => horizontal;
        public InputAxis Vertical => vertical;
        

        [HideInInspector]
        [SerializeField]
        DataSettings dataSettings;

        public bool HaveControl => _haveControl;
        bool _haveControl = true;
        
        void Awake()
        {
            if (_instance == null)
                _instance = this;
            else
            {
                throw new UnityException("There cannot be more than one PlayerInput script");
            }
        }

        void OnEnable()
        {
            if (_instance == null)
                _instance = this;
            else if(_instance!=this)
                throw new UnityException("There cannot be more than one PlayerInput script");
            
            PersistentDataManager.RegisterPersister(this);
        }

        void OnDisable()
        {
            PersistentDataManager.UnregisterPersister(this);
            _instance = null;
        }

        
        #region InputSystem

        protected override void GetInputs(bool fixedUpdateHappened)
        {
            pause.Get(fixedUpdateHappened,UserInputType);
            interact.Get(fixedUpdateHappened,UserInputType);
            jump.Get(fixedUpdateHappened,UserInputType);
            horizontal.Get(UserInputType);
            vertical.Get(UserInputType);
            
        }

        /// <summary>
        /// user gain control, system will not check user Input
        /// </summary>
        /// <example>Game pause or user is in vertigo state</example>
        public override void GainControl()
        {
            _haveControl = true;
            GainControl(pause);
            GainControl(interact);
            GainControl(jump);
            GainControl(horizontal);
            GainControl(vertical);
        }

        /// <summary>
        /// user release control, system can check user Input
        /// </summary>
        /// <example>Game continue or user is not in vertigo state</example>
        public override void ReleaseControl(bool resetValues = true)
        {
            _haveControl = false;
            ReleaseControl(pause,resetValues);
            ReleaseControl(interact,resetValues);
            ReleaseControl(jump,resetValues);
            ReleaseControl(horizontal,resetValues);
            ReleaseControl(vertical,resetValues);
        }
        

        #endregion
        

        #region DataPersister

        public DataSettings GetDataSettings()
        {
            return dataSettings;
        }

        public void SetDataSettings(string dataTag, PersistenceType persistenceType)
        {
            dataSettings.dataTag = dataTag;
            dataSettings.persistenceType = persistenceType;
        }

        public PData SaveData()
        {
            return new PData();
        }

        public void LoadData(PData data)
        {
            throw new NotImplementedException();
        }

        #endregion
        
    }
}