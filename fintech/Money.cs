


using System.ComponentModel;
using System.Text.RegularExpressions;

namespace fintech
{
    //public enum Currency{
    //    USD,
    //    EUR,
    //    RUB
    //}
    public class Money:IComparable<Money>,IEquatable<Money>
    {
        public int sign { get; private set; } = 1;
        public uint integer { get; private set; }    
        public uint fractional { get; private set; } 
        public Currency currency { get; private set; }  

        public Money()
        {
            Random random = new Random();   
            var currencies = Enum.GetValues<Currency>();
            this.currency = (Currency)currencies.GetValue(random.Next(currencies.Length));
            this.integer = (uint)random.Next(1000);
            this.fractional = (uint)random.Next(0,100);
        }

        public Money(int sign, uint integer,uint fractional, Currency currency)
        {
            this.sign = sign;
            this.integer = integer;
            this.fractional = fractional;
            this.currency = currency;
        }

        public Money(Money m)
        {
            this.sign = m.sign;
            this.integer = m.integer;
            this.fractional = m.fractional;
            this.currency = m.currency;
        }

        public Money(string num)
        {
            setWholeNum(num);
        }
        public string DisplayNumAsStr()
        {
            string str = string.Empty;
            str += sign == 1 ? "" : "-";

            return str + integer.ToString() + "." + fractional.ToString();
        }

        public bool setSign(int sign)
        {
            if(sign ==1 || sign == -1){
                this.sign = sign;
                return true;
            }
            return false;
        }
        public bool setInteger(uint integer)
        {
            this.integer = integer;
            return true;
        }
        public bool setFractional(uint fractional)
        {
            if(fractional >=0 && fractional < 100)
            {
                this.fractional = fractional;
                return true;
            }
            return false;
        }
        public void setCurrency(Currency currency)
        {
            this.currency= currency;
        }
        public bool setWholeNum(string num)
        {            
            Regex moneyReg = new Regex(@"^[-+]?\d+(\.\d+)?$");
            Regex integerReg = new Regex(@"\d+(?=\.)");
            Regex fractionalReg = new Regex(@"(?<=\.)\d+");
            if (moneyReg.IsMatch(num))
            {
                uint fractionalPart = uint.Parse(fractionalReg.Match(num).Value);
                if (fractionalPart>=0 && fractionalPart < 100)
                {
                    this.fractional = fractionalPart;
                }
                else
                {
                    return false;
                }
                this.integer = uint.Parse(integerReg.Match(num).Value);
                if (num[0] == '-')
                {
                    this.sign = -1;
                }
                else
                {
                    this.sign = 1;
                }
                return true;
            }
            return false;
        }

        public void addValue(int sign, uint integer, uint fractional)
        {
            Money money = new Money(sign, integer, fractional, this.currency);
            addMoney(money);
        }

        public void addMoney(Money m)
        {
            if(this.sign == m.sign)
            {
                uint num1 = convert2cents(this.integer, this.fractional);
                uint num2 = convert2cents(m.integer, m.fractional);

                uint sum = num1 + num2;
                this.integer = sum / 100;
                this.fractional = sum % 100;
            }
            else
            {
                var signThis = this.sign;
                var signM = m.sign;

                this.sign = 1;
                m.sign = 1;

                var result = this.CompareTo(m);
                if (result == 1)
                {
                    this.sign = signThis;
                }
                else if (result == -1)
                {
                    this.sign = signM;
                }
                else
                {
                    sign = 1;
                }


                uint num1 = convert2cents(this.integer, this.fractional);
                uint num2 = convert2cents(m.integer, m.fractional);

                uint diff = num1> num2? num1 - num2: num2 - num1;
                this.integer = diff / 100;
                this.fractional = diff % 100;
            }
        }

        public void subtractValue(int sign, uint integer, uint fractional)
        {
            Money money = new Money(sign,integer, fractional,this.currency);
            subtractMoney(money); ;
        }

        public void subtractMoney(Money m)
        {
            if (this.sign == m.sign)
            {
                var tempsign = this.sign;

                this.sign = 1;
                m.sign = 1;

                var result = this.CompareTo(m); 
                if(result == 1)
                {
                    this.sign = tempsign;
                }else if(result == -1)
                {
                    this.sign = tempsign * -1;
                }
                else
                {
                    this.sign = 1;
                }

                uint num1 = convert2cents(this.integer, this.fractional);
                uint num2 = convert2cents(m.integer, m.fractional);
                
                uint diff = num1 > num2 ? num1 - num2 : num2 - num1;
                this.integer = diff / 100;
                this.fractional = diff % 100;
            }
            else
            {

                uint num1 = convert2cents(this.integer, this.fractional);
                uint num2 = convert2cents(m.integer, m.fractional);

                uint sum = num1 + num2;
                this.integer = sum / 100;
                this.fractional = sum % 100;

            }
        }

        public static Money add2Monies(Money one, Money two)
        {
            one.addMoney(two);
            return one;
        }

        public static Money subtract2Monies(Money one, Money two)
        {
            one.subtractMoney(two);
            return one;
        }

        public void convertTo(Currency currency)
        {
            Dictionary<Tuple<Currency, Currency>, double> conversionRates = new()
            {
                {Tuple.Create(Currency.EUR,Currency.USD) , 1.08 },
                {Tuple.Create(Currency.EUR,Currency.RUB) , 74.80 },
                {Tuple.Create(Currency.USD,Currency.EUR) , 0.92 },
                {Tuple.Create(Currency.USD,Currency.RUB) , 69.17 },
                {Tuple.Create(Currency.RUB,Currency.EUR) , 0.013 },
                {Tuple.Create(Currency.RUB,Currency.USD) , 0.014 },
                {Tuple.Create(Currency.RUB,Currency.RUB) , 1 },
                {Tuple.Create(Currency.USD,Currency.USD) , 1 },
                {Tuple.Create(Currency.EUR,Currency.EUR) , 1 }
            };

            var balance = convert2cents(this.integer, this.fractional);
            balance = (uint)(balance * conversionRates[Tuple.Create(this.currency, currency)]);
            this.currency = currency;
            this.integer = balance / 100;
            this.fractional = balance % 100;

        }

        //utility methods
        private uint convert2cents(uint integer, uint fractional)
        {
            return integer * 100 + fractional;
        }

        public int CompareTo(Money other)
        {
            if(this.sign != other.sign)
            {
                return this.sign.CompareTo(other.sign);
            }

            if(this.integer != other.integer)
            {
                return this.integer.CompareTo(other.integer);
            }

            return this.fractional.CompareTo(other.fractional);
            
        }

        public bool Equals(Money? other)
        {
            if (this.sign == other.sign && this.integer == other.integer
                && this.fractional == other.fractional && this.currency == other.currency)
            {
                return true;
            }
            return false;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;
            Money moneyObj = obj as Money;
            if(moneyObj == null)
            {
                return false;
            }
            else
            {
                return this.Equals(moneyObj);
            }
        }

        public static bool operator  == (Money? left, Money? right)
        {
            return left.CompareTo(right) == 0;
        }

        public static bool operator !=(Money? left, Money? right)
        {
            return left.CompareTo(right) != 0;
        }
    }
}