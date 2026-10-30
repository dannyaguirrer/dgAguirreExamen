namespace dgAguirreExamen.Views;

public partial class vRegistro : ContentPage
{
    string usuario;

    public vRegistro(string usuario)
    {
        InitializeComponent();
        this.usuario = usuario;
        lblUsuario.Text = "Usuario conectado: " + usuario;

        dpFecha.MinimumDate = DateTime.Today;

        pkPlan.Items.Add("Musculación");
        pkPlan.Items.Add("Cardio");
        pkPlan.Items.Add("Crossfit");
        pkPlan.Items.Add("Full");
    }

    private async Task<bool> Validar()
    {
        if (string.IsNullOrWhiteSpace(txtNombre.Text))
        {
            await DisplayAlertAsync("Error", "Ingrese el nombre", "OK");
            return false;
        }

        if (string.IsNullOrWhiteSpace(txtApellido.Text))
        {
            await DisplayAlertAsync("Error", "Ingrese el apellido", "OK");
            return false;
        }

        int edad;
        if (!int.TryParse(txtEdad.Text, out edad) || edad < 16)
        {
            await DisplayAlertAsync("Error", "La edad debe ser un numero entero mayor o igual a 16", "OK");
            return false;
        }

        if (pkPlan.SelectedIndex == -1)
        {
            await DisplayAlertAsync("Error", "Seleccione un plan", "OK");
            return false;
        }

        double monto;
        if (!double.TryParse(txtMonto.Text, out monto) || monto < 120 || monto > 600)
        {
            await DisplayAlertAsync("Error", "El monto inicial debe ser un numero entre 120 y 600", "OK");
            return false;
        }

        return true;
    }

    private async void btnCalcular_Clicked(object sender, EventArgs e)
    {
        if (!await Validar())
        {
            return;
        }

        double monto = double.Parse(txtMonto.Text);
        double cuota = (1200 - monto) / 5 + 1200 * 0.02;

        if (swCasillero.IsToggled)
        {
            cuota = cuota + 10;
        }

        txtPago.Text = cuota.ToString("0.00");
    }

    private async void btnResumen_Clicked(object sender, EventArgs e)
    {
        if (!await Validar())
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(txtPago.Text))
        {
            await DisplayAlertAsync("Error", "Primero calcule el pago mensual", "OK");
            return;
        }

        double monto = double.Parse(txtMonto.Text);
        double cuota = (1200 - monto) / 5 + 1200 * 0.02;
        if (swCasillero.IsToggled)
        {
            cuota = cuota + 10;
        }
        double total = monto + cuota * 5;

        string casillero = lblCasillero.Text;

        string nombre = txtNombre.Text;
        string apellido = txtApellido.Text;
        int edad = int.Parse(txtEdad.Text);
        DateTime fecha = (DateTime)dpFecha.Date;
        string plan = pkPlan.SelectedItem.ToString();

        txtNombre.Text = "";
        txtApellido.Text = "";
        txtEdad.Text = "";
        dpFecha.Date = DateTime.Today;
        pkPlan.SelectedIndex = -1;
        swCasillero.IsToggled = false;
        txtMonto.Text = "";
        txtPago.Text = "";

        await Navigation.PushAsync(new vResumen(usuario, nombre, apellido, edad,
            fecha.ToString("dd/MM/yyyy"), plan, casillero, monto, cuota, total));
    }
    private void swCasillero_Toggled(object sender, ToggledEventArgs e)
    {
        if (e.Value)
        {
            lblCasillero.Text = "Sí";
        }
        else
        {
            lblCasillero.Text = "No";
        }

        txtPago.Text = "";
    }
}