using System;

namespace Zadatak_1.Calculations
{
    class Calculation
    {
        /// <summary>
        /// This method checks if passed jmbg contains a valid date and if date valid calculates birth date.
        /// </summary>
        /// <param name="JMBG">JMBG.</param>
        /// <param name="dateOfBirth">Output birth date.</param>
        /// <returns>True if user has more than 15 years, false if jmbg is not valid or user has less than 15 years.</returns>
        public bool CalculateDateOfBirth(string JMBG, out DateTime dateOfBirth)
        {
            try
            {
                string day = JMBG.Substring(0, 2);
                string month = JMBG.Substring(2, 2);
                string year = JMBG.Substring(4, 3);
                string validyear = "";
                if (year[0] == '0')
                {
                    validyear = "2" + year;
                }
                else
                {
                    validyear = "1" + year;
                }
                bool conversionYear = Int32.TryParse(year, out int y);
                bool conversionMonth = Int32.TryParse(month, out int m);
                bool conversionDay = Int32.TryParse(day, out int d);
                //checks if passed jmbg contains a valid date
                var expectedDatetime = new DateTime(y, m, d);
                //gets a birth date from passed jmbg
                dateOfBirth = DateTime.Parse(validyear + "-" + month + "-" + day);
                //calculates age 
                int ageInDays = (DateTime.Today - dateOfBirth).Days;
                //if age less than 15 returns false, else returns true
                if (ageInDays < 5479)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            //if cannot convert to DateTime, because jmbg doesn't contain a valid date
            catch (Exception)
            {
                dateOfBirth = DateTime.Now;
                return false;
            }
        }
    }
}
