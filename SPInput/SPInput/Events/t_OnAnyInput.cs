﻿#pragma warning disable 0649 // variable declared but not used.

using UnityEngine;

using com.spacepuppy.Events;

namespace com.spacepuppy.SPInput.Events
{

    public class t_OnAnyInput : SPComponent, IObservableTrigger
    {

        #region Fields

        [SerializeField]
        private SPEvent _onInput;

        private Vector3 _lastMouse;

        #endregion

        #region Methods

        protected override void OnEnable()
        {
            base.OnEnable();

            _lastMouse = Input.mousePosition;
        }

        // Update is called once per frame
        void Update()
        {
            var mpos = Input.mousePosition;

            if (Input.anyKeyDown || Input.mouseScrollDelta.sqrMagnitude > 0f || (mpos - _lastMouse).sqrMagnitude > 0.0001f)
            {
                _lastMouse = mpos;
                _onInput.ActivateTrigger(this, null);
                return;
            }

            var inputManager = Services.Get<IInputManager>();
            foreach (var dev in inputManager)
            {
                if (dev.AnyInputActivated)
                {
                    _onInput.ActivateTrigger(this, null);
                    return;
                }
            }
        }

        #endregion

        #region IObservableTrigger Interface

        BaseSPEvent[] IObservableTrigger.GetEvents()
        {
            return new BaseSPEvent[] { _onInput };
        }

        #endregion

    }

}
