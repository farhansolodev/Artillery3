﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace ArtillerySeries.src
{
    public class LoadingGameState : GameState
    {
        Stack<GameState> _gameStates;
        A3RData _a3RData;
        public LoadingGameState(Stack<GameState> gameStates, A3RData a3RData) : base(null)
        {
            _gameStates = gameStates;
            _a3RData = a3RData;
        }

        public override void Update()
        {
            GameState poppedState = _gameStates.Pop();
            if (poppedState != this)
            {
                throw new Exception("Loading state was not most recent state -- something smells fishy.");
            }


            //let's make a fun transition:
            float i = 0;
            
            while (i <= 1)
            {
                i += 0.05f;
                Color _rectColour = SwinGame.RGBAFloatColor(1f, 1f, 1f, i);
                SwinGame.FillRectangle(_rectColour,_a3RData.Camera.Pos.X, _a3RData.Camera.Pos.Y, 2000,2000);

                SwinGame.RefreshScreen(60);
            }
            UserInterface.Instance.Clear();


            GameState nextState = _gameStates.Pop();
            GameState leavingState = _gameStates.Pop();

            leavingState.ExitState();
            nextState.EnterState();

            _gameStates.Push(nextState);
        }
    }
}
