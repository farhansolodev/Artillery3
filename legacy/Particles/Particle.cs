﻿using SwinGameSDK;

namespace ArtillerySeries.src
{

    public class Particle : Entity, IPhysicsComponent
    {
        Bitmap _bitmap;
        PhysicsComponent _physics;
        double _life;
        double _maxLife;
        Color _color;
        double _radius;
        float _damage = 0;

        public Particle(double life, Point2D pos, Point2D vel, double radius, Color color, float weight, float windFricMult)
            : this(life, pos, vel, radius, color, weight)
        {
            _physics.WindFrictionMult = windFricMult;
        }

        public Particle(double life, Point2D pos, Point2D vel, double radius, Color color, float weight)
            : base("particle")
        {
            _physics = new PhysicsComponent(this);
            _physics.Velocity = vel;
            _physics.Position = pos;

            _maxLife = life;
            _life = life;
            _radius = radius;

            _color = color;

            if (weight == 0f)
            {
                _physics.GravityEnabled = false;
                _physics.Weight = 0f;
            }
            else
            {
                _physics.GravityEnabled = true;
                _physics.Weight = weight;
            }
        }

        public Point2D Vel
        {
            get => _physics.Velocity;
            set => _physics.Velocity = value;
        }

        public override void Draw()
        {
            if (Visible)
            {
                if (_bitmap == null)
                {
                    SwinGame.FillCircle(_color, Pos, (int)_radius);
                }
            }

        }

        public override void Update()
        {
            if (Enabled)
            {
                _life -= Constants.ParticleLifeDispersion;
                if (_life < 0)
                {
                    Enabled = false;
                    Visible = false;
                    ParticleEngine.Instance.RemoveParticle(this);
                    PhysicsEngine.Instance.RemoveComponent(this);
                }

                _color = SwinGame.RGBAColor(_color.R, _color.G, _color.B, (byte)(255 * _life / _maxLife));


                if (_damage != 0f)
                {
                    EntityManager.Instance.DamageEntities(this, _damage, (int)_radius, Pos);
                }

                Pos = _physics.Position;
            } else
            {
                _physics = null;
                PhysicsEngine.Instance.RemoveComponent(this);
                ParticleEngine.Instance.RemoveParticle(this);
            }
        }

        public void SetGravity(bool hasGravity)
        {
            _physics.GravityEnabled = hasGravity;
        }

        public void SetFriction(float friction)
        {
            _physics.FricCoefficient = friction;
        }

        public void SetDamage(float damage)
        {
            _damage = damage;
        }

        public PhysicsComponent Physics { get => _physics; set => _physics = value; }
        public Color Color { get => _color; set => _color = value; }
        public double Radius { get => _radius; set => _radius = value; }
    }
}
