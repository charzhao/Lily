using System;

namespace Lily.Microservice.Microparts.EventBus.Features
{
    public class AccountHierarchyInfo
    {
        public AccountHierarchyInfo(Guid dealerId, Guid customerId, Guid siteId)
        {
            DealerId = dealerId;
            SiteId = siteId;
            CustomerId = customerId;
        }
        public Guid DealerId { get; }
        public Guid SiteId { get; }
        public Guid CustomerId { get; }
    }
}
