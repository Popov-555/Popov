using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using Wpfpopov.Model;

namespace Wpfpopov
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged







    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
            Globals.dataProvider = new LocalDataProvider();
            StudentsList = Globals.dataProvider.GetStudents();

            StudentsGroupList = Globals.dataProvider.GetsStudentGroups().ToList();
            StudentsGroupList.Insert(0, new StudentsGroup { Title = "Все группы" });
        }








        public List<StudentsGroup> StudentsGroupList { get; set; }



        private IEnumerable<Model.Students> _StudentsList;
        public IEnumerable<Model.Students> StudentsList
        {
            get
            {
                var res = _StudentsList;

                res = res.Where(c => (SelectedGroup == "Все группы" || c.Group == SelectedGroup));
            
               
                // фильтруем по группе
               // res = res.Where(c => (c.Group >= SelectedGroup.GroupFrom && c.Group < SelectedGroup.GroupTo));

                // если фильтр не пустой, то ищем ВХОЖДЕНИЕ подстроки поиска в кличке без учета регистра
                if (SearchFilter != "")
                    res = res.Where(c => c.Name.IndexOf(SearchFilter, StringComparison.OrdinalIgnoreCase) >= 0);

                if (SortAsc) res = res.OrderBy(c => c.Group);
                else res = res.OrderByDescending(c => c.Group);

                
                return res;





            }

            set
            {
               _StudentsList = value;
            }
        }




        public string SelectedGroup { get; set; }






        public event PropertyChangedEventHandler PropertyChanged;
        private void Invalidate()


        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("StudentsList"));
        }



              public string SelectedBreed = "";

        public IEnumerable<StudentsGroup> GroupList { get; set; }
       
    
       
        

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }



        private void GroupFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedGroup = (GroupFilterComboBox.SelectedItem as StudentsGroup).Title;
            Invalidate();
        


          
            }



        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        /// <summary>
        
        /// </summary>
       

    

         private string SearchFilter = "";

        private void SearchFilter_KeyUp(object sender, KeyEventArgs e)
        {
            SearchFilter = SearchFilterTextBox.Text;
            Invalidate();

        }

        private void SearchFilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchFilter = SearchFilterTextBox.Text;
            Invalidate();
        }

   

        private bool SortAsc = true;
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            SortAsc = (sender as RadioButton).Tag.ToString() == "1";
            Invalidate();
        }
    }
        
    }
    
    




      
        
    

