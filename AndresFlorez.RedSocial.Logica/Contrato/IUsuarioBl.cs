using AndresFlorez.RedSocial.Modelo.EF;

namespace AndresFlorez.RedSocial.Logica.Contrato
{
    public interface IUsuarioBl
    {
        RsocialUsuario GetByEmail(string email);
    }
}
