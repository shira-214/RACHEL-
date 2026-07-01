using System;
using WpfRentingApartementRacheli.ServiceReference1;

namespace WpfRentingApartementRacheli
{
    internal static class Global
    {
        public enum UserRole
        {
            None,
            Hirer,
            Owner,
            Manager
        }

        public static UserRole CurrentRole { get; set; } = UserRole.None;
        public static DTOHirers CurrentHirer { get; set; }
        public static DTOApartments CurrentOwner { get; set; }
        public static DateTime SelectedDate { get; set; }
        public static bool isManager = false;

        public static DTOHirers currentHirers
        {
            get { return CurrentHirer; }
            set
            {
                CurrentHirer = value;
                if (value != null && !isManager)
                    CurrentRole = UserRole.Hirer;
            }
        }

        public static DTOApartments currentApartments
        {
            get { return CurrentOwner; }
            set
            {
                CurrentOwner = value;
                if (value != null)
                    CurrentRole = UserRole.Owner;
            }
        }

        public static DateTime selectedDate
        {
            get { return SelectedDate; }
            set { SelectedDate = value; }
        }

        public static void Logout()
        {
            CurrentRole = UserRole.None;
            CurrentHirer = null;
            CurrentOwner = null;
            SelectedDate = default(DateTime);
            isManager = false;
        }
    }
}
