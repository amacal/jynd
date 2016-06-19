using System;

namespace Jynd
{
    [Serializable]
    public class JyndException : Exception
    {
        public JyndException(string message)
            : base(message)
        {
        }
    }
}