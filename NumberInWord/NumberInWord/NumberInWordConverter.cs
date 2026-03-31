using System;
using Ascon.Pilot.SDK.ObjectCard;
using System.ComponentModel.Composition;

namespace Ascon.Pilot.SDK.NumberInWord
{
    [Export(typeof(IObjectCardHandler))]
    [Export(typeof(ISettingsFeature))]
    public class NumberInWordConverter : IObjectCardHandler, ISettingsFeature
    {
        public bool Handle(IAttributeModifier modifier, ObjectCardContext context)
        {
            throw new NotImplementedException();
        }

        public bool OnValueChanged(IAttribute sender, AttributeValueChangedEventArgs args, IAttributeModifier modifier)
        {
            throw new NotImplementedException();
        }

        public void SetValueProvider(ISettingValueProvider settingValueProvider)
        {
            throw new NotImplementedException();
        }

        public string Key { get; }
        public string Title { get; }
        public System.Windows.FrameworkElement Editor { get; }
    }
}
