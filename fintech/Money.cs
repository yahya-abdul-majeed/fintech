using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fintech
{
    public class Money : IEquatable<Money>, IComparable<Money>
    {
        //fields
        private int sign = 1;
        private int money_whole_part;
        private double money_frac_part;

        //constructors

        public Money()//C1
        {
            Random random = new Random();
            money_whole_part = random.Next();
            money_frac_part = random.NextDouble();
        }
        public Money(int sign, int money_whole_part, double money_frac_part)//C2
        {
            this.sign = sign;
            this.money_whole_part = money_whole_part;
            this.money_frac_part = money_frac_part;
        }
        public Money(Money money)//C3
        {
            sign = money.sign;
            money_whole_part = money.money_whole_part;
            money_frac_part = money.money_frac_part;
        }
        public Money(string numStr)//C4
        {
            strToNum(numStr);
        }

        //properties
        public int Sign//AM1
        {
            get { return sign; }
            set { sign = value; }
        }
        public int Money_whole_part//AM2
        {
            get { return money_whole_part; }
            set { money_whole_part = value; }
        }
        public double Money_frac_part//AM3
        {
            get { return money_frac_part; }
            set { money_frac_part = value; }
        }

        //methods
        public void DisplayNumAsStr()//MD
        {
            double wholeNum = sign * (money_whole_part + money_frac_part);
            Console.WriteLine(wholeNum.ToString());
        }
        public void setSign(int sign)//MS1
        {
            this.sign = sign;
        }
        public void setMoney_whole_part(int money_whole_part)//MS2
        {
            this.money_whole_part = money_whole_part;
        }
        public void setMoney_frac_part(double money_frac_part)//MS3
        {
            this.money_frac_part = money_frac_part;
        }
        public void strToNum(string numStr)//MS4
        {
            double num = double.Parse(numStr);
            if (num < 0)
            {
                sign = -1;
                num *= sign;
            }
            else
            {
                sign = 1;
            }

            money_whole_part = Convert.ToInt32(Math.Floor(num));
            money_frac_part = num - money_whole_part;

        }
        public void addValue(int sign, int money_whole_part, double money_frac_part)//MAdd1
        {
            double num = sign * (money_whole_part + money_frac_part);
            double org_num = this.sign * (this.money_whole_part + this.money_frac_part);
            double total = num + org_num;
            strToNum(total.ToString());
        }
        public void addMoney(Money money)//MAdd2
        {
            double org_num = sign * (money_whole_part + money_frac_part);
            double ext_num = money.sign * (money.money_whole_part + money.money_frac_part);
            double total = org_num + ext_num;
            strToNum(total.ToString());
        }
        public void subtractValue(int sign, int money_whole_part, double money_frac_part)//MSub1
        {
            double num = sign * (money_whole_part + money_frac_part);
            double org_num = this.sign * (this.money_whole_part + this.money_frac_part);
            double total = org_num - num;
            strToNum(total.ToString());
        }
        public void subtractMoney(Money money)//MSub2
        {
            double org_num = sign * (money_whole_part + money_frac_part);
            double ext_num = money.sign * (money.money_whole_part + money.money_frac_part);
            double total = org_num - ext_num;
            strToNum(total.ToString());
        }

        public bool Equals(Money other)//MEq
        {
            if (other == null)
                return false;
            if (sign == other.sign &&
               money_frac_part == other.money_frac_part &&
               money_whole_part == other.money_whole_part)
                return true;
            else
                return false;
        }

        public int CompareTo(Money other)//MComp
        {
            double org_num = sign * (money_whole_part + money_frac_part);
            double com_num = other.sign * (other.money_whole_part + other.money_frac_part);

            return org_num.CompareTo(com_num);
        }

        public Money addTwoMoneyObjects(Money money1, Money money2)//MSum
        {
            Money result = new Money();
            double money1num = money1.sign * (money1.money_whole_part + money1.money_frac_part);
            double money2num = money2.sign * (money2.money_whole_part + money2.money_frac_part);
            double res = money1num + money2num;
            if (res < 0)
            {
                result.sign = -1;
                res *= result.sign;
            }
            else
            {
                result.sign = 1;
            }
            result.money_whole_part = Convert.ToInt32(res);
            result.money_frac_part = res - result.money_whole_part;
            return result;
        }
        public Money subtractTwoMoneyObjects(Money money1, Money money2)//MDif
        {
            Money result = new Money();
            double money1num = money1.sign * (money1.money_whole_part + money1.money_frac_part);
            double money2num = money2.sign * (money2.money_whole_part + money2.money_frac_part);
            double res = money1num - money2num;
            if (res < 0)
            {
                result.sign = -1;
                res *= result.sign;
            }
            else
            {
                result.sign = 1;
            }
            result.money_whole_part = Convert.ToInt32(res);
            result.money_frac_part = res - result.money_whole_part;
            return result;
        }
        public Money multiplyBy(float multiplier)//MMul
        {
            Money result = new Money();
            double num = sign * (money_whole_part + money_frac_part);
            double res = num * multiplier;

            if (res < 0)
            {
                result.sign = -1;
                res *= -1;
            }
            else
            {
                sign = 1;
            }
            result.money_whole_part = Convert.ToInt32(res);
            result.money_frac_part = res - result.money_whole_part;
            return result;
        }
        public Money divideBy(float divisor)//MDiv1
        {
            Money result = new Money();
            double num = sign * (money_whole_part + money_frac_part);
            double res = num / divisor;

            if (res < 0)
            {
                result.sign = -1;
                res *= -1;
            }
            else
            {
                sign = 1;
            }
            result.money_whole_part = Convert.ToInt32(res);
            result.money_frac_part = res - result.money_whole_part;
            return result;
        }
        public float divideMoneybyMoney(Money divisor)//MDiv2
        {
            double org_num = sign * (money_frac_part + money_whole_part);
            double n_num = divisor.sign * (divisor.money_whole_part + divisor.money_frac_part);
            return (float)(org_num / n_num);

        }
    }
}
