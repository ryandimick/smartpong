using System.Collections.Generic;
using System.Linq;
using SmartPong.Models;

namespace SmartPong
{
    internal class SettingsManager
    {
        private readonly SmartPongContext _context;

        internal SettingsManager(SmartPongContext context)
        {
            _context = context;
        }

        internal IEnumerable<Setting> RetrieveSettings()
        {
            return _context.Settings.ToList();
        }

        internal Setting UpdateSetting(Setting updatedSetting)
        {
            var currentSetting = _context.Settings.First(s => s.Key == updatedSetting.Key);
            _context.Entry(currentSetting).CurrentValues.SetValues(updatedSetting);
            _context.SaveChanges();
            return updatedSetting;
        }
    }
}
