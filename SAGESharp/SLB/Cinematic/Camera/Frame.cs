using SAGESharp.SLB.IO;
using System;

namespace SAGESharp.SLB.Cinematic.Camera
{
    public sealed class Frame : IEquatable<Frame>
    {
        [SLBElement(1)]
        public float Time { get; set; }

        [SLBElement(2)]
        public Point3D Position { get; set; }

        [SLBElement(3)]
        public Point3D Target { get; set; }

        public bool Equals(Frame other)
        {
            if (other == null)
            {
                return false;
            }

            return Time == other.Time &&
                Position == other.Position &&
                Target == other.Target;
        }

        public override string ToString()
            => $"Time={Time}, Position={Position}, Target={Target}";

        public override bool Equals(object other)
            => Equals(other as Frame);

        public override int GetHashCode()
        {
            int hash = 7507;
            Time.AddHashCodeByVal(ref hash, 907);
            Position.AddHashCodeByRef(ref hash, 907);
            Target.AddHashCodeByRef(ref hash, 907);

            return hash;
        }

        public static bool operator ==(Frame left, Frame right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }
            else if (left is null)
            {
                return right.Equals(left);
            }
            else
            {
                return left.Equals(right);
            }
        }

        public static bool operator !=(Frame left, Frame right)
            => !(left == right);
    }
}
