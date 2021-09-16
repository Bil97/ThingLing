using System;
using ThingLing.Shared.Models;

namespace ThingLing.Client.Services
{
    public class StateContainer
    {
        private string title;
        private Software software;

        public event Action OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();

        public string Title
        {
            get => title;
            set
            {
                title = value;
                NotifyStateChanged();
            }
        }

        public Software Software
        {
            get => software;
            set
            {
                software = value;
                NotifyStateChanged();
            }
        }
    }
}
