using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows;

namespace InstaArt
{
    public class SearchBlock
    {
        public int index { get; set; }
        public Grid mainGrid { get; set; }
        private ComboBox searchParametrSelector { get; set; }
        private DatePicker dateInput { get; set; }
        private TextBox otherInput { get; set; }
        private Button functionalButton { get; set; }
        public SearchType type { get; set; }

        public SearchBlock(List<SearchParametr> parametrs)
        {
            mainGrid = new Grid();
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40, GridUnitType.Pixel) });
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50, GridUnitType.Pixel) });
            mainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            mainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(40, GridUnitType.Pixel) });

            dateInput = new DatePicker();
            otherInput = new TextBox();
            searchParametrSelector = new ComboBox();
            functionalButton = new Button();

            SetComboBoxPosition(0, 0, 1, 1);
            SetInputElementsPosotion(1, 0, 1, 2);
            SetButtonPosition(0, 1, 1, 1);

            searchParametrSelector.SelectionChanged += OnSelectionChanged;
            searchParametrSelector.ItemsSource = parametrs;
            searchParametrSelector.DisplayMemberPath = "Name";
            searchParametrSelector.SelectedValuePath = "Type";
            searchParametrSelector.SelectedIndex = 0;
        }

        public void UploadNewParametr(SearchParametr parametr)
        {
            if (searchParametrSelector != null)
            {
                searchParametrSelector.Items.Add(parametr);
            }
        }

        public void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchParametr par = (SearchParametr)searchParametrSelector.SelectedItem;
            type = par.Type;

            switch (type)
            {
                case SearchType.date:
                    ResetInputFields();
                    dateInput.Visibility = Visibility.Visible;
                    break;
                default:
                    ResetInputFields();
                    otherInput.Visibility = Visibility.Visible;
                    break;
            }

        }

        public dynamic GetValue()
        {
                switch (type)
                {
                    case SearchType.date:
                        return dateInput.SelectedDate;
                    case SearchType.str:
                        return otherInput.Text;
                    case SearchType.ints:
                        return Convert.ToInt32(otherInput.Text);
                    case SearchType.floats:
                        return Convert.ToSingle(otherInput.Text);
                    case SearchType.doubles:
                        return Convert.ToDouble(otherInput.Text);
                    default:
                        return null;
                }
        }
        public SearchParametr GetParametr()
        {
            return searchParametrSelector.SelectedItem as SearchParametr;
        }

        public void SetComboBoxPosition(int row = 0, int col = 0, int rowSpan = 1, int colSpan = 1)
        {
            mainGrid.Children.Add(searchParametrSelector);
            Grid.SetRow(searchParametrSelector, row);
            Grid.SetColumn(searchParametrSelector, col);
            Grid.SetRowSpan(searchParametrSelector, rowSpan);
            Grid.SetColumnSpan(searchParametrSelector, colSpan);
        }

        public void SetInputElementsPosotion(int row = 0, int col = 0, int rowSpan = 1, int colSpan = 1)
        {
            mainGrid.Children.Add(dateInput);       mainGrid.Children.Add(otherInput);
            Grid.SetRow(dateInput, row);            Grid.SetRow(otherInput, row);
            Grid.SetColumn(dateInput, col);         Grid.SetColumn(otherInput, col);
            Grid.SetRowSpan(dateInput, rowSpan);    Grid.SetRowSpan(otherInput, rowSpan);
            Grid.SetColumnSpan(dateInput, colSpan); Grid.SetColumnSpan(otherInput, colSpan);
        }
        public void SetButtonPosition(int row = 0, int col = 0, int rowSpan = 1, int colSpan = 1)
        {
            mainGrid.Children.Add(functionalButton);
            Grid.SetRow(functionalButton, row);
            Grid.SetColumn(functionalButton, col);
            Grid.SetRowSpan(functionalButton, rowSpan);
            Grid.SetColumnSpan(functionalButton, colSpan);
        }
        private void ResetInputFields()
        {
            dateInput.Visibility = Visibility.Hidden;
            otherInput.Visibility = Visibility.Hidden;

            dateInput.SelectedDate = null;
            otherInput.Text = null;
        }

        public void DeclareFunction(Action a)
        {
            functionalButton = new Button();
            SetButtonPosition(0, 1, 1, 1);

            functionalButton.Click += (s, e) =>
                {
                    a(); 
                };
            
        }

        public void DeclareFunction(Action<int> d)
        {
                functionalButton.Click += (s, e) =>
                {
                    d(index);
                };
        }

        //возврат значения и расстановка внутренних элементов по гриду, впихивание в value значений. Возможно методы возврата дял всех базовых типов отдельно
    }

    public enum SearchType : byte
    {
        str=0,
        date,
        ints,
        floats,
        doubles
    }

    public class SearchParametr
    {
        public string Name { get; set; }
        public SearchType Type { get; set; }

        public SearchParametr(string n, SearchType t)
        {
            Name = n; Type = t;
        }
    }


}
