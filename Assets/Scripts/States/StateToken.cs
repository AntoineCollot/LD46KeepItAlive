using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class StateToken : StateBase
    {
        public bool isOn = false;

        public override bool IsOn
        {
            get
            {
                return isOn;
            }
        }
    }