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
using System.Xml;
using System.Xml.Serialization;

namespace Test_technique_sbs
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Check if no upper case and if doesn't already exist
        //Check si tag en minuscule
        private bool check_case(string to_check)
        {
            for (int i = 0; i < to_check.Length; i++)
            {
                if (Char.IsUpper(to_check[i]))
                    return false;
            }
            return true;
        }

        //Check si le tag exist déja
        private bool check_if_exist(string str)
        {
            if (listbox_assigned.Items.Contains(str))
                return false;
            else if (listbox_free.Items.Contains(str))
                return false;
            else
                return true;
        }
        #endregion

        //Ajouter le contenu de tag dans listbox_free
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (tag.Text != "")
            {
                if (check_case(tag.Text) == true && check_if_exist(tag.Text) == true)
                    listbox_free.Items.Add(tag.Text);
            }
            tag.Clear();
        }

        //detection touche entrer pour confirmer
        private void tag_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Add_Click(sender, e);
        }

        #region Switch to listbox_assigned
        //Ajoute l'item sélectionné dans listbox_assigned et le supprime de lisbox_free
        private void To_assigned_Click(object sender, RoutedEventArgs e)
        {
            if (listbox_free.SelectedItem != null)
            {
                string save_item = listbox_free.SelectedItem.ToString();

                listbox_assigned.Items.Add(save_item);
                listbox_free.Items.Remove(save_item);
            }
        }

        //detection touche entrer
        private void listbox_assigned_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                To_free_Click(sender, e);
        }
        #endregion

        #region Switch to listbox_free
        //Ajoute l'item sélectionné dans listbox_free et le supprime de lisbox_assigned
        private void To_free_Click(object sender, RoutedEventArgs e)
        {
           
            if (listbox_assigned.SelectedItem != null)
            {
                string save_item = listbox_assigned.SelectedItem.ToString();

                listbox_free.Items.Add(save_item);
                listbox_assigned.Items.Remove(save_item);
            }
        }

        //detection touche entrer
        private void listbox_free_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            To_assigned_Click(sender, e);
            
        }

        #endregion

        //Export de listbox_assigned sous format XML
        private void export_Click(object sender, RoutedEventArgs e)
        {
            if (listbox_assigned.Items.Count > 0)
            {
                XmlTextWriter xwriter = new XmlTextWriter("save_tag.xml", Encoding.Unicode);
                xwriter.WriteStartDocument();
                xwriter.WriteStartElement("Xml File");
                xwriter.WriteStartElement("Title");
                xwriter.WriteString("List Tag");
                xwriter.WriteEndElement();
                foreach (string items in listbox_assigned.Items)
                {
                    xwriter.WriteStartElement("Item");
                    xwriter.WriteString(items);
                    xwriter.WriteEndElement();
                }
                xwriter.WriteEndElement();
                xwriter.WriteEndDocument();
                xwriter.Close();
                MessageBox.Show("Le fichier a été sauvegardé");
            }
            else
                MessageBox.Show("Impossible de sauvegarder une liste vide.");
        }
    }
}
