using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace WpfRentingApartementRacheli
{
    public class EmailRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string email = (string)value;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
                return ValidationResult.ValidResult;
            return new ValidationResult(false, "יש להזין כתובת אימייל תקינה");
        }
    }

    public class isHebrewOrNumbers : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string pattern = (string)value;
            Regex reg = new Regex(@"\b[א-ת-\s ]+$");
            Match match = reg.Match(pattern);
            Regex reg2 = new Regex(@"\b[0-9-\s]+$");
            Match match2 = reg2.Match(pattern);
            if (match.Success || match2.Success)
                return ValidationResult.ValidResult;
            return new ValidationResult(false, "יש להזין עברית או מספרים בלבד");
        }
    }

    public class isHebrewOrEnglish : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string pattern = (string)value;
            Regex reg = new Regex(@"\b[א-ת-\s ]+$");
            Match match = reg.Match(pattern);
            Regex reg1 = new Regex(@"\b[a-z-\s ]+$");
            Match match1 = reg1.Match(pattern);
            if (match.Success || match1.Success)
                return ValidationResult.ValidResult;
            return new ValidationResult(false, "יש להזין עברית או אנגלית בלבד");
        }
    }

    public class isHebrewRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string pattern = (string)value;
            Regex reg = new Regex(@"\b[א-ת-\s ]+$");
            Match match = reg.Match(pattern);
            if (match.Success)
                return ValidationResult.ValidResult;
            return new ValidationResult(false, "יש להזין טקסט בעברית בלבד");
        }
    }

    public class IsCellPhone : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string pattern = (string)value;
            Regex reg = new Regex(@"\b05[0 2 4 5 6 7 8 3][2-9]\d{6}$");
            Match match = reg.Match(pattern);
            if (match.Success)
                return ValidationResult.ValidResult;
            return new ValidationResult(false, "יש להזין מספר טלפון נייד תקין");
        }
    }

    public class IsCreditCard : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string pattern = (string)value;
            Regex reg = new Regex(@"\b[0-9-\s]+$");
            Match match = reg.Match(pattern);
            if (match.Success && pattern.Length == 16)
                return ValidationResult.ValidResult;
            return new ValidationResult(false, "מספר אשראי חייב להיות 16 ספרות");
        }
    }

    public class IsCvv : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string pattern = (string)value;
            Regex reg = new Regex(@"\b[0-9-\s]+$");
            Match match = reg.Match(pattern);
            if (match.Success && pattern.Length == 3)
                return ValidationResult.ValidResult;
            return new ValidationResult(false, "CVV חייב להיות 3 ספרות");
        }
    }

    public class IsNumber : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string pattern = (string)value;
            Regex reg = new Regex(@"\b[0-9-\s]+$");
            Match match = reg.Match(pattern);
            if (match.Success)
                return ValidationResult.ValidResult;
            return new ValidationResult(false, "יש להזין מספרים בלבד");
        }
    }

    public class IsId : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string s = (string)value;

            int x;
            if (!int.TryParse(s, out x))
                return new ValidationResult(false, "יש להזין מספרים בלבד");
            if (s.Length < 5 || s.Length > 9)
                return new ValidationResult(false, "תעודת זהות חייבת להיות 5–9 ספרות");

            for (int i = s.Length; i < 9; i++)
                s = "0" + s;

            int sum = 0;
            for (int i = 0; i < 9; i++)
            {
                int k = ((i % 2) + 1) * (Convert.ToInt32(s[i]) - '0');
                if (k > 9)
                    k -= 9;
                sum += k;
            }

            if (sum % 10 != 0)
                return new ValidationResult(false, "תעודת זהות לא תקינה");
            return ValidationResult.ValidResult;
        }
    }
}
