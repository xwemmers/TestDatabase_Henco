Imports System.IO

Public Class Form1
    Dim context As New TrainingExamplesDataContext

    Private Sub btnGetCities_Click(sender As Object, e As EventArgs) Handles btnGetCities.Click
        Try
            Dim steden = context.Cities.OrderByDescending(Function(c) c.Population).ToList
            DataGridView1.DataSource = steden

        Catch ex As Exception
            File.AppendAllText("errorlog.txt", $"{DateTime.Now} - {ex.Message}" + Environment.NewLine + Environment.NewLine)

        End Try

        Directory.GetFiles("c:\tmp")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim query = From c In context.Cities
                    Where c.CityName.Contains(TextBox1.Text)
                    Order By c.Population Descending
                    Select c


        Dim result = query.ToList

        DataGridView1.DataSource = result
    End Sub

    Private Sub btnAddCity_Click(sender As Object, e As EventArgs) Handles btnAddCity.Click

        'Maak het nieuw City object aan (in lokaal geheugen)
        Dim city = New City
        city.CityName = TextBox2.Text
        city.Population = 10
        city.Year = 2025
        city.Country = "Belgie"

        'Voeg lokaal toe aan de verzameling steden
        context.Cities.InsertOnSubmit(city)

        'Verstuur het nieuwe record naar de DB Server
        context.SubmitChanges()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'Om de cache van de data context leeg te maken moet het object nieuw worden aangemaakt
        context = New TrainingExamplesDataContext

        DataGridView1.DataSource = context.Movies.ToList
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim m As New Movie

        m.Title = TextBox3.Text
        m.Year = 1985
        m.Duration = 200
        m.Genre = "Cartoon"
        m.Rating = 8.8

        context.Movies.InsertOnSubmit(m)
        context.SubmitChanges()

        MsgBox($"Movie {m.Title} toegevoegd met ID = {m.MovieID}")

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        context.SubmitChanges()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        Dim query = From m In context.Movies
                    Select m.MovieID, m.Title, m.Producer.Name

        DataGridView1.DataSource = query.ToList

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        Dim st As New Student

        'Open een reader naar het genoemde bestand
        Dim reader As New StreamReader("errorlog.txt")

        'Dit is hetzelfde:
        'Dim reader = My.Computer.FileSystem.OpenTextFileReader("errorlog.txt")

        'Lees regel voor regel uit het bestand
        Dim line = reader.ReadLine()

        'Stop de While loop wanneer niets gelezen is, want dan aan het einde van het bestand
        While line <> Nothing
            MsgBox(line)

            'Lees de volgende regel
            line = reader.ReadLine()
        End While

        'Vergeet niet om het bestand te sluiten
        reader.Close()

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click

        'Laat de gebruiker zelf een bestand (en folder) kiezen met een standaard dialoog:
        Dim ofd As New OpenFileDialog()
        ofd.ShowDialog()

        Dim contents = File.ReadAllText(ofd.FileName)
        MsgBox(contents)

        'Er is ook een SaveFileDialog

    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim cities = context.Cities.ToList()

        'Voeg cities toe een voor een:

        'For Each item In cities
        '    ComboBox1.Items.Add(item)
        'Next

        'Voeg in 1 keer de hele verzameling cities toe:
        ComboBox1.DataSource = cities

        ComboBox1.DisplayMember = "CityName"

    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim item As City = ComboBox1.SelectedItem

        MsgBox(item.CityID)
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click


        Dim cities = context.Cities.First(Function(c) c.Country = "Nederland")



    End Sub
End Class
