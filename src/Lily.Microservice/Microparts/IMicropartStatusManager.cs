using System;
using System.Collections.Generic;
using Lily.Microservice.Microparts.Imp;

namespace Lily.Microservice.Microparts
{
    public interface IMicropartStatusManager
    {


        IMicroPartInfo UpdateStatus<T>(MicropartType micropartType) 
            where T : class, IMicroPartInfo;

        void UpdateStatus(MicropartType micropartType,
            Action<MicropartStatusInfo> statusUpdateAction);



        IMicroPartInfo UpdateStatus<T>(string statusKey)
            where T : class, IMicroPartInfo;

        void UpdateStatus(string statusKey,
            Action<MicropartStatusInfo> statusUpdateAction);

        Dictionary<string, MicropartStatusInfo> GetAllStatus();
    }
}
