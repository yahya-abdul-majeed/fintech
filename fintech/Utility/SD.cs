using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace fintech.Utility
{
    public static class SD
    {
        public static Regex Phonereg = new Regex(@"^\d{10}$");
        public static Regex Emailreg = new Regex(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
        public static Regex Namereg = new Regex(@"^[a-zA-Z\s]+$");
        public static Regex Loginreg = new Regex(@"^[\w\d\-_]+$");
        public static Regex Yearreg = new Regex(@"^[\d]{4}$");
        public static Regex Bookreg = new Regex(@"^[\w\s]+$");
        public static Regex datereg = new Regex(@"\d\d\/\d\d\/\d\d\d\d");
        public static Regex MoneyReg = new Regex(@"([\d]+\.?[\d]*)||([\d]*\.?[\d]+)");
        
    }
}
