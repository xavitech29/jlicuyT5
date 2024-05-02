﻿namespace jlicuyT5.Views;
using jlicuyT5.Models;

public partial class vPerson : ContentPage
{
	public vPerson()
	{
		InitializeComponent();
        //RefreshPicker();
    }

    //Picker

    protected override void OnAppearing()
    {
        base.OnAppearing();
        RefreshPicker();
    }

    private void RefreshPicker()
    {
        if (App.personRepo != null)
        {
            var people = App.personRepo.getAllPeople();
            namePicker.ItemsSource = people;
            namePicker.ItemDisplayBinding = new Binding("Name");
        }
        else
        {
            DisplayAlert("Error", "No se pudo cargar la información de las personas. Por favor, reinicie la aplicación.", "OK");
        }
    }


    private void namePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (namePicker.SelectedIndex != -1)
        {
            Person selectedPerson = (Person)namePicker.SelectedItem;
            txtNewName.Text = selectedPerson.Name;  // Establece el nombre en el Entry de nombre
            txtId.Text = selectedPerson.Id.ToString();  // Establece la ID en el Entry de ID
        }
    }



    //Fin Picker
    /* private void btnInsertar_Clicked(object sender, EventArgs e)
     {
         lblStatus.Text = "";
         App.personRepo.AddNewPerson(txtName.Text);
         lblStatus.Text = App.personRepo.StatusMessage;
         RefreshPicker();  // Actualiza el Picker
     }*/

    /* private async void btnInsertar_Clicked(object sender, EventArgs e)
     {
         lblStatus.Text = "";
         try
         {
             App.personRepo.AddNewPerson(txtName.Text);  // Asumiendo que este método es síncrono
             lblStatus.Text = App.personRepo.StatusMessage;

             // Asumiendo que RefreshPicker es síncrono
             RefreshPicker();

             // Muestra un mensaje de éxito
             await DisplayAlert("Éxito", lblStatus.Text, "OK");
         }
         catch (Exception ex)
         {
             // Muestra un mensaje de error
             await DisplayAlert("Error", "Error al insertar persona: " + ex.Message, "OK");
         }
     }*/

    private async void btnInsertar_Clicked(object sender, EventArgs e)
    {
        App.personRepo.AddNewPerson(txtName.Text);
        string message = App.personRepo.StatusMessage;

        if (message.StartsWith("Error"))
        {
            await DisplayAlert("Error", message, "OK");
        }
        else
        {
            await DisplayAlert("Éxito", message, "OK");
        }

        RefreshPicker();
    }




    /* private void btnObtener_Clicked(object sender, EventArgs e)
     {
         lblStatus.Text = "";
         List<Person> people = App.personRepo.getAllPeople();
         listPerson.ItemsSource = people;
         lblStatus.Text = App.personRepo.StatusMessage;

     }*/

    private void btnObtener_Clicked(object sender, EventArgs e)
    {
        try
        {
            List<Person> people = App.personRepo.getAllPeople();
            if (people.Count > 0)
            {
                listPerson.ItemsSource = people;
            }
            else
            {
                DisplayAlert("Información", "No hay registros disponibles.", "OK");
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", "Error al obtener personas: " + ex.Message, "OK");
        }
    }


    /*private void btnActualizar_Clicked(object sender, EventArgs e)
    {
        lblStatus.Text = "";
        if (int.TryParse(txtId.Text, out int id))
        {
            string newName = txtNewName.Text;
            App.personRepo.UpdatePerson(id, newName);
            lblStatus.Text = App.personRepo.StatusMessage;
            RefreshList(); // Actualiza la lista y el picker
            RefreshPicker();
        }
        else
        {
            lblStatus.Text = "ID inválido. Por favor, seleccione un registro válido.";
        }
    }*/

    private async void btnActualizar_Clicked(object sender, EventArgs e)
    {
        if (int.TryParse(txtId.Text, out int id))
        {
            string newName = txtNewName.Text;
            App.personRepo.UpdatePerson(id, newName);
            string message = App.personRepo.StatusMessage;

            await DisplayAlert("Resultado", message, "OK");

            RefreshList(); // Actualiza la lista y el picker
            RefreshPicker();
        }
        else
        {
            await DisplayAlert("Error", "ID inválido. Por favor, seleccione un registro válido.", "OK");
        }
    }



    /*private void btnEliminar_Clicked(object sender, EventArgs e)
    {
        lblStatus.Text = "";
        if (int.TryParse(txtId.Text, out int id))
        {
            App.personRepo.DeletePerson(id);
            lblStatus.Text = App.personRepo.StatusMessage;
            RefreshList(); // Actualiza la lista y el picker
            RefreshPicker();
        }
        else
        {
            lblStatus.Text = "ID inválido. Por favor, seleccione un registro válido.";
        }
    }*/



    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        if (int.TryParse(txtId.Text, out int id))
        {
            App.personRepo.DeletePerson(id);
            string message = App.personRepo.StatusMessage;

            // Asumimos que cualquier mensaje es relevante y debe ser mostrado.
            await DisplayAlert("Resultado", message, "OK");

            // Refrescamos la lista y el picker siempre, independientemente del resultado.
            RefreshList();
            RefreshPicker();
        }
        else
        {
            await DisplayAlert("Error", "ID inválido. Por favor, seleccione un registro válido.", "OK");
        }
    }



    private void RefreshList()
    {
        List<Person> people = App.personRepo.getAllPeople();
        listPerson.ItemsSource = people;
    }


}