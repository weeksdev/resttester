using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using MahApps.Metro;
using System.Windows;


namespace RestTester.viewmodel
{
    class ThemeViewModel: ViewModelBase
    {
        class ThemeSettings: AppSettings<ThemeSettings>
        {
            private string _baseTheme = "BaseLight";

            public string BaseTheme
            {
                get { return _baseTheme; }
                set { _baseTheme = value; }
            }

            private string _themeAccent = "Red";

            public string ThemeAccent
            {
                get { return _themeAccent; }
                set { _themeAccent = value; }
            }

        }
        private ThemeSettings themeSettings;
        public ThemeViewModel()
        {
            themeSettings = ThemeSettings.Load("theme.settings.json");
            BaseTheme = themeSettings.BaseTheme;
            ThemeAccent = themeSettings.ThemeAccent;
            SetTheme();
        }
        private List<string> _baseThemes = new List<string>()
        {
            "BaseLight",
            "BaseDark"
        };
        public List<string> BaseThemes
        {
            get { return _baseThemes; }
            set { _baseThemes = value; RaisePropertyChanged("BaseThemes"); }
        }

        private string _baseTheme;
        public string BaseTheme
        {
            get { return _baseTheme; }
            set { _baseTheme = value; themeSettings.BaseTheme = _baseTheme; themeSettings.Save("theme.settings.json"); SetTheme(); RaisePropertyChanged("BaseTheme"); }
        }

        private List<string> _themeAccents = new List<string>()
        {
            "Red", "Green", "Blue", "Purple", "Orange", "Lime", "Emerald", "Teal", "Cyan", "Cobalt", "Indigo", "Violet", "Pink", "Magenta", "Crimson", "Amber", "Yellow", "Brown", "Olive", "Steel", "Mauve", "Taupe", "Sienna"
        };

        public List<string> ThemeAccents
        {
            get { return _themeAccents; }
            set { _themeAccents = value; RaisePropertyChanged("ThemeAccents"); }
        }

        private string _themeAccent;
        public string ThemeAccent
        {
            get { return _themeAccent; }
            set { _themeAccent = value; themeSettings.ThemeAccent = _themeAccent; themeSettings.Save("theme.settings.json"); SetTheme(); RaisePropertyChanged("ThemeAccent"); }
        }

        private void SetTheme()
        {
            var theme = ThemeManager.DetectAppStyle(Application.Current);
            ThemeManager.ChangeAppStyle(Application.Current,
                                    ThemeManager.GetAccent(themeSettings.ThemeAccent),
                                    ThemeManager.GetAppTheme(themeSettings.BaseTheme));
            
            
        }
    }
}
