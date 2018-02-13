using System;

namespace Lily.Microservice.Exceptions
{
    public class MicroServicePartNotEnabledException : Exception
    {
        public string MicroServicePartName { get; private set; }
        public MicroServicePartNotEnabledException(string microServicePartName)
        : base(microServicePartName)
        {
            MicroServicePartName = microServicePartName;
        }
    }
}
