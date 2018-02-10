using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frostie
{
    public class Enums
    {
        public enum KindsOfDeath
        {
            Normal,
            InFire,
            Squeezed
        }

        public enum ObjectType
        {
            SOLID,
            LEATHAL,
            HOT,
            SLIPPERY
        }

        public enum Features
        {
            MELTING,
            THROW_HEAD,
            SHOOT_BUTTON,
            FREEZE_GROUND,
            TAKE_WATER,
            DECOUPLE_MID
        };

        public enum Faces
        {
            FRONT,
            SIDE,
            BACK,
        };

        public enum Mood
        {
            SAD,
            HAPPY,
            ANGRY
        }

        public enum FollowType
        {
            MoveTowards,
            Lerp
        }
    }
}
