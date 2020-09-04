using System;
using Services;

namespace SoftwareLock
{
    public class HardSerialInfo
    {
        public static string GetFirstSerial()
        {
            var serial = "";
            try
            {
                var df = new GetDiskInfo.DiskInfo();
                GetDiskInfo.HDiskInfo.GetDriveInfo(0, ref df);
                serial = df.SerialNumber;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }

            return serial;
        }
        public static string GetSecondSerial()
        {
            var serial = "";
            try
            {
                var df = new GetDiskInfo.DiskInfo();
                GetDiskInfo.HDiskInfo.GetDriveInfo(1, ref df);
                serial = df.SerialNumber;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }

            return serial;
        }
        public static string GetThirdSerial()
        {
            var serial = "";
            try
            {
                var df = new GetDiskInfo.DiskInfo();
                GetDiskInfo.HDiskInfo.GetDriveInfo(2, ref df);
                serial = df.SerialNumber;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }

            return serial;
        }
        public static string GetFourthSerial()
        {
            var serial = "";
            try
            {
                var df = new GetDiskInfo.DiskInfo();
                GetDiskInfo.HDiskInfo.GetDriveInfo(3, ref df);
                serial = df.SerialNumber;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }

            return serial;
        }
    }
}
