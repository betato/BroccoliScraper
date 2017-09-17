using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodCommon
{
    public class Util
    {

        public enum QuantityType
        {
            Fraction,
            Number,
            None
        }

        public static QuantityType ParseQuantity(string text, out float quantity)
        {
            if (text == "A" || text == "a")
            {
                quantity = 1.0f;
                return QuantityType.Number;
            }
            if (float.TryParse(text, out float result))
            {
                quantity = result;
                return QuantityType.Number;
            }
            else if (text.Contains('/'))
            {
                var fractionSplit = text.Split('/');
                if (float.TryParse(fractionSplit[0], out float numerator) && float.TryParse(fractionSplit[1], out float denominator))
                {
                    quantity = numerator / denominator;
                    return QuantityType.Fraction;
                }
            }
            quantity = float.NaN;
            return QuantityType.None;
        }
    }
}
