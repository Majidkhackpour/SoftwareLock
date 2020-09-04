using System;
using Microsoft.Win32;
using Services;

namespace SoftwareLock
{
    public static class clsRegistery
    {
        public static ReturnedSaveFuncInfo SetRegistery(string value, string name)
        {
            var ret = new ReturnedSaveFuncInfo();
            try
            {
                Registry.SetValue("HKEY_CURRENT_USER\\Software\\Arad", name,
                    value);
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
                ret.AddReturnedValue(ex);
            }
            return ret;
        }
        public static string GetRegistery(string name)
        {
            try
            {
                var a = Registry.GetValue("HKEY_CURRENT_USER\\Software\\Arad\\", name,
                    "");
                return a.ToString();
            }
            catch (Exception ex)
            {
                //WebErrorLog.ErrorInstence.StartErrorLog(ex);
                return null;
            }
        }

        public static bool DeleteRegistery(string value)
        {
            try
            {

                GetRegistery(value);
                SetRegistery("", value);
                return true;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
                return false;
            }
        }

    }
}
