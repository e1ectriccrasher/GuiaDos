using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Odbc;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GuiaDos
{
    public partial class ProfesorPreguntasResponder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cProf"] == null  || Session["paswdProf"]==null || Session["CU"]!=null)
            {//Si alguna de las variables de sesion es null,
                Session.Clear(); //<-- borra todas las variables de sesion
                Session.Abandon(); //<-- lo mismo que clear, pero borra
                                   //todos los parametros de sesion
                                   //regresas al usuario al login
                Response.Redirect("login.aspx");
            }

            String DropDownList1Query = " select Pregunta.cPreg, Pregunta.contenido from Pregunta inner join Profesor on Pregunta.cProf=Profesor.cProf where Profesor.cProf = ? and Pregunta.cPreg in (select Respuesta.cPreg from Respuesta) ";
            OdbcConnection DropDownList1Con = new ConexionBD().conexion;
            OdbcCommand DropDownList1Comando = new OdbcCommand(DropDownList1Query, DropDownList1Con);
            DropDownList1Comando.Parameters.AddWithValue("cProf", Session["cProf"].ToString());
            OdbcDataReader DropDownList1Lector = DropDownList1Comando.ExecuteReader();

            if (DropDownList1.Items.Count == 0)
            {
                DropDownList1.DataSource = DropDownList1Lector;
                DropDownList1.DataTextField = "contenido";
                DropDownList1.DataValueField = "cPreg";
                DropDownList1.DataBind();
            }
            DropDownList1Con.Close();

            String DropDownList2Query = " select Pregunta.cPreg, Pregunta.contenido from Pregunta inner join Profesor on Pregunta.cProf=Profesor.cProf where Profesor.cProf = ? and Pregunta.cPreg not in (select Respuesta.cPreg from Respuesta) ";
            OdbcConnection DropDownList2Con = new ConexionBD().conexion;
            OdbcCommand DropDownList2Comando = new OdbcCommand(DropDownList2Query, DropDownList2Con);
            DropDownList2Comando.Parameters.AddWithValue("cProf", Session["cProf"].ToString());
            OdbcDataReader DropDownList2Lector = DropDownList2Comando.ExecuteReader();

            if (DropDownList2.Items.Count == 0)
            {
                DropDownList2.DataSource = DropDownList2Lector;
                DropDownList2.DataTextField = "contenido";
                DropDownList2.DataValueField = "cPreg";
                DropDownList2.DataBind();
            }
            DropDownList2Con.Close();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // DropDownList1

            String Buton1Query = " select Respuesta.cRes, Respuesta.contenido from Respuesta inner join Pregunta on Respuesta.cPreg = Pregunta.cPreg where Pregunta.cPreg = ? ";
            OdbcConnection Buton1Con = new ConexionBD().conexion;
            OdbcCommand Buton1Comando = new OdbcCommand(Buton1Query, Buton1Con);
            Buton1Comando.Parameters.AddWithValue("cPreg", DropDownList1.SelectedValue.ToString());
            OdbcDataReader Buton1Lector = Buton1Comando.ExecuteReader();
            if (Buton1Lector.Read())
            {
                Label1.Text = Buton1Lector.GetString(1);
            }
            Buton1Con.Close();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int key = rnd.Next(10000000);
            String contenido = TextBox1.Text;
            String fecha = DateTime.Now.ToString("yyyy-MM-dd");
            String Buton2Query = " insert into respuesta values(?, ?, ?, ?) ";
            OdbcConnection Buton2Con = new ConexionBD().conexion;
            OdbcCommand Buton2Comando = new OdbcCommand(Buton2Query, Buton2Con);
            Buton2Comando.Parameters.AddWithValue("cRes", key);
            Buton2Comando.Parameters.AddWithValue("contenido", contenido);
            Buton2Comando.Parameters.AddWithValue("fecha", fecha);
            Buton2Comando.Parameters.AddWithValue("cPreg", DropDownList2.SelectedValue.ToString());
            Buton2Comando.ExecuteNonQuery();
            Buton2Con.Close();
            Response.Redirect("ProfesorPreguntasResponder.aspx");

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("login.aspx");
        }
    }
}