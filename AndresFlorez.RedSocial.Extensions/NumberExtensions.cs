using System;

namespace AndresFlorez.RedSocial.Extensions
{
    public static class NumberExtensions
    {
        public static int ToInt(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                int.TryParse(value, out int resultado);
                return resultado;
            }
            throw new Exception("Se requiere un valor valido para convertir en Entero");
        }

        public static decimal ToDecimal(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                System.Globalization.CultureInfo info = System.Globalization.CultureInfo.InstalledUICulture;
                System.Globalization.NumberFormatInfo numberFormat = (System.Globalization.NumberFormatInfo)info.NumberFormat.Clone();
                return decimal.Parse(value, numberFormat);
            }
            throw new Exception("Se requiere un valor valido para convertir en Decimal");
        }
    }
}
