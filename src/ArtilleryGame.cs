﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;

namespace ArtillerySeries.src
{

    public static class Constants
    {
        public const float Gravity = 0.6f;
        public const float VelocityLoss = 1f;
        public const string Data = "data.xml";

        public const int WindowHeight = 900;
        public const int WindowWidth = WindowHeight * 16 / 9;


        //For now we'll have consts inside here, i'll incorporate xml support later.

        
        public const int InvalidPlayerCircleRadius = 3;
        public const float PlayerSpeed = 0.5f; //TODO: Change to Accel
        public const float BaseExplosionRadius = 20;
        public const int BaseExplosionDiaScaling = 35;
        public const int BaseCollisionRadius = 5;
        public const float BaseFrictionCoefKinetic = 0.5f;
        public const float BaseFrictionCoefStatic = 0.8f;
        public const float BaseFrictionStaticError = 0.2f;
        public const float BaseVehicleWeight = 1000f; //Arbitrary units
        public const int VectorSightSize = 20;
    }
    class ArtilleryGame
    {
        Rectangle _windowRect;
        World _world;
        InputHandler _inputHandler;





        public ArtilleryGame()
        {
            LoadResources();

            _windowRect = new Rectangle
            {
                Width = Constants.WindowWidth,
                Height = Constants.WindowHeight
            };

            _inputHandler = new InputHandler();
            _world = new World(_windowRect, _inputHandler);
            
        }


        private void LoadResources()
        {

        }
      



        public void Run()
        {

            SwinGame.OpenAudio();
            //Open the game window
            SwinGame.OpenGraphicsWindow("Artillery3", (int)_windowRect.Width, (int)_windowRect.Height);


            Player player1 = new Player("2B");
            Character Innocentia = new Character("Innocentia");
            player1.Character = Innocentia;


            Player player2 = new Player("Sam");
            Character char2 = new Character("char2");
            player2.Character = char2;

            _world.AddPlayer(player1);
            _world.AddPlayer(player2);

            _world.CyclePlayers();
            _world.NewSession();


            while (!SwinGame.WindowCloseRequested())
            {
                //Fetch the next batch of UI interaction
                SwinGame.ProcessEvents();

                _world.HandleInput();



                PhysicsEngine.Instance.Simulate();
                EntityManager.Instance.Update();
                


                SwinGame.ClearScreen(Color.White);
                SwinGame.DrawFramerate(0, 0);


                _world.Draw();
                EntityManager.Instance.Draw();
                SwinGame.RefreshScreen(60);
            }

            SwinGame.CloseAudio();
            SwinGame.ReleaseAllResources();
        }

    }
}
