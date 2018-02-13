namespace Lily.Microservice.Microparts.CallChain
{
    public abstract class BaseMicropartOfCallTree : IMicropart
    {
        public MicropartType MicropartType => MicropartType.CallTree;
    }
}
