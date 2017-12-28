﻿using UnityEngine;

using com.spacepuppy.Utils;

namespace com.spacepuppy.Events
{

    public abstract class Triggerable : SPComponent, ITriggerable
    {

        #region Fields

        [SerializeField()]
        private int _order;

        #endregion

        #region ITriggerable Interface

        public int Order
        {
            get { return _order; }
            set { _order = value; }
        }

        public virtual bool CanTrigger
        {
            get { return this.isActiveAndEnabled; }
        }

        public void Trigger()
        {
            this.Trigger(null, null);
        }

        public abstract bool Trigger(object sender, object arg);

        #endregion

    }

    public abstract class AutoTriggerable : Triggerable
    {

        #region Fields

        [SerializeField()]
        private ActivateEvent _activateOn = ActivateEvent.None;

        #endregion

        #region CONSTRUCTOR

        protected override void Start()
        {
            base.Start();

            if (_activateOn.HasFlag(ActivateEvent.OnStart))
            {
                this.Trigger(this, null);
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            if (_activateOn.HasFlag(ActivateEvent.OnStart) && !this.started) return;

            if (_activateOn.HasFlag(ActivateEvent.OnEnable))
            {
                this.Trigger(this, null);
            }
        }

        #endregion

        #region Properties

        public ActivateEvent ActivateOn
        {
            get { return _activateOn; }
            set { _activateOn = value; }
        }

        #endregion


    }

}
