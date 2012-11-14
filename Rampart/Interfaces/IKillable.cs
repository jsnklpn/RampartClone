using System;

namespace Rampart.Interfaces
{
    public interface IKillable
    {
        int HitPoints { get; set; }
        bool IsDead { get; }
    }
}
