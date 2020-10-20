using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Odbc;

namespace GuiaDos
{
    public class ConexionBD
    {
        //Atributo conexion a base de datos
        public OdbcConnection conexion { get; set; }

        //Constructor para generar la conexion a la base de datos
        public ConexionBD()
        {
            //Extraer las caracteristicas de conexion del Web.config
            //Objeto para abrir el web.config
            System.Configuration.Configuration webConfig;
            //Abrir el web.config y guardarlo en el objeto
            //ProyectoClase032020 <-- nombre de mi proyecto
            webConfig = System.Web.Configuration
                .WebConfigurationManager
                .OpenWebConfiguration("/GuiaDos");
            //Sacar el string de conexion del web.config
            //BDEncuestas <-- nombre del string de conexion en el Web.Config
            System.Configuration.ConnectionStringSettings objetoStringDeConexion;
            objetoStringDeConexion = webConfig
                                    .ConnectionStrings
                                    .ConnectionStrings["BDGuiaDos"];
            //Crear la nueva conexion
            conexion = new OdbcConnection(objetoStringDeConexion.ToString());
            //Abrir la conexion para que la aplicacion la pueda usar
            conexion.Open();
        }
    }
}