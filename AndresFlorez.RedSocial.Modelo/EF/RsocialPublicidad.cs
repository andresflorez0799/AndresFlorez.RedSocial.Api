using System;
using System.Collections.Generic;

#nullable disable

namespace AndresFlorez.RedSocial.Modelo.EF
{
    public partial class RsocialPublicidad : Entidad
    {
        public string Informacion { get; set; }
        public byte[] Banner { get; set; }
        public DateTime? FechaFinalizacion { get; set; }
        public bool? IsActivo { get; set; }
        public string VinculoComercial { get; set; }
        public DateTime? FechaCreacion { get; set; }
    }
}
