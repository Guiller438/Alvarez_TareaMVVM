using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Alvarez_TareaMVVM.AlvarezViewsModels
{
    class AlvarezAboutViewModels
    {
        public string Title => AppInfo.Name;
        public string Version => AppInfo.VersionString;
        public string MoreInfoUrl => "https://www.youtube.com/watch?v=DA9E_a5AqIk";
        public string Message => "Esta app esta realizada por Guillermo Alvarez";
        public ICommand ShowMoreInfoCommand { get; }

        public AlvarezAboutViewModels()
        {
            ShowMoreInfoCommand = new AsyncRelayCommand(ShowMoreInfo);
        }

        async Task ShowMoreInfo() =>
            await Launcher.Default.OpenAsync(MoreInfoUrl);
    }
}
