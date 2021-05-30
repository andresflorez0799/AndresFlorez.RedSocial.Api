using System;
using System.Collections.Generic;

#nullable disable

namespace AndresFlorez.RedSocial.Modelo.EF
{
    public partial class RsocialBitacoraErrore : Entidad
    {
        public string ErrorMessage { get; set; }
        public string ErrorType { get; set; }
        public string ErrorInnerException { get; set; }
        public string ErrorStackTrace { get; set; }
        public string ErrorSource { get; set; }
        public string ErrorTargetSite { get; set; }
        public DateTime ErrorTimeStamp { get; set; }
    }
}
