using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class State : StateBase
    {
        public List<StateBase> tokens = new List<StateBase>();

        /// <summary>
        /// True if any token is true
        /// </summary>
        public override bool IsOn
        {
            get
            {
                foreach (StateBase token in tokens)
                {
                    if(token.IsOn)
                        return true;
                }
                return false;
            }
        }

        public void Add(StateBase token)
        {
            tokens.Add(token);
        }

        public void Remove(StateBase token)
        {
            tokens.Remove(token);
        }
    }