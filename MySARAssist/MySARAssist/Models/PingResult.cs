using System;
using System.Collections.Generic;
using System.Text;

namespace MySARAssist.Models
{
    public class PingResult
    {
        private Guid _PingResultID;
        private DateTime _PingTime;
        private long _PingResultMs;
        private double _Latitude;
        private double _Longitude;
        private double _LocationAccuracy;
        private bool _HasBeenExported;

        public Guid PingResultID { get => _PingResultID; set => _PingResultID = value; }
        public DateTime PingTime { get => _PingTime; set => _PingTime = value; }
        public long PingResultMs { get => _PingResultMs; set => _PingResultMs = value; }
        public double Latitude { get => _Latitude; set => _Latitude = value; }
        public double Longitude { get => _Longitude; set => _Longitude = value; }
        public double LocationAccuracy { get => _LocationAccuracy; set => _LocationAccuracy = value; }
        public bool HasBeenExported { get => _HasBeenExported; set => _HasBeenExported = value; }


        public PingResult()
        {
            PingResultID = Guid.NewGuid();

        }
        public bool IsComplete
        {
            get
            {
                if (PingTime <= DateTime.MinValue) { return false; }
                if (PingResultMs <= 0) { return false; }
                if (Latitude == 0) { return false; }
                if (Longitude == 0) { return false; }
                if (LocationAccuracy == 0) { return false; }
                return true;
            }
        }
    }
}
