﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinGameSDK;
using static ArtillerySeries.src.Utilities;

namespace ArtillerySeries.src
{
    class Laser : Projectile
    {
        int _rayCastStep;

        Point2D _destination;
        Point2D _direction;

        Color _color = Color.Ivory;

        float _lifeTime;
        float _maxLife;

        float _originalAlpha = 1;

        public Laser(string name, Weapon parentWeapon, Point2D from, Point2D to, float damage, float explRad, float damageRad)
            : base(name, parentWeapon, from, ZeroPoint2D(), damage, explRad, damageRad)
        {
            Physics.Enabled = false;
            _destination = to;
            _maxLife = 25f;
            _lifeTime = _maxLife;
            Pos = from;
            Explode(to);
        }

        public Color Color { get => _color; set
            {
                _color = value;
                _originalAlpha = ((float)value.A / 255);
            }
        }

        public override void Draw()
        {
            for(int i = -3; i < 6; i++)
            {
                SwinGame.DrawLine(_color, Pos.X + i, Pos.Y + i, _destination.X + i, _destination.Y + i);
                SwinGame.DrawLine(_color, Pos.X + i, Pos.Y - i, _destination.X + i, _destination.Y - i);
            }

        }

        public override void Explode(Point2D pt)
        {
            Artillery3R.Services.EntityManager.DamageEntities(this, BaseDamage, (int)DamageRad, pt);
            BlowUpTerrain(pt);
            Artillery3R.Services.ParticleEngine.CreateLaserExplosion(pt, 100);
        }

        public override void Update()
        {
            if (Enabled)
            {
                _lifeTime -= 0.1f;
                if (_lifeTime < 0)
                    SwitchState(ProjectileState.Dead);

                _color = SwinGame.RGBAColor(_color.R, _color.G, _color.B, (byte)(_lifeTime / _maxLife * _originalAlpha * 255));

            }
            else
            {
                Visible = false;
                Artillery3R.Services.EntityManager.RemoveEntity(this);
                Artillery3R.Services.PhysicsEngine.RemoveComponent(this);
            }
        }
    }
}
