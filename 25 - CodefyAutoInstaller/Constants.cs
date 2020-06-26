using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CodefyAutoInstaller
{
    static class Constants
    {
        public static readonly string APP_VERSION = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;

        public static readonly string TEMP_DIRECTORY = Path.GetTempPath();

        public static readonly string SERVER_BASE_URL = "https://misc.codefycs.org/automated_installer_system/";

        public static readonly string VERSION_URL = SERVER_BASE_URL + "newestVersion.txt";

        public static readonly string CERTIFICATE_NAME = "codefy_root_certificate.cer";

        public static readonly string CERTIFICATE_URL = SERVER_BASE_URL + CERTIFICATE_NAME;

        public enum FileList
        {
            [Description("scratch_basic.json")]
            SCRATCH_BASIC,
            [Description("java_basic.json")]
            JAVA_BASIC,
            [Description("java_advanced.json")]
            JAVA_ADVANCED,
            [Description("python_basic.json")]
            PYTHON_BASIC,
            [Description("python_advanced.json")]
            PYTHON_ADVANCED,
            [Description("web_development.json")]
            WEB_DEVELOPMENT,
            [Description("web_applications.json")]
            WEB_APPLICATIONS
        }

        public static string GetDescription<T>(this T e) where T : IConvertible //Copy & Pasted from codementor.io
        {
            if (e is Enum)
            {
                Type type = e.GetType();
                Array values = Enum.GetValues(type);

                foreach (int val in values)
                {
                    if (val == e.ToInt32(System.Globalization.CultureInfo.InvariantCulture))
                    {
                        var memInfo = type.GetMember(type.GetEnumName(val));
                        var descriptionAttribute = memInfo[0]
                            .GetCustomAttributes(typeof(DescriptionAttribute), false)
                            .FirstOrDefault() as DescriptionAttribute;

                        if (descriptionAttribute != null)
                        {
                            return descriptionAttribute.Description;
                        }
                    }
                }
            }

            return null; // could also return string.Empty
        }
    }
}
