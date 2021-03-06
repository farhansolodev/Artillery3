﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;
using static ArtillerySeries.src.Utilities;

namespace ArtillerySeries.src
{
    // A leaf node of the composite pattern used for entities.
    // Entities are defined as any game thing that is not the UI.
    // That should have its own composite tree or will be integrated here slowly.
    // Entities have a position and angle, and face a certain direction, though they don't need to be utilised.
    public abstract class Entity : DrawableObject, ICameraCanFocus
    {
        // PhysicsComponent _physicsComponent

        string _name;
        string _shortDesc;
        string _longDesc;
        Point2D _pos;
        FacingDirection _direction;
        float _absAngle;

        private bool _damageable = true;

        public Entity(string name)
        {
            _name = name;
            _pos = ZeroPoint2D();
            _direction = FacingDirection.Left;
            _absAngle = 0;

            _shortDesc = "Entity " + _name;
            _longDesc = "This object is an entity of the " + name + " class.";


        }

        public string Name { get => _name; set => _name = value; }
        public Point2D Pos { get => _pos; set => _pos = value; }
        public virtual string ShortDesc { get => _shortDesc; set => _shortDesc = value; }
        public virtual string LongDesc { get => _longDesc; set => _longDesc = value; }
        internal FacingDirection Direction { get => _direction; set => _direction = value; }
        public float AbsoluteAngle { get => _absAngle; set => _absAngle = value; }
        public bool Damageable { get => _damageable; set => _damageable = value; }

        public abstract override void Draw();

        public abstract override void Update();

        public virtual void Damage(float damage)
        {
        }

        public virtual void UpdatePosition(Point2D pos, FacingDirection direction, float absoluteAngle)
        {
            _absAngle = absoluteAngle;
            _pos = pos;
            _direction = direction;
        }

    }
}
