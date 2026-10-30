namespace dgAguirreExamen.Views;

public partial class vLogin : ContentPage
{
    string[,] usuarios = {
        { "recepcion",  "gym2026"  },
        { "entrenador", "fit_01"   },
        { "uisrael",    "2026"     },
        { "gerente",    "admin123" }
    };

    public vLogin()
    {
        InitializeComponent();
    }

    private async void btnIniciar_Clicked(object sender, EventArgs e)
    {
        string usuario = txtUsuario.Text;
        string contrasena = txtContrasena.Text;
        bool acceso = false;

        for (int i = 0; i < 4; i++)
        {
            if (usuarios[i, 0] == usuario && usuarios[i, 1] == contrasena)
            {
                acceso = true;
            }
        }

        if (acceso)
        {
            await Navigation.PushAsync(new vRegistro(usuario));
        }
        else
        {
            await DisplayAlertAsync("Error", "Usuario o contraseña incorrectos", "OK");
        }
    }

    private async void btnAcercaDe_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new vAcercaDe());
    }
}