﻿using System;
using System.Text;

namespace SAGESharp.SLB.Level.Conversation
{
    public sealed class Frame : IEquatable<Frame>
    {
        /// <summary>
        /// The size in bytes of an <see cref="Frame"/> in a binary SLB file.
        /// </summary>
        /// 
        /// This includes all the properties (as 32 bit numbers) except by
        /// <see cref="ConversationSounds"/>, for that property the only the
        /// offset where the actual string is located is stored instead.
        internal const int BINARY_SIZE = 24;

        public int ToaAnimation { get; set;  }

        public int CharAnimation { get; set; }

        public int CameraPositionTarget { get; set; }

        public int CameraDistance { get; set; }

        public int StringIndex { get; set; }

        public string ConversationSounds { get; set; }

        public bool Equals(Frame other)
        {
            if (other == null)
            {
                return false;
            }

            return ToaAnimation == other.ToaAnimation &&
                CharAnimation == other.CharAnimation &&
                CameraPositionTarget == other.CameraPositionTarget &&
                CameraDistance == other.CameraDistance &&
                StringIndex == other.StringIndex &&
                ConversationSounds.SafeEquals(other.ConversationSounds);
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.AppendFormat("ToaAnimation={0}", ToaAnimation).Append(", ");
            result.AppendFormat("CharAnimation={0}", CharAnimation).Append(", ");
            result.AppendFormat("CameraPositionTarget={0}", CameraPositionTarget).Append(", ");
            result.AppendFormat("CameraDistance={0}", CameraDistance).Append(", ");
            result.AppendFormat("StringIndex={0}", StringIndex).Append(", ");
            result.AppendFormat("ConversationSounds={0}", ConversationSounds);

            return result.ToString();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Frame);
        }

        public override int GetHashCode()
        {
            var hash = 31;
            ToaAnimation.AddHashCodeByVal(ref hash, 89);
            CharAnimation.AddHashCodeByVal(ref hash, 89);
            CameraPositionTarget.AddHashCodeByVal(ref hash, 89);
            CameraDistance.AddHashCodeByVal(ref hash, 89);
            StringIndex.AddHashCodeByVal(ref hash, 89);
            ConversationSounds.AddHashCodeByRef(ref hash, 89);

            return hash;
        }

        public static bool operator ==(Frame left, Frame right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }
            else if (right is null)
            {
                return left.Equals(right);
            }
            else
            {
                return right.Equals(left);
            }
        }

        public static bool operator !=(Frame left, Frame right)
        {
            return !(left == right);
        }
    }
}
