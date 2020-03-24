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

namespace Slam4Dico
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            UneSalle = new Salle("Manche a air");  //Inialise une salle dans le public

        }

        Salle UneSalle; //Déclare une salle

        private void btnajoutajouter_Click(object sender, RoutedEventArgs e)
        {
            int outParse;
            int verif1 = Convert.ToInt32(tbajoutnumeroposte.Text);
            bool verification = UneSalle.getLesPostes().ContainsKey(verif1); // Variable booléenne qui permet de vérifier que le numéro contenu dans la textbox n'existe pas en tant que clé

            if (verification==true) // Utilise la variable précédente pour vérifier
            {
                MessageBox.Show("Ce numero de poste est deja présent, veuillez en saisir un autre", "Erreur"); 
            }
            else if (tbajoutnumeroposte.Text == "" || Int32.TryParse(tbajoutnumeroposte.Text, out outParse) == false || Convert.ToInt32(tbajoutnumeroposte.Text) > 7) // Vérifie que le contenu de la text box soit conforme, de type entier, non null et inférieur a 7
            {
                MessageBox.Show("Veuillez renseignez un numero de poste correct", "Erreur");
            }
            else if (cbajoutnumerotavree.SelectedItem == null) // Vérifie que le numéro contenu dans la textbox ne soit pas null
            {
                MessageBox.Show("Veuillez renseignez un numero de travee correct", "Erreur");
            }
            else if (cbajoutnumerorangee.SelectedItem == null) // Vérifie que le numéro contenu dans la textbox ne soit pas null
            {
                MessageBox.Show("Veuillez renseignez un numero de rangee correct", "Erreur");
            }
            else
            {
                //Ajoute le poste déclaré juste avant dans la collection les postes en utilisant la méthode ajouterPoste 
                #region AjouterPoste
                Poste PosteAjout;
                PosteAjout = new Poste(Convert.ToInt16(tbajoutnumeroposte.Text), Convert.ToInt16(cbajoutnumerotavree.SelectedItem), Convert.ToInt16(cbajoutnumerorangee.SelectedItem));
                UneSalle.AjouterPoste(PosteAjout);
                MessageBox.Show("Insertion reussite pour le numéro de poste : " + Convert.ToInt16(tbajoutnumeroposte.Text), "Reussite"); 
                #endregion  
            }


        }

        private void Window_Loaded(object sender, RoutedEventArgs e) // Charge les numéros de rangées et de travées possible dans chaque combo box respective
        {
            cbajoutnumerotavree.Items.Add("1");
            cbajoutnumerotavree.Items.Add("2");
            cbajoutnumerotavree.Items.Add("3");


            cbajoutnumerorangee.Items.Add("1");
            cbajoutnumerorangee.Items.Add("2");
            cbajoutnumerorangee.Items.Add("3");


        }



        private void ConsultationLoaded(object sender, RoutedEventArgs e) // Supprime les items de la combobox et de la list box puis rempli la combobox avec chaque poste existant
        {

            cbajoutvisiblenumeroposte.Items.Clear(); 
            lboxajoutvisible.Items.Clear();
            foreach (KeyValuePair<int, Poste> unPoste in UneSalle.getLesPostes()) //Parcours la collection lesPostes obtenus grace a getLesPostes
            {
                Poste unposte;
                unposte = unPoste.Value;
                cbajoutvisiblenumeroposte.Items.Add(unposte.getNuméro());

            }


        }





        private void btnajoutvisibleajouter_Click(object sender, RoutedEventArgs e) //Ajoute les postes visibles par poste
        {

            if (MessageBox.Show("Voulez vous ajouter les postes visibles ?", "Ajout", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No) //Message de confirmation
            {
                MessageBox.Show("Les postes visibles n'ont pas été ajoutés", "Ajout postes");
            }
            else
            {
                MessageBox.Show("Les postes visibles ont correctement été ajoutés", "Ajout postes");
                ItemCollection comboitems = cbajoutvisiblenumeroposte.Items; //envoie tout les objets de la combo box dans comboitems
                foreach (int objet in comboitems) //Parcours les objet de comboitems
                {
                    int i = 1; // Initialise la valeur  de la boucle while
                    if (Convert.ToInt32(cbajoutvisiblenumeroposte.Items[cbajoutvisiblenumeroposte.SelectedIndex]) == objet) // Si l'objet de la combo box est le même que celui parcouru en ce moment le code s'execute uniquement pour ce numéro
                        while (i <= lboxajoutvisible.Items.Count) // Tant que i ne dépasse pas le nombres d'items de la listbox
                        {

                            CheckBox cb = (CheckBox)lboxajoutvisible.Items[i - 1]; // Ajoute une checkbox dans la list box
                            if (cb.IsChecked == true)  // Si la checkbox est cochée elle l'envoie dans la variable result
                            {
                                int result = Convert.ToInt32(cb.Content);



                                foreach (KeyValuePair<int, Poste> unPoste in UneSalle.getLesPostes()) //Parcours chaque poste de la salle
                                {
                                    Poste unposte;
                                    unposte = unPoste.Value;  

                                    int numtravée = unposte.getNuméroTravée();
                                    int numrangée = unposte.getNuméroRangée();
                                    //Initialise les valeurs du poste correspondant


                                    if (unposte.getNuméro() == objet) //Si le poste correspond a l'objet selectionné sur la combobox elle l'ajoute dans la collection de Poste lesPostesVisibles sinon elle ne fait rien
                                    {

                                        Poste PosteAjoutVisible;
                                        PosteAjoutVisible = new Poste(Convert.ToInt16(result), numtravée, numrangée);
                                        unposte.getLesPostesVisibles().Add(PosteAjoutVisible);


                                    }

                                }


                            }  
                            i++; // Incremente i qui permet le parcours des items de la list box
                        }

                }
            }
           
          
                              
        }
    
    private void ChargementListePostes(object sender, SelectionChangedEventArgs e) //Charge la liste des postes
    {
        lboxajoutvisible.Items.Clear();
        foreach (KeyValuePair<int, Poste> unPoste in UneSalle.getLesPostes()) //Parcours le dictionnaire des postes
        {
            int x = unPoste.Key;
            Poste y = unPoste.Value;
            int item = Convert.ToInt32(cbajoutvisiblenumeroposte.Items[cbajoutvisiblenumeroposte.SelectedIndex]) - 1; //Permet d'obtenir la valeur de l'item selectionné pour l'utiliser dans le if d'après
            int dernierobjet = Convert.ToInt32(cbajoutvisiblenumeroposte.Items[cbajoutvisiblenumeroposte.Items.Count - 1].ToString()); // Permet d'obtenir la valeur du denier item afin de l'utiliser dans le if après

            if (item < dernierobjet) // Compare l'item selectionné au dernier
                item = item + 1; //Rajoute 1 sauf si le dernier item est selectionné

            if (y.getNuméro() != item) // N'ajoute pas le numero du poste selectionné dans la listbox
            {
                CheckBox itemm = new CheckBox(); //Crée une checkbox
                itemm.Content = y.getNuméro();  //La remplie avec le numéro du poste
                lboxajoutvisible.Items.Add(itemm); //Ajoute ce checkbox dans la liste
            }
        }
    }

        private void ConsultationLoadedConsultation(object sender, RoutedEventArgs e) //Charge la listbox qui contient les différents postes et leurs postes visibles
        {

            lbconsultation.Items.Clear(); //Vide la listbox
            foreach (KeyValuePair<int, Poste> unPoste in UneSalle.getLesPostes()) //Parcours le dictionnaires lesPostes obtenus avec getLesPostes()
            {
                Poste unposte;
                string Postescontenu = ""; //Initialise la chaîne Postescontenu
                unposte = unPoste.Value;
                List<Poste> lespostesvisibles = unposte.getLesPostesVisibles();

                foreach (Poste item in lespostesvisibles) //Parcours lespostesvisibles la collection de postes visibles
                {
                    
                    Postescontenu += Convert.ToString(item.getNuméro()) + " "; // Envoie a chaque execution le numéro dans la variable  Postescontenu

                }

                lbconsultation.Items.Add(Convert.ToString(unposte.getNuméro())+"                                        " + Postescontenu); //Envoie l'objet dans la listbox, il contient les numéros de postes ainsi que les numéros de postes visibles
            }
        }
    }
}

