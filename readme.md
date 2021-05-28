</td>
  </tr>
  <tr>
    <td style="text-align: center; border: none; height: 15em;">
    <td style="text-align: center; border: none; height: 16em;">
    <h2 style="font-size:3em;">Отчет</h2>
      <h3>по лабораторной работе<br><br> по дисциплине "Основы алгоритмизации и программирования"<br><br> Тема:<b> "Подготовка к экзамену "Создание WPF-приложения."<b> </h3></td>
     
  </tr>
  <tr>
    <br><br><td style="text-align: right; border: none; height: 20em;">
      Разработал:<br/>
     Попов Илья<br>

      Группа: И-21<br>
      Преподаватель:<br>
      Колесников Евгений Иванович
@@ -30,60 +30,32 @@

# Цели и задачи:

1: Создать приложение WPF .NET Framework
2:Написать РАБОЧЕЕ Windows (WPF) приложение, отображающее данные в табличном виде, с возможностью фильтрации, поиска и сортировки данных,
3:Написать вменяемую пояснительную записку в README.MD,
Опубликовать в репозитории



# Что делал
1)Написал каркас приложения в Grid
Разметка выглядит так:
 
```<Grid ShowGridLines="True">
    <Grid.RowDefinitions>
        <RowDefinition Height="auto"/>
        <RowDefinition />
        <RowDefinition Height="50"/>
    </Grid.RowDefinitions>
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="200"/>
        <ColumnDefinition/>
    </Grid.ColumnDefinitions>

    <!-- типа логотип компании -->
    <Image 
            Source="./Img/simon.png" 
            Grid.RowSpan="2"/>

    <StackPanel 
        Orientation="Vertical"
        Grid.RowSpan="3"
        VerticalAlignment="Bottom">
        <Button 
            x:Name="ExitButton"
            Content="Выход" 
            Click="ExitButton_Click"
            Height="50"/>
    </StackPanel>

    <WrapPanel
        Orientation="Horizontal"
        Grid.Column="1"
        MinHeight="50">
        <!-- минимальную высоту я тут поставил, чтобы верхнюю строку сетки было видно. В реальном приложении она не нужна -->
    </WrapPanel>
</Grid>
```
Создал папку модель и добавил класс студенты
```
namespace Wpfpopov.Model
{
    public class Students
    {
        public string Name  { get; set; }
        public string Date { get; set; }
        public string Telephone { get; set; }
        
        public string Course { get; set; }
        public string Group { get; set; }
       
    }
}
```
Создал интерфейс IDataProvider и класс  LocalDataProvider,реализующий этот интерфейс
```
namespace Wpfpopov.Model
{
    interface IDataProvider
    {
        IEnumerable<Students> GetStudents();
        IEnumerable<StudentsGroup> GetsStudentGroups();

    }
}
```
Написал данные в класс LocalDataProvider
``` public class LocalDataProvider : IDataProvider
    {
        public IEnumerable<Students> GetStudents()
        {


            return new Students[]{
                new Students{Name="Popov Ilya Alecsandrovich",Date="21.11.2003",Telephone="89969588724", Course="2",Group="И-21"},
                new Students{Name="Petrov Ivan Victorovich",Date="11.11.2003",Telephone="89969987842", Course="3",Group="И-31"},
                new Students{Name="Fodorov Daniil Rovenkovich",Date="05.01.2004",Telephone="88752211221", Course="1",Group="И-11"}
            };
        }
```
В простарнстве имен проекта создал класс Globals
```
namespace Wpfpopov
{
    class Globals
    {
        public static IDataProvider dataProvider;
    }
}
```
В конструкторе главного окна (MainWindow.xaml.cs) присвоил глобальной переменной dataProvider экземпляр класса LocalDataProvider
```

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

```
Привязываем данные  в MainWindow.xaml 
```
 <DataGrid
    Grid.Row="1"
    Grid.Column="1"
    CanUserAddRows="False"
    AutoGenerateColumns="False"
    ItemsSource="{Binding StudentsList}" SelectionChanged="DataGrid_SelectionChanged">
            <DataGrid.Columns>
                <DataGridTextColumn
            Header="ФИО"
            Binding="{Binding Name}"/>
                <DataGridTextColumn
            Header="Дата рождения"
            Binding="{Binding Date }"/>
                <DataGridTextColumn
            Header="Телефон"
            Binding="{Binding Telephone}"/>
                <DataGridTextColumn
            Header="Курс"
            Binding="{Binding Course}"/>
                <DataGridTextColumn
                Header="Группа"
            Binding="{Binding Group}"/>
            </DataGrid.Columns>

        </DataGrid>
```
2:Создаем фильтрацию по словарю
1 создаем класс
```
{
    
    public class StudentsGroup
    {
    public string Title { get; set; }
    }
}
```
Создаем в классе главного окна свойство для хранения справочника
```
 public List<StudentsGroup> StudentsGroupList { get; set; }
 ```
 В интерфейс поставщика данных (IDataProvider) добавляем метод для получения списка пород
  ```
  {
    interface IDataProvider
    {
        IEnumerable<Students> GetStudents();
   ```

Реализуем этот метод в LocalDataProvider
 ```

 public class LocalDataProvider : IDataProvider
    {
        public IEnumerable<Students> GetStudents()
        {


            return new Students[]{
                new Students{Name="Popov Ilya Alecsandrovich",Date="21.11.2003",Telephone="89969588724", Course="2",Group="И-21"},
                new Students{Name="Petrov Ivan Victorovich",Date="11.11.2003",Telephone="89969987842", Course="3",Group="И-31"},
                new Students{Name="Fodorov Daniil Rovenkovich",Date="05.01.2004",Telephone="88752211221", Course="1",Group="И-11"}
            };
        }
         ```
В коде пишем
          ```
         
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
 ```
 Теперь группы, добавляем в разметку выпадающий список для выбор группы(во WrapPanel):
 ```
<Label 
    Content="Группа"
    VerticalAlignment="Center"/>

            <ComboBox
    Name="GroupFilterComboBox"
    SelectionChanged="GroupFilterComboBox_SelectionChanged"
    VerticalAlignment="Center"
    MinWidth="100"
    SelectedIndex="0"
    ItemsSource="{Binding StudentsGroupList}">

                <ComboBox.ItemTemplate>
                    <DataTemplate>
                        <Label 
                Content="{Binding Title}"/>
                    </DataTemplate>
                </ComboBox.ItemTemplate>
            </ComboBox>
 ```
В коде главного окна добаляем
 ```
 private void GroupFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedGroup = (GroupFilterComboBox.SelectedItem as StudentsGroup).Title;
            Invalidate();
    
            }

```
Если сейчас запустить приложение, то выпадающий список будет отображаться, но реации на выбор не будет - дело в том, что визуальная часть не знает, что данные изменились.

Добавляем интерфейс окну
```
  public partial class MainWindow : Window, INotifyPropertyChanged
  ```
Реализуем интерфейс
    ```
        public event PropertyChangedEventHandler PropertyChanged;
        private void Invalidate()


        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("StudentsList"));
        }
        ```
  

В обработчик события выбора породы добавим вызов этого метода

 private void GroupFilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedGroup = (GroupFilterComboBox.SelectedItem as StudentsGroup).Title;
            Invalidate();
    
            }
Поиск,Сортировка
  В разметке окна (в элемент WrapPanel) добавляем элемент для ввода теста - TextBox
  ```


    
   <Label 
    Content="искать" 
    VerticalAlignment="Center"/>
            <TextBox
    Width="100"
    VerticalAlignment="Center"
    x:Name="SearchFilterTextBox" 
    KeyUp="SearchFilter_KeyUp" TextChanged="SearchFilterTextBox_TextChanged"/>
  ```
 В коде окна создаем переменную для хранения строки поиска и запоминаем её в обработчике ввода текста (SearchFilter_KeyUp)
  ```

  
         private string SearchFilter = "";

        private void SearchFilter_KeyUp(object sender, KeyEventArgs e)
        {
            SearchFilter = SearchFilterTextBox.Text;
            Invalidate();

        }
   ```
   Дорабатываем геттер 
 ```

 private IEnumerable<Model.Students> _StudentsList;
        public IEnumerable<Model.Students> StudentsList
        {
            get
            {
                var res = _StudentsList;

                res = res.Where(c => (SelectedGroup == "Все группы" || c.Group == SelectedGroup));
            
               
 ``` 
Сортировка
В разметке добавляем радиокнопки
 ```
  <Label 
    Content="Возраст:" 
    VerticalAlignment="Center"/>
            <RadioButton
    GroupName="Age"
    Tag="1"
    Content="по возрастанию"
    IsChecked="True"
    Checked="RadioButton_Checked"
    VerticalAlignment="Center"/>
            <RadioButton
    GroupName="Age"
    Tag="2"
    Content="по убыванию"
    Checked="RadioButton_Checked"
    VerticalAlignment="Center"/>
        </WrapPanel>
  ```
В коде добавляем переменную для хранения варианта сортировки и обработчик смены варианта сортировки
```
  private bool SortAsc = true;
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            SortAsc = (sender as RadioButton).Tag.ToString() == "1";
            Invalidate();
        }
    }
```
И дорабатываем геттер 

```

                if (SortAsc) res = res.OrderBy(c => c.Group);
                else res = res.OrderByDescending(c => c.Group);

                
                return res;

```
весь код в главном окне
```
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
    
    
```
![](..C:\Users\User\source\repos\Wpfpopov\123.png)

  Вывод:Научился разрабатывать WPF-приложение,поиск,сортировку,фильтрацию по словарю.




