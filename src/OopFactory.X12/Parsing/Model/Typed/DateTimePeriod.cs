using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class DateTimePeriod
    {
        public string Qualifier { get; private set; }
        public DateTime? _startDate;
        public DateTime? _endDate;
        public TimeSpan? _time;
        public bool IsDateRange
        {
            get
            {
                return _time.HasValue && 0 != _time.Value.Ticks;
            }
        }

        public DateTimePeriod()
        {
        }

        public DateTimePeriod(TimeSpan time)
        {
            this.Qualifier = "TM";
            this._time = time;
        }

        public DateTimePeriod(DateTime date)
        {
            this.Qualifier = "D8";
            this._startDate = date;
        }


        public DateTimePeriod(DateTime date, TimeSpan time)
        {
            SetDT(date, time);
        }

        public DateTimePeriod(DateTime startDate, DateTime endDate)
        {
            _time = endDate.Subtract(startDate);
            SetRD8(startDate, endDate);
        }

        public bool IsTM
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Qualifier))
                {
                    return false;
                }

                if (Qualifier == "TM")
                {
                    return true;
                }

                return false;
            }
        }

        public bool IsDT
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Qualifier))
                {
                    return false;
                }

                if (Qualifier == "DT")
                {
                    return true;
                }

                return false;
            }
        }

        public bool IsD8
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Qualifier))
                {
                    return false;
                }

                if (Qualifier == "D8")
                {
                    return true;
                }

                return false;
            }
        }

        public bool IsRD8
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Qualifier))
                {
                    return false;
                }

                if (Qualifier == "RD8")
                {
                    return true;
                }

                return false;
            }
        }

        public void SetRD8(DateTime startDate, DateTime endDate)
        {
            this.Qualifier = "RD8";
            this._startDate = startDate;
            this._endDate = endDate;
        }

        public void SetDT(DateTime date, TimeSpan time)
        {
            this.Qualifier = "DT";
            this._startDate = date;
            this._time = time;
            this._endDate = null;

            string s = date.ToString("yyyyMMdd");
            s += time.ToString("hhmm");

            this._startDate = DateTime.ParseExact(s, "yyyyMMddhhmm", null, System.Globalization.DateTimeStyles.None);
        }

        public void SetD8(DateTime date)
        {
            this.Qualifier = "D8";
            this._startDate = date;
            this._endDate = null;
            this._time = null;
        }

        public DateTime StartDate
        {
            get { return _startDate.Value; }
        }

        public DateTime EndDate
        {
            get { return _endDate.Value; }
        }

        public TimeSpan Time
        {
            get { return _time.Value; }
        }

        public override string ToString()
        {
            if (!this._endDate.HasValue && !this._startDate.HasValue && !this._time.HasValue)
            {
                return string.Empty;
            }

            if (string.IsNullOrWhiteSpace(Qualifier))
            {
                return string.Empty;
            }

            switch (Qualifier)
            {
                case "TM":
                    return String.Format("{0:hhmm}", this._time);
                case "DT":
                    return String.Concat(String.Format("{0:yyyyMMdd}", this._startDate),
                        string.Format("{0:hhmm}", this._time));
                case "D8":
                    return String.Format("{0:yyyyMMdd}", this._startDate);
                case "RD8":
                    return String.Format("{0:yyyyMMdd}-{1:yyyyMMdd}",
                        this._startDate, this._endDate);
                default:
                    return string.Empty;
            }
        }

        public static bool TryParse(string s, out DateTimePeriod result)
        {
            result = new DateTimePeriod();

            if (string.IsNullOrWhiteSpace(s))
            {
                return false;
            }

            int length = s.Length;

            // TM - hmm Time: 800
            if (length == 3)
            {
                TimeSpan ts;

                // TimeSpan TryParse in Mono seems broken. Manually do it.
                string sHours = s.Substring(0, 1);
                string sMin = s.Substring(1);

                int iHours = 0;
                int iMin = 0;

                if (!int.TryParse(sHours, out iHours))
                {
                    return false;
                }

                if (!int.TryParse(sMin, out iMin))
                {
                    return false;
                }

                try
                {
                    ts = new TimeSpan(iHours, iMin, 0);
                }
                catch (Exception)
                {
                    return false;
                }

                result = new DateTimePeriod(ts);
                return true;
            }

            // TM - hhmm Time: 0800
            if (length == 4)
            {
                TimeSpan ts;

                // TimeSpan TryParse in Mono seems broken. Manually do it.
                string sHours = s.Substring(0, 2);
                string sMin = s.Substring(2);

                int iHours = 0;
                int iMin = 0;

                if (!int.TryParse(sHours, out iHours))
                {
                    return false;
                }

                if (!int.TryParse(sMin, out iMin))
                {
                    return false;
                }

                try
                {
                    ts = new TimeSpan(iHours, iMin, 0);
                }
                catch (Exception)
                {
                    return false;
                }

                result = new DateTimePeriod(ts);
                return true;
            }

            // DT - yyyyMMddhhmm
            if (length == 12 && !s.Contains('-'))
            {
                TimeSpan ts;
                DateTime d;

                string sDate = s.Substring(0, 8);
                string sHours = s.Substring(8, 2);
                string sMin = s.Substring(10, 2);

                int iHours = 0;
                int iMin = 0;

                if (!int.TryParse(sHours, out iHours))
                {
                    return false;
                }

                if (!int.TryParse(sMin, out iMin))
                {
                    return false;
                }

                try
                {
                    ts = new TimeSpan(iHours, iMin, 0);
                }
                catch (Exception)
                {
                    return false;
                }

                if (!DateTime.TryParseExact(sDate, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out d))
                {
                    return false;
                }

                result = new DateTimePeriod(d, ts);
                return true;
            }

            var dates = s.TrimEnd('-').TrimStart('-').Split('-');

            // D8 - yyyyMMDD
            if (dates.Length == 1)
            {
                DateTime d1;

                if (!DateTime.TryParseExact(dates[0], "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out d1))
                {
                    return false;
                }

                result = new DateTimePeriod(d1);
                return true;
            }

            // RD8 - yyyyMMDD-yyyyMMDD
            if (dates.Length == 2)
            {
                DateTime d1;
                DateTime d2;

                if (!DateTime.TryParseExact(dates[0], "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out d1))
                {
                    return false;
                }

                if (!DateTime.TryParseExact(dates[1], "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out d2))
                {
                    return false;
                }

                result = new DateTimePeriod(d1, d2);
                return true;
            }

            return false;
        }

        public static DateTimePeriod Parse(string s)
        {
            DateTimePeriod result = new DateTimePeriod();

            if (TryParse(s, out result) == false)
            {
                throw new FormatException(string.Format("Invalid DateTimePeriod format: {0}", s));
            }

            return result;
        }
    }
}
