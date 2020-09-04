using System;
using Services;

namespace SoftwareLock
{
    public static class GenerateActivationCodeClient
    {
        public static string ActivationCode()
        {
            var code = "";
            try
            {
                var hddSerial = HardSerialInfo.GetFirstSerial();
                if (string.IsNullOrEmpty(hddSerial))
                {
                    hddSerial = HardSerialInfo.GetSecondSerial();
                    if (string.IsNullOrEmpty(hddSerial))
                    {
                        hddSerial = HardSerialInfo.GetThirdSerial();
                        if (string.IsNullOrEmpty(hddSerial))
                        {
                            hddSerial = HardSerialInfo.GetFourthSerial();
                            if (string.IsNullOrEmpty(hddSerial)) hddSerial = "456812";
                        }
                    }
                }

                var sum = (long)0;
                foreach (var item in hddSerial.ToCharArray())
                    sum += (char.Parse(item.ToString()));

                sum += 5891732;
                sum *= 45;
                
                code = sum.ToString();
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }

            return code;
        }

        public static string GenerateExpireDate(string serverCode, string clientCode)
        {
            try
            {
                DateTime? expDate = null;
                var server = serverCode.Remove(0, clientCode.Length);

                var year = server.Substring(0, 4).ParseToInt();
                var mounth = server.Substring(4, 2).ParseToInt();
                var day = server.Substring(6, 2).ParseToInt();
                expDate = new DateTime(year, mounth, day);

                return expDate.ToString();
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
                return null;
            }
        }

        public static bool GenerateCodeOnActive(string activationCode, string enteredCilientCode)
        {
            try
            {
                var server = enteredCilientCode.Remove(activationCode.Length, 8);

                var activCode = server.ParseToInt() - 13992598;

                if (activCode.ToString() == activationCode)
                    return true;
                else return false;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
                return false;
            }
        }

    }
}
