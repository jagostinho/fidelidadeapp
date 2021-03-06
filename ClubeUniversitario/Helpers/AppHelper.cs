﻿/*  
	This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
	Copyright Bruno Assis 2015 <bruno.assis@live.com>
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ClubeUniversitario.Helpers
{
    public static class AppHelper
    {
        private static string[] validImageExtensions = {".jpg",".jpeg",".gif",".png"};

        /// <summary>
        /// Checks if the file has a valid extension, returns true if positive.
        /// It uses AppHelper.validImageExtensions attribute if the second argument is not set.
        /// </summary>
        /// <param name="file">HttpPostedFile</param>
        /// <param name="validImageExtensions">string[]</param>
        /// <returns>bool</returns>
        public static bool isValidExtension(HttpPostedFileBase file, string[] validImageExtensions = null)
        {
            if (file == null) throw new ArgumentNullException();
            string fileExtension = Path.GetExtension(file.FileName);
            if (validImageExtensions != null)
                return AppHelper.validImageExtensions.Contains(fileExtension);
            else
                return AppHelper.validImageExtensions.Contains(fileExtension);
        }

        public static DateTime GetCurrentTime(DateTime? timeToConvert = null)
        {
            DateTime serverTime = timeToConvert ?? DateTime.Now;
            DateTime _localTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(serverTime, TimeZoneInfo.Local.Id, "E. South America Standard Time");
            return _localTime;
        }

        public static DateTime ConvertToBrazilDate(DateTime timeToConvert)
        {
            return GetCurrentTime(timeToConvert);
        }

    }
}