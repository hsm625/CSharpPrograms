using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Character_Creator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Character newchar;

        public MainWindow()
        {
            InitializeComponent();
        }

        //fill newchar's fields with information from the form.
        private void makeNewChar()
        {
            newchar = new Character();
            error.Text = "";
            newchar.Name = name.Text.Replace("'", "");
            newchar.Type = type.Text.Replace("'", "");
            newchar.Species = species.Text.Replace("'", "");
            try
            {
                newchar.Age = float.Parse(age.Text);
            }
            catch (Exception E)
            {
                error.Text = "Invalid entry for Age. ";
                newchar.Age = -1;
            }
            newchar.Neighbourhood = neighbourhood.Text.Replace("'", "");
            newchar.Appearance = appearance.Text.Replace("'", "");
            newchar.Personality = personality.Text.Replace("'", "");
            newchar.Notes = notes.Text.Replace("'", "");
            try
            {
                newchar.S = short.Parse(S.Text);
                newchar.P = short.Parse(P.Text);
                newchar.E = short.Parse(E.Text);
                newchar.C = short.Parse(C.Text);
                newchar.I = short.Parse(I.Text);
                newchar.A = short.Parse(A.Text);
                newchar.L = short.Parse(L.Text);
            }
            catch
            {
                error.Text += "Invalid SPECIAL entry.";
                newchar.S = 50;
            }
        }

        //activated on selection of the Send button.
        private void button_Click(object sender, RoutedEventArgs e)
        {
            DBConnect db = new DBConnect();

            makeNewChar();

            if (newchar.Age > 0 && newchar.S + newchar.P + newchar.E + newchar.C + newchar.I + newchar.A + newchar.L <= 42)
            {
                //If the Age and Special values are valid, send the character to the database.
                db.Insert(newchar);
            }
            else
            {
                error.Text = "Invalid Age or SPECIAL point usage";
            }
        }

        //Loads a character based on the name entered in the name field on the form. Sends the data to the form text boxes.
        private void Load_Click(object sender, RoutedEventArgs e)
        {
            DBConnect db = new DBConnect();

            //get a list of data from each character found.
            List<string>[] results = db.Select(name.Text);
            try
            {
                //fill the text boxes with the information on the first character found.
                name.Text = results[1].ElementAt<string>(0);
                type.Text = results[2].ElementAt<string>(0);
                species.Text = results[3].ElementAt<string>(0);
                age.Text = results[4].ElementAt<string>(0);
                neighbourhood.Text = results[5].ElementAt<string>(0);
                appearance.Text = results[6].ElementAt<string>(0);
                personality.Text = results[7].ElementAt<string>(0);
                notes.Text = results[8].ElementAt<string>(0);
                S.Text = results[9].ElementAt<string>(0);
                P.Text = results[10].ElementAt<string>(0);
                E.Text = results[11].ElementAt<string>(0);
                C.Text = results[12].ElementAt<string>(0);
                I.Text = results[13].ElementAt<string>(0);
                A.Text = results[14].ElementAt<string>(0);
                L.Text = results[15].ElementAt<string>(0);
                error.Text = "";
            }
            catch(ArgumentOutOfRangeException A)
            {
                error.Text = "No character by that name exists.";
            }
        }
    }
}
