using System;
using System.Collections.Generic;
using Ascon.Pilot.SDK.ObjectCard;
using System.ComponentModel.Composition;
using System.Globalization;
using Ascon.Pilot.SDK.NumberInWord.Model;
using System.IO;
using NumberInWords;
using Newtonsoft.Json;
using System.Runtime.Remoting.Contexts;
using System.Linq;

namespace Ascon.Pilot.SDK.NumberInWord
{

    [Export(typeof(IObjectCardHandler))]
    [Export(typeof(ISettingsFeature))]
    public class NumberInWordConverter : IObjectCardHandler, ISettingsFeature
    {
        private List<TripleNumberTextAttr> _tripleNumberTextAttr;
        private IPersonalSettings _personalSettings;

        [ImportingConstructor]
        public NumberInWordConverter(IPersonalSettings personalSettings)
        {
            Key = "NumberInWordConverter-E4D4E10E-F4AE-40A1-AD9A-FB50A3FA8486";
            Title = "Конвертер чисел debug";
            Editor = null;
            LoadSettings(personalSettings);
            _personalSettings = personalSettings;
        }

        public bool Handle(IAttributeModifier modifier, ObjectCardContext context)
        {
          
            var isObjectModification = context.EditiedObject != null;
            if (context.IsReadOnly)
                return false;

            if (_tripleNumberTextAttr == null)
            {
                LoadSettings(_personalSettings);
                throw new Exception("Не загрузились настройки");
            }

            return false;    
        }

        public bool OnValueChanged(IAttribute sender, AttributeValueChangedEventArgs args, IAttributeModifier modifier)
        {
            var rule = _tripleNumberTextAttr?.FirstOrDefault(x => x.NumberAttr == sender.Name);

            if (rule != null && args.NewValue != null)
            {
                try
                {
                    decimal n = Convert.ToDecimal(args.NewValue, CultureInfo.InvariantCulture);

                    modifier.SetValue(rule?.StrAttr, RusNumber.Str(n));

                    modifier.SetValue(rule?.StrNumberAttr, n.ToString("#,##0.##", CultureInfo.GetCultureInfo("ru-RU")));
                }
                catch { /* Ошибка конвертации — пользователь еще в процессе ввода */ }
            }

            return false;
        }

        public void SetValueProvider(ISettingValueProvider settingValueProvider)
        {
            
        }

        public string Key { get; }
        public string Title { get; }
        public System.Windows.FrameworkElement Editor { get; }

        private async void LoadSettings(IPersonalSettings personalSettings)
        {
            var setting = new SettingsLoader(personalSettings);
            var json = await setting.Load();
            _tripleNumberTextAttr = GetListTripleNumberTextAttr(json);
        }

        private static List<TripleNumberTextAttr> GetListTripleNumberTextAttr(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<TripleNumberTextAttr>>(json);

            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
