using BoteForms.Data;
using BoteForms.modelo;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace BoteForms.Util
{
    public class GuardarListaBBDD
    {
        private _Default instanciaDefault;

        public GuardarListaBBDD(_Default defaultInstance = null)
        {
            instanciaDefault = defaultInstance;
        }


        public bool GuardarUsuarioActivo(List<Trabajador> trabajadores)
        {
            using (var db = new AppDbContext())
            {
                foreach (var trabajador in trabajadores)
                {
                    db.Trabajadores.AddOrUpdate(t => new { t.Nombre, t.UsuarioID }, trabajador);
                }

                db.SaveChanges();
            }
            return true;
        }

        public bool GuardarListaTemporal(List<Trabajador> listaTrabajadores, HttpSessionState session, int userId)
        {
            {
                using (var db = new AppDbContext())
                {
                    _Default instancia = new _Default();

                    foreach (var trabajadorTemporal in listaTrabajadores)
                    {
                        var trabajador = new Trabajador
                        {
                            UsuarioID = userId,
                            Nombre = trabajadorTemporal.Nombre,
                            Horas = trabajadorTemporal.Horas,
                            UltimoBote = trabajadorTemporal.UltimoBote,
                            BoteAcumulado = instancia.BoteAcumulado(trabajadorTemporal.UltimoBote, trabajadorTemporal.Nombre),
                        };

                        db.Trabajadores.AddOrUpdate(t => new { t.Nombre, t.UsuarioID }, trabajador);
                    }

                    db.SaveChanges();
                }
                session.Remove("Trabajadores"); // Limpiar los datos de la sesión una vez que se han guardado 
                return true;
            }
        }
    }
}
