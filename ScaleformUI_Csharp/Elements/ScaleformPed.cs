#if FIVEM
using CitizenFX.Core;
#endif

#if ALTV
using System.Numerics;
#endif

namespace ScaleformUI.Elements
{
    internal class ScaleformPed
    {
        public int Handle { get; private set; }

#if FIVEM
        private Ped Ped => new Ped(Handle);
#endif

        public ScaleformPed(int handle)
        {
            Handle = handle;
        }

        public bool IsInRangeOf(Vector3 position, float distance)
        {
#if FIVEM
            return Ped.IsInRangeOf(position, distance);
#endif
        }

        public Vector3 Position
        {
#if FIVEM
            get => Ped.Position;
            set => Ped.Position = value;
#endif
        }
    }
}
