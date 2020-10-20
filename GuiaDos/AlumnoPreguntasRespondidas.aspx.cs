using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Odbc;

namespace GuiaDos
{
    public partial class AlumnoPreguntasRespondidas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CU"] == null || Session["passwdAl"] == null || Session["cProf"]!=null) 
            {//Si alguna de las variables de sesion es null,
                Session.Clear(); //<-- borra todas las variables de sesion
                Session.Abandon(); //<-- lo mismo que clear, pero borra
                                   //todos los parametros de sesion
                                   //regresas al usuario al login
                Response.Redirect("login.aspx");
            }

            Label1.Text = "Bienvenid@ " + Session["CU"];

            //Cargar encuestas
            //Este query no requiere parametros
            String Profquery = "select distinct Profesor.cProf , Profesor.nombre from Profesor inner join Pregunta on Profesor.cProf = Pregunta.cProf inner join Alumno on Pregunta.CU=Alumno.CU where Alumno.CU= ?";
            OdbcConnection Profcon = new ConexionBD().conexion;
            OdbcCommand comando = new OdbcCommand(Profquery, Profcon);
            comando.Parameters.AddWithValue("CU", Session["CU"].ToString());
            OdbcDataReader Proflector = comando.ExecuteReader();


            if (DropDownList1.Items.Count == 0)
            {
                //Cargar los datos al radiobuttonlist
                //Asigno el lector como datasource del radiobuttonlist
                DropDownList1.DataSource = Proflector;
                //Asigno la parte visible
                DropDownList1.DataTextField = "nombre"; //<-- viene del query
                //Asigno la parte no visible del query
                DropDownList1.DataValueField = "cProf"; //<-- viene del query
                //Ligar la configuracion al radiobuttonlist
                DropDownList1.DataBind();
            }
            

           
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session["cPreguntaProf"] = DropDownList1.SelectedValue;
            String PreguntasQuery = "select Pregunta.cPreg, Pregunta.contenido, Alumno.CU from Pregunta inner join Alumno on Alumno.CU=Pregunta.CU inner join Profesor on Pregunta.cProf=Profesor.cProf where Alumno.CU=?  and Profesor.cProf = ? and Pregunta.cPreg   in (select Respuesta.cPreg from Respuesta)";
            OdbcConnection PreguntasCon = new ConexionBD().conexion;
            OdbcCommand PreguntasComando = new OdbcCommand(PreguntasQuery, PreguntasCon);
            PreguntasComando.Parameters.AddWithValue("CU", Session["CU"].ToString());
            PreguntasComando.Parameters.AddWithValue("cProf", Session["cPreguntaProf"].ToString());
            OdbcDataReader PreguntasLector = PreguntasComando.ExecuteReader();
            if (RadioButtonList1.Items.Count == 0)
            {
                RadioButtonList1.DataSource = PreguntasLector;
                RadioButtonList1.DataTextField = "contenido";
                RadioButtonList1.DataValueField = "cPreg";
                RadioButtonList1.DataBind();
            
            }

            String NoPreguntasQurery = "select Pregunta.cPreg, Pregunta.contenido, Alumno.CU from Pregunta inner join Alumno on Alumno.CU=Pregunta.CU inner join Profesor on Pregunta.cProf=Profesor.cProf where Alumno.CU=?  and Profesor.cProf = ? and Pregunta.cPreg  not in (select Respuesta.cPreg from Respuesta)";
            OdbcConnection NoPreguntasCon = new ConexionBD().conexion;
            OdbcCommand  NoPreguntasComando = new OdbcCommand(NoPreguntasQurery, NoPreguntasCon);
            NoPreguntasComando.Parameters.AddWithValue("CU", Session["CU"]);
            NoPreguntasComando.Parameters.AddWithValue("cProf", Session["cPreguntaProf"]);
            OdbcDataReader NoPreguntasLector = NoPreguntasComando.ExecuteReader();
            if (ListBox1.Items.Count == 0)
            {
                ListBox1.DataSource = NoPreguntasLector;
                ListBox1.DataTextField = "contenido";
                ListBox1.DataValueField = "cPreg";
                ListBox1.DataBind();
            }

         


            {

            }
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {

            

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            String RespuestaQuery = "select Respuesta.cRes, Respuesta.contenido from Respuesta inner join Pregunta on Respuesta.cPreg=Pregunta.cPreg where Pregunta.cPreg = ?";
            OdbcConnection RespuestaCon = new ConexionBD().conexion;
            OdbcCommand RespuestaComando = new OdbcCommand(RespuestaQuery, RespuestaCon);
            RespuestaComando.Parameters.AddWithValue("cPregunta", RadioButtonList1.SelectedValue.ToString());

            OdbcDataReader RespuestaLector = RespuestaComando.ExecuteReader();
            if (RespuestaLector.Read())
            {
                Session["cRespuesta"] = RespuestaLector.GetInt32(0);
                Label2.Text = RespuestaLector.GetString(1);
            }
        }
    }
}