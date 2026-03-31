using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ascon.Pilot.SDK.NumberInWord
{
    public class SettingsLoader : IObserver<KeyValuePair<string, string>>
    {
        private readonly IPersonalSettings _personalSettings;

        private IDisposable _subscription;
        private TaskCompletionSource<string> _tcs;

        public SettingsLoader(IPersonalSettings personalSettings)
        {
            _personalSettings = personalSettings;

        }

        public Task<string> Load()
        {
            _tcs = new TaskCompletionSource<string>();
            _subscription = _personalSettings.SubscribeSetting("NumberInWordConverter-E4D4E10E-F4AE-40A1-AD9A-FB50A3FA8486").Subscribe(this);
            return _tcs.Task;
        }


        public void OnNext(KeyValuePair<string, string> value)
        {
            if (value.Key == "NumberInWordConverter-E4D4E10E-F4AE-40A1-AD9A-FB50A3FA8486")
            {
                _tcs.TrySetResult(value.Value);
                _subscription.Dispose();
            }
        }

        public void OnError(Exception error)
        {
        }

        public void OnCompleted()
        {
        }
    }
}
