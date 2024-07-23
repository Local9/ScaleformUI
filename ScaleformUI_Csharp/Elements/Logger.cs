namespace ScaleformUI.Elements
{
    internal class Logger
    {
        public static void WriteLine(string message)
        {
#if FIVEM
            CitizenFX.Core.Debug.WriteLine(message);
#else
            Console.WriteLine(message);
#endif
        }
    }
}
