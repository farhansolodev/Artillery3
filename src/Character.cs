﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;
using static ArtillerySeries.src.ArtilleryGame; // Constants

namespace ArtillerySeries.src
{
    // Players use a single Character per game and they are pre-defined in the system (e.g. Innocentia/Object 261)
    //  but they can also be added to and customized on the fly.
    //  I don't know if this will work well but hey, let's hope for the best!
    class Character : EntityAssembly, IPhysicsComponent
    {
        Vehicle _vehicle;
        //Point2D _pos;
        Bitmap _charBitmap;
        PhysicsComponent _physics;
        bool _selected;

        Weapon _weapon;


        public Character(string name) 
            : base(name)
        {
            _physics = new PhysicsComponent(this);
            _vehicle = new Vehicle(name);
            _selected = false;
            //_pos = new Point2D();

            Entities.Add(_vehicle);
            EntityManager.Instance.AddEntity(this);

            _weapon = new Weapon("weapon");
            Entities.Add(_weapon);


        }

        public void MoveLeft()
        {
            Move(Constants.PlayerSpeed * -1);
        }
        public void MoveRight()
        {
            Move(Constants.PlayerSpeed);
        }

        void Move(float acc) //TODO: Change to accel
        {
            _physics.AccX = acc;
        }

        public bool Selectecd { get => _selected; set => _selected = value; }
        PhysicsComponent IPhysicsComponent.Physics { get => _physics; set => _physics = value; }

        public override void Draw()
        {

            if (_charBitmap == null)
            {
                if (_physics.OnGround)
                    SwinGame.FillCircle(Color.IndianRed, Pos.X, Pos.Y, Constants.InvalidPlayerCircleRadius);
                else
                    SwinGame.FillCircle(Color.Purple, Pos.X, Pos.Y, Constants.InvalidPlayerCircleRadius);

                if (_physics.Facing == FacingDirection.Left)
                    SwinGame.FillCircle(Color.Aquamarine, Pos.X - 3, Pos.Y, Constants.InvalidPlayerCircleRadius);
                else
                    SwinGame.FillCircle(Color.Aquamarine, Pos.X + 3, Pos.Y, Constants.InvalidPlayerCircleRadius);

            }



            float angle = (float)(_physics.AbsAngleToGround * 180 / Math.PI);
            SwinGame.DrawText("Absolute Angle: " + angle.ToString(), Color.Black, 50, 50);
            angle = (float)(_physics.RelAngleToGround * 180 / Math.PI);
            SwinGame.DrawText("Relative Angle: " + angle.ToString(), Color.Black, 50, 70);

            base.Draw(); // Draws the sub-entities
        }


        public override void Update()
        {
            Direction = _physics.Facing;
            Pos = _physics.Position;
            UpdatePosition(_physics.Position, _physics.Facing);
            base.Update(); // Updates the sub-entities
        }
    }
}
