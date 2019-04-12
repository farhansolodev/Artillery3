﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;
using static ArtillerySeries.src.ArtilleryGame; // Constants

namespace ArtillerySeries.src
{

    enum CharacterState // Out of fuel state?
    {
        Idle,
        Walking,
        Firing,
        Damage,
        Finished
    }
    // Players use a single Character per game and they are pre-defined in the system (e.g. Innocentia/Object 261)
    //  but they can also be added to and customized on the fly.
    //  I don't know if this will work well but hey, let's hope for the best!
    class Character : EntityAssembly, IPhysicsComponent, IStateComponent<CharacterState>
    {
        Vehicle _vehicle;
        //Point2D _pos;
        Bitmap _charBitmap;
        PhysicsComponent _physics;
        StateComponent<CharacterState> _state;
        bool _selected;

        //Weapons should be seperate list.
        //they are treated seperately as base.draw() will draw all the sights, which isn't required...
        // OR, they can be in the same list with a known selected weaopn who's sight will be drawn.
        // Weapons are selected by iterating through known weapons by iterating through the list
        // and checking if the entity implements the IWeapon interface, or having a seperate list that
        // is updated every time a new entity is added to the list.

        Weapon _selectedWeapon;
        Weapon _weapon;
        Weapon _weapon2;
        List<Weapon> _weapons;


        public Character(string name)
            : base(name)
        {
            _physics = new PhysicsComponent(this);
            _vehicle = new Vehicle(name);
            _selected = false;
            _weapon = new Weapon("Base Weapon 1 -- Remove asap.", 0f, 50f);
            _weapon2 = new Weapon("Base Weapon 2 -- Remove asap.", 50f, 120f);
            _weapons = new List<Weapon>();

            Entities.Add(_vehicle);
            Entities.Add(_weapon);
            Entities.Add(_weapon2);
            EntityManager.Instance.AddEntity(this);

            _state = new StateComponent<CharacterState>(CharacterState.Idle);


            _selectedWeapon = _weapon;


            UpdateWeaponList();

        }

        public void MoveLeft()
        {
            Move(Constants.PlayerSpeed * -1);
        }
        public void MoveRight()
        {
            Move(Constants.PlayerSpeed);
        }

        public void ChargeWeapon()
        {
            _selectedWeapon.Charge();
            SwitchState(CharacterState.Firing);
        }

        public void FireWeapon()
        {
            _selectedWeapon.Fire();
            SwitchState(CharacterState.Finished); // Can only fire each weapon once
        }

        void Move(float acc)
        {
            _physics.AccX = acc;
        }

        public bool Selected { get => _selected; set => _selected = value; }

        PhysicsComponent IPhysicsComponent.Physics { get => _physics; set => _physics = value; }

        void UpdateWeaponList()
        {
            _weapons = new List<Weapon>();
            foreach (Entity e in Entities)
            {
                if (e is Weapon)
                {
                    _weapons.Add(e as Weapon);
                }
            }
        }

        public void ElevateWeapon()
        {
            _selectedWeapon.ElevateWeapon();
        }

        public void DepressWeapon()
        {
            _selectedWeapon.DepressWeapon();
        }

        public void SwitchWeapon()
        {
            if (_weapons.Count != 1)
            {
                _selectedWeapon = _weapons[(_weapons.IndexOf(_selectedWeapon) + 1) % _weapons.Count];
            }
        }

        public void AddEntity(Entity entity)
        {
            Entities.Add(entity);
            UpdateWeaponList();
        }

        public void RemoveEntity(Entity entity)
        {
            Entities.Remove(entity);
            UpdateWeaponList();
        }

        public void DrawSight()
        {
            _selectedWeapon.DrawSight();
        }

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

            SwinGame.DrawText(Name, Color.DarkGray, Pos.X, Pos.Y - 30);


            float angle = (float)(_physics.AbsAngleToGround * 180 / Math.PI);
            //SwinGame.DrawText("Absolute Angle: " + angle.ToString(), Color.Black, 50, 50);
            angle = (float)(_physics.RelAngleToGround * 180 / Math.PI);
            //SwinGame.DrawText("Relative Angle: " + angle.ToString(), Color.Black, 50, 70);

            

            base.Draw(); // Draws the sub-entities
        }




        public override void Update()
        {

            Direction = _physics.Facing;
            Pos = _physics.Position;
            AbsoluteAngle = _physics.AbsAngleToGround;

            base.Update(); // Updates the sub-entities
        }

        public void SwitchState(CharacterState state)
        {
            // State machine transition code goes here

            switch (PeekState())
            {
                case CharacterState.Idle:
                    if (state == CharacterState.Firing)
                    {

                    }
                    //Play firing animation

                    break;

                case CharacterState.Firing:
                    if(state == CharacterState.Finished)
                    {
                        //get camera to mvoe?
                        Console.WriteLine("Camera move should go here!");
                    }
                    break;

                default:
                    break;
            }


            _state.Switch(state);
            Console.WriteLine(Name + " switched states to " + state);


        }


        public void PushState(CharacterState state)
        {
            _state.Push(state);
        }

        public CharacterState PeekState()
        {
            return _state.Peek();
        }

        public CharacterState PopState()
        {
            return _state.Pop();
        }
    }
}
