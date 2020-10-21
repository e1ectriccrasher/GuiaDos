using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Odbc;

namespace GuiaDos
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           

        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            String CU = TextBoxCU.Text;

            String passwd = TextBoxPWD.Text;




            // Alumno
            String Alumnoquery = "select Alumno.CU, Alumno.passwd from Alumno where CU = ? and passwd = ? ";
            ConexionBD alumnoConexion = new ConexionBD();
            OdbcConnection conLoginAlumno = alumnoConexion.conexion;
            OdbcCommand Alumnocomando = new OdbcCommand(Alumnoquery, conLoginAlumno);
            Alumnocomando.Parameters.AddWithValue("CU", CU);
            Alumnocomando.Parameters.AddWithValue("passwd", passwd);
            OdbcDataReader Alumnolector = Alumnocomando.ExecuteReader();
            


            if (Alumnolector.HasRows == true)
            {
                Label1.Text = "Credenciales correctas";
                
                Alumnolector.Read(); 
                String s = Alumnolector.GetString(0);
                String d = Alumnolector.GetString(1);
                                                
                                                
                Session["CU"] = s;
                Session["passwdAl"] = d;
                Session.Timeout = 15;
                Response.Redirect("AlumnoPreguntasRespondidas.aspx");
                
            }
            
            else
            {//No puso bien sus credenciales, no lo dejamos pasar
                Label1.Text = "Credenciales incorrectas";
            }
            conLoginAlumno.Close();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            String cProf = TextBoxCU.Text;

            String passwd = TextBoxPWD.Text;

            //Profesor 

            ConexionBD profesorConexion = new ConexionBD();
            OdbcConnection conProfesor = profesorConexion.conexion;
            String ProfesorQuery = " select Profesor.cProf, Profesor.passwd from Profesor where cProf = ? and passwd = ? ";
            OdbcCommand ProfesorComando = new OdbcCommand(ProfesorQuery, conProfesor);
            ProfesorComando.Parameters.AddWithValue("cProf", cProf);
            ProfesorComando.Parameters.AddWithValue("passwd", passwd);
            OdbcDataReader ProfesorLector = ProfesorComando.ExecuteReader();

            if (ProfesorLector.HasRows== true)
            {
                Label1.Text = "Credenciales correctas";

                ProfesorLector.Read();
                String c = ProfesorLector.GetString(0);
                String v = ProfesorLector.GetString(1);


                Session["cProf"] = c;
                Session["paswdProf"] = v;
                Session.Timeout = 15;

                Response.Redirect("ProfesorPreguntasResponder.aspx");
            }
            else
            {//No puso bien sus credenciales, no lo dejamos pasar
                Label1.Text = "Credenciales incorrectas";
            }
        }
    }
}
