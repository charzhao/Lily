using System;
using System.Collections.Generic;
using System.Text;
using Lily.Microservice.Common.Extensions;

namespace Lily.Microservice.Microparts.Imp
{
    internal class MicropartStatusManager : IMicropartStatusManager
    {
        private IEnumerable<IMicroPartInfo> _microServiceParts;
        public MicropartStatusManager(IEnumerable<IMicroPartInfo> microServiceParts)
        {
            _microServiceParts = microServiceParts;
        }

        private readonly Dictionary<string, MicropartStatusInfo> _status = new Dictionary<string, MicropartStatusInfo>();
        private readonly Dictionary<MicropartType,string> _micropartMetadata=new Dictionary<MicropartType, string>()
        {
            {MicropartType.CallTree, "CallTree"},
            {MicropartType.EventBus, "EventBus"},
            {MicropartType.ServiceCenter, "ServiceCenter"},
            {MicropartType.ConfigurationCenter, "ConfigurationCenter"},
            {MicropartType.Authentication, "Authentication"},
            {MicropartType.Log, "Log"},
        };

        private void UpdateStatus(MicropartType micropartType, IMicroPartInfo microParInfo)
        {
            UpdateStatus(micropartType, micropartStatusInfo =>
                {
                    micropartStatusInfo.MicroPartInstanceName = microParInfo.MicroServicePartName;
                    micropartStatusInfo.IsEnabled = microParInfo.IsEnabled;
                });
        }

      

        public void UpdateStatus(string statusKey, IMicroPartInfo microParInfo)
        {
            UpdateStatus(statusKey, micropartStatusInfo =>
            {
                micropartStatusInfo.MicroPartInstanceName = microParInfo.MicroServicePartName;
                micropartStatusInfo.IsEnabled = microParInfo.IsEnabled;
            });
        }

        public IMicroPartInfo UpdateStatus<T>(string statusKey) where T : class, IMicroPartInfo
        {
            var micropartInfo = _microServiceParts.FirstOfType<T>();
            UpdateStatus(statusKey, micropartStatusInfo =>
            {
                micropartStatusInfo.MicroPartInstanceName = micropartInfo.MicroServicePartName;
                micropartStatusInfo.IsEnabled = micropartInfo.IsEnabled;
            });
            return micropartInfo;
        }

        public void UpdateStatus(string statusKey, Action<MicropartStatusInfo> statusUpdateAction)
        {
            var microServiceComponentStatus = Get(statusKey);
            statusUpdateAction(microServiceComponentStatus);
        }

        public IMicroPartInfo UpdateStatus<T>(MicropartType micropartType) 
            where T : class, IMicroPartInfo
        {
            var micropartInfo = _microServiceParts.FirstOfType<T>();
            UpdateStatus(micropartType, micropartStatusInfo =>
            {
                micropartStatusInfo.MicroPartInstanceName = micropartInfo.MicroServicePartName;
                micropartStatusInfo.IsEnabled = micropartInfo.IsEnabled;
            });
            return micropartInfo;
        }

        public void UpdateStatus(MicropartType micropartType, Action<MicropartStatusInfo> statusUpdateAction)
        {
            var micropartStatusName = _micropartMetadata[micropartType];
            var microServiceComponentStatus = Get(micropartStatusName);
            statusUpdateAction(microServiceComponentStatus);
        }





        public Dictionary<string, MicropartStatusInfo> GetAllStatus()
        {
            return _status;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var status in _status)
            {
                sb.Append(status.Key).Append(" : ");
                sb.Append(status.Value.IsEnabled).Append("   ");
                sb.Append(status.Value.IsInitSuccessed).Append("   ");
                sb.Append(status.Value.IsWrokingNow).Append("   ");
                sb.Append(status.Value.Comments).Append("   ");
                sb.AppendLine();
            }
            return sb.ToString();
        }

        private MicropartStatusInfo Get(string key)
        {
            if (!_status.ContainsKey(key))
            {
                _status[key] = new MicropartStatusInfo();
            }

            return _status[key];
        }
    }
}
