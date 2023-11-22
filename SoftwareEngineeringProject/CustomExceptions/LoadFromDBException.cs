using SoftwareEngineeringProject.Delegates;

namespace SoftwareEngineeringProject.CustomExceptions
{
    public class LoadFromDBException : Exception
    {
        public LoadFromDBException(string message, LogDelegate logDelegate) : base(message)
        {
            LogException(message, logDelegate);
        }

        private void LogException(string message, LogDelegate logDelegate)
        {
            logDelegate($"[ERR] Exception: {message}");
        }
    }
}
