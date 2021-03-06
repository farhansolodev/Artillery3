﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artillery
{
    public class CombatState : GameState
    {
        #region Fields

        #endregion

        #region Constructor
        public CombatState(A3Data a3Data) 
            : base(a3Data)
        {
            Character Innocentia = new Character("Innocentia");
            Player testPlayer = new Player("Doll 1", new PlayerInputMethod());
            testPlayer.Character = Innocentia;

            _a3Data.SelectedPlayer = testPlayer;
        }
        #endregion

        #region Methods
        public override void Draw()
        {
            _a3Data.LogicalTerrain.Draw();
        }

        public override void Exit()
        {

        }

        public override void Initialise()
        {
            Console.WriteLine("Look, we're in the combat state. You happy?");

            _a3Data.GenerateTerrain();
            Console.WriteLine("length of terrain: " + _a3Data.LogicalTerrain.Map.Length);

        }

        public override void Update()
        {
            _inputHandler.HandleInput(_a3Data);
        }
        #endregion

        #region Properties

        #endregion

    }
}
