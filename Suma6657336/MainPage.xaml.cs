namespace Suma6657336
{
    public partial class MainPage : ContentPage
    {

        private readonly LocalDbService _dbService;
        private int _editResultadoId;

        public MainPage(LocalDbService dbService)
        {
            _dbService = dbService;
            InitializeComponent();
            Task.Run(async () => listview.ItemsSource = await _dbService.GetResutado());
        }

        private async void sumarbutton_Clicked(object sender, EventArgs e)
        {
            if (double.TryParse(entryprimernumero.Text, out double number1) && double.TryParse(entrysegundonumero.Text, out double number2))
            {
                double result = number1 + number2;
                labelresultado.Text = result.ToString();



                if (_editResultadoId == 0)
                {
                    await _dbService.Create(new Resutado
                    {
                        Numero1 = entryprimernumero.Text,
                        Numero2 = entrysegundonumero.Text,
                        Suma = labelresultado.Text,

                        //(PRUEBA
                        //Suma = entryprimernumero.Text + entrysegundonumero.Text,
                        //Suma = labelresultado.Text,)
                    });
                }
                else
                {
                    await _dbService.Update(new Resutado
                    {
                        Id = _editResultadoId,
                        Numero1 = entryprimernumero.Text,
                        Numero2 = entrysegundonumero.Text,
                        Suma = labelresultado.Text,
                    });
                    _editResultadoId = 0;
                }
                entryprimernumero.Text = string.Empty;
                entrysegundonumero.Text = string.Empty;
                labelresultado.Text = string.Empty;

                listview.ItemsSource = await _dbService.GetResutado();
            }
            else
            {
                labelresultado.Text = "Entrada inválida";
            }
        }

        private async void listview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var resultado = (Resutado)e.Item;
            var action = await DisplayActionSheet("Action" , "Cancel" , null , "Edit" , "Delete");

            switch (action)
            {
                case "Edit":
                    _editResultadoId = resultado.Id;
                    entryprimernumero.Text = resultado.Numero1;
                    entrysegundonumero.Text = resultado.Numero1;
                    labelresultado.Text = resultado.Suma;
                    break;

                case "Delete":
                    await _dbService.Delete(resultado);
                    listview.ItemsSource = await _dbService.GetResutado();
                    break;
            }
        }

    }

}
