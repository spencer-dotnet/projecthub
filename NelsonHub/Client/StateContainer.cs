using NelsonHub.Shared.NWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NelsonHub.Client
{
    public class StateContainer
    {
        // Initial value
        public AwsPeriodModel[] CurrentForecast {get; set;}

        public event Action OnChange;

        public void SetCurrentForecast(AwsPeriodModel[] value)
        {
            CurrentForecast = value;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
