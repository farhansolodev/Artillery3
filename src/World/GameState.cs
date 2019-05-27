﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtillerySeries.src
{

    public interface IGameState : IDrawable, IUpdatable
    {

    }
    public abstract class GameState : IGameState
    {


        #region Fields
        bool _enabled;
        A3RData _a3RData;
        protected UIElementAssembly _uiModule;
        #endregion

        #region Constructor
        public GameState(A3RData a3RData)
        {
            Enabled = true;
            _a3RData = a3RData;
        }
        #endregion

        #region Methods
        public virtual void Draw()
        {
            
        }

        public virtual void Update()
        {
            
        }
        #endregion

        #region Properties
        public bool Enabled { get => _enabled; set => _enabled = value; }

        #endregion
    }
}
