using System;
using Ascon.Pilot.SDK;

namespace PilotObjectInfo.Services
{
    /// <summary>
    /// Service wrapper for ITabServiceProvider
    /// Encapsulates navigation operations from SDK
    /// </summary>
    public class NavigationService
    {
        private readonly ITabServiceProvider _tabServiceProvider;

        public NavigationService(ITabServiceProvider tabServiceProvider)
        {
            _tabServiceProvider = tabServiceProvider;
        }

        public void ShowElement(Guid objectId)
        {
            _tabServiceProvider.ShowElement(objectId);
        }
    }
}

