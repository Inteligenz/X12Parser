using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed {
    public class DateTimePeriod {
        public bool IsDateRange { get; private set; }
        public bool IsTime { get; private set; }
        public DateTime? _startDate;
        public DateTime? _endDate;
        public TimeSpan? _time;

        public DateTimePeriod() {
            IsDateRange = false;
        }

        public DateTimePeriod(TimeSpan time) {
            this._time = time;
            IsTime = true;
            IsDateRange = false;
        }

        public DateTimePeriod(DateTime date) {
            this._startDate = date;
            IsDateRange = false;
        }

        public DateTimePeriod(DateTime startDate, DateTime endDate) {
            this._startDate = startDate;
            this._endDate = endDate;
            IsDateRange = true;
        }

        public DateTime StartDate {
            get { return _startDate.Value; }
            set {
                this._startDate = value;
                SetIsDateRange();
            }
        }

        public DateTime EndDate {
            get { return _endDate.Value; }
            set {
                this._endDate = value;
                SetIsDateRange();
            }
        }

        public TimeSpan Time {
            get { return _time.Value; }
            set {
                this._time = value;
                this.IsTime = true;
                this.IsDateRange = false;
                this._endDate = null;
                this._startDate = null;
            }
        }

        private void SetIsDateRange() {
            if (this._endDate.HasValue && this._startDate.HasValue) {
                this.IsDateRange = true;
            } else {
                this.IsDateRange = false;
            }

            if (this._endDate.HasValue || this._startDate.HasValue) {
                this.IsTime = false;
                this._time = null;
            }
        }

        public override string ToString() {
            if (!this._endDate.HasValue && !this._startDate.HasValue && !this._time.HasValue) {
                return string.Empty;
            }

            // DT
            if (IsTime) {
                return String.Format("{0:hhmm}", this._time);
            } 

            // RD8
            if (IsDateRange) {
                return String.Format("{0:yyyyMMdd}-{1:yyyyMMdd}", 
                    this._startDate, this._endDate);
            }

            // D8
            return String.Format("{0:yyyyMMdd}", this._startDate);
        }

        public static bool TryParse(string s, out DateTimePeriod result) {
            result = new DateTimePeriod();

            if (string.IsNullOrWhiteSpace(s)) {
                return false;
            }

            int length = s.Length;

            // TM - HMM Time: 800
            if (length == 3) {
                TimeSpan ts;

                // TimeSpan TryParse in Mono seems broken. Manually do it.
                string sHours = s.Substring(0, 1);
                string sMin = s.Substring(1);

                int iHours = 0;
                int iMin = 0;

                if (!int.TryParse(sHours, out iHours)) {
                    return false;
                }

                if (!int.TryParse(sMin, out iMin)) {
                    return false;
                }

                try {
                    ts = new TimeSpan(iHours, iMin, 0);
                } catch (Exception) {
                    return false;
                }

                result.Time = ts;
                return true;
            }

            // TM - HMM Time: 0800
            if (length == 4) {
                TimeSpan ts;

                // TimeSpan TryParse in Mono seems broken. Manually do it.
                string sHours = s.Substring(0, 2);
                string sMin = s.Substring(2);

                int iHours = 0;
                int iMin = 0;

                if (!int.TryParse(sHours, out iHours)) {
                    return false;
                }

                if (!int.TryParse(sMin, out iMin)) {
                    return false;
                }

                try {
                    ts = new TimeSpan(iHours, iMin, 0);
                } catch (Exception) {
                    return false;
                }

                result.Time = ts;
                return true;
            }

            var dates = s.TrimEnd('-').Split('-');

            // D8 - yyyyMMDD
            if (dates.Length == 1) {
                DateTime d1;

                if (!DateTime.TryParseExact(dates[0], "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out d1)) {
                    return false;
                }

                result.StartDate = d1;
                return true;
            }

            // RD8 - yyyyMMDD-yyyyMMDD
            if (dates.Length == 2) {
                DateTime d1;
                DateTime d2;

                if (!DateTime.TryParseExact(dates[0], "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out d1)) {
                    return false;
                }

                if (!DateTime.TryParseExact(dates[1], "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out d2)) {
                    return false;
                }

                result.StartDate = d1;
                result.EndDate = d2;
                return true;
            }

            return false;
        }

        public static DateTimePeriod Parse(string s) {
            DateTimePeriod result = new DateTimePeriod();

            if (string.IsNullOrWhiteSpace(s)) {
                // Deviating from convention. If a empty or null is parsed, don't throw 
                // an exception. This will ease the programming load of not using TryParse
                // everytime when setting DateTimePeriod properties.
                return result;
            }

            // DT
            if (s.Length == 4) {
                TimeSpan ts;

                if (!TimeSpan.TryParseExact(s, "hhmm",null, System.Globalization.TimeSpanStyles.None, out ts)) {
                    throw new FormatException(string.Format("Invalid DateTimePeriod format: {0}", s));
                }

                result.Time = ts;
                return result;
            }

            var dates = s.TrimEnd('-').Split('-');

            // D8
            if (dates.Length == 1) {
                DateTime d1;

                if (!DateTime.TryParseExact(dates[0], "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out d1)) {
                    throw new FormatException(string.Format("Invalid DateTimePeriod format: {0}", s));
                }

                result.StartDate = d1;
                return result;
            }

            // RD8
            if (dates.Length == 2) {
                DateTime d1;
                DateTime d2;

                if (!DateTime.TryParseExact(dates[0], "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out d1)) {
                    throw new FormatException(string.Format("Invalid DateTimePeriod format: {0}", s));
                }

                if (!DateTime.TryParseExact(dates[1], "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out d2)) {
                    throw new FormatException(string.Format("Invalid DateTimePeriod format: {0}", s));
                }

                result.StartDate = d1;
                result.EndDate = d2;
                return result;
            }

            throw new FormatException(string.Format("Invalid DateTimePeriod format: {0}", s));
        }
    }
}
