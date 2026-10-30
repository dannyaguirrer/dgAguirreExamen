namespace dgAguirreExamen.Views;

public partial class vResumen : ContentPage
{
    public vResumen(string usuario, string nombre, string apellido, int edad, string fecha,
                    string plan, string casillero, double monto, double pago, double total)
    {
        InitializeComponent();

        lblUsuario.Text = "Usuario conectado: " + usuario;
        lblNombre.Text = nombre;
        lblApellido.Text = apellido;
        lblEdad.Text = edad.ToString();
        lblFecha.Text = fecha;
        lblPlan.Text = plan;
        lblCasillero.Text = casillero;
        lblMonto.Text = monto.ToString("0.00");
        lblPago.Text = pago.ToString("0.00");
        lblTotal.Text = total.ToString("0.00");
    }

    private async void btnExportar_Clicked(object sender, EventArgs e)
    {
        string nombreArchivo = "socio_" + lblNombre.Text + "_" + lblApellido.Text + ".txt";
        string ruta = Path.Combine(FileSystem.AppDataDirectory, nombreArchivo);

        string contenido = "RESUMEN DEL SOCIO\n"
            + lblUsuario.Text + "\n"
            + "Nombre: " + lblNombre.Text + "\n"
            + "Apellido: " + lblApellido.Text + "\n"
            + "Edad: " + lblEdad.Text + "\n"
            + "Fecha de inicio: " + lblFecha.Text + "\n"
            + "Plan: " + lblPlan.Text + "\n"
            + "Casillero: " + lblCasillero.Text + "\n"
            + "Monto Inicial: " + lblMonto.Text + "\n"
            + "Pago mensual: " + lblPago.Text + "\n"
            + "Pago Total: " + lblTotal.Text;

        File.WriteAllText(ruta, contenido);

        await DisplayAlertAsync("Archivo generado", "Nombre: " + nombreArchivo + "\nRuta: " + ruta, "OK");
    }

   

    private void btnCerrar_Clicked(object sender, EventArgs e)
    {
        Application.Current!.Windows[0].Page = new NavigationPage(new vLogin());
    }
}