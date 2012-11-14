using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using Rampart.BaseClasses;

namespace Rampart.Actors
{
    public class ParticleExplosion : KillableGameObjectBase
    {
        private const int DEFAULT_NUM_PARTICLES = 15;
        private List<SimpleParticle> _particles;

        public ParticleExplosion(int numParticles)
        {
            // We're using the hitpoints to control the particle animation
            this.HitPoints = numParticles;

            _particles = new List<SimpleParticle>();
            for (int i = 0; i < numParticles; i++)
            {
                var particle = new SimpleParticle();
                particle.X = this.Center.X;
                particle.Y = this.Center.Y;
                particle.VelocityY = 5;
                particle.AccelY = -0.1f;
                particle.VelocityX = 1;
                _particles.Add(particle);
            }
        }

        public ParticleExplosion()
            : this(DEFAULT_NUM_PARTICLES)
        {
        }

        public override void Move()
        {
            base.Move();

            // Move them
            _particles.ForEach(p => p.Move());

            // Remove the dead ones
            _particles.RemoveAll(p => p.IsDead);

            this.HitPoints = _particles.Count;
        }

        protected override void OnPaint(Graphics gfx, Rectangle drawableArea)
        {
            _particles.ForEach(p => p.Draw(gfx, drawableArea));
        }
    }
}
