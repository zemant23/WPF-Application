using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Application
{
    public partial class MainWindow : Window
    {
        private double _currentValue = 0;
        private double _lastValue = 0;
        private string _lastOperator = string.Empty;
        private bool _isOperatorClicked = false;
        private bool _isDecimalAdded = false;

        public MainWindow() 
        {
            InitializeComponent();
        }

        // Kliknutí na tlačítka číslic
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                string content = button.Content.ToString();
                if (_isOperatorClicked)
                {
                    Display.Text = content;
                    _isOperatorClicked = false;
                }
                else
                {
                    if (Display.Text == "0")
                    {
                        Display.Text = content;
                    }
                    else
                    {
                        Display.Text += content;
                    }
                }
            }
        }

        // Operátory +, -, *, /
        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                string operatorSymbol = button.Content.ToString();
                if (operatorSymbol == "=")
                {
                    PerformCalculation();
                }
                else
                {
                    _lastOperator = operatorSymbol;
                    _lastValue = double.Parse(Display.Text);
                    _isOperatorClicked = true;
                    _isDecimalAdded = false;
                }
            }
        }

        // Provádí výpočet na základě posledního operátoru
        private void PerformCalculation()
        {
            double result = 0;
            double currentValue = double.Parse(Display.Text);

            switch (_lastOperator)
            {
                case "+":
                    result = _lastValue + currentValue;
                    break;
                case "-":
                    result = _lastValue - currentValue;
                    break;
                case "*":
                    result = _lastValue * currentValue;
                    break;
                case "/":
                    if (currentValue != 0)
                    {
                        result = _lastValue / currentValue;
                    }
                    else
                    {
                        Display.Text = "Error";
                        return;
                    }
                    break;
                case "%":
                    result = _lastValue * currentValue / 100;
                    break;
                case "1/x":
                    if (currentValue != 0)
                    {
                        result = 1 / currentValue;
                    }
                    else
                    {
                        Display.Text = "Error";
                        return;
                    }
                    break;
                case "x²":
                    result = currentValue * currentValue;
                    break;
                case "√x":
                    if (currentValue >= 0)
                    {
                        result = Math.Sqrt(currentValue);
                    }
                    else
                    {
                        Display.Text = "Error";
                        return;
                    }
                    break;
            }

            Display.Text = result.ToString();
            _lastValue = result;
            _lastOperator = string.Empty;
        }

        // Funkce pro vymazání celé kalkulačky
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            _currentValue = 0;
            _lastValue = 0;
            _lastOperator = string.Empty;
            Display.Text = "0";
            _isDecimalAdded = false;
        }

        // Funkce pro vymazání posledního zadání
        private void ClearEntry_Click(object sender, RoutedEventArgs e)
        {
            Display.Text = "0";
            _isDecimalAdded = false;
        }

        // Funkce pro smazání posledního zadání (backspace)
        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            if (Display.Text.Length > 1)
            {
                Display.Text = Display.Text.Substring(0, Display.Text.Length - 1);
            }
            else
            {
                Display.Text = "0";
            }
        }

        // Funkce pro negaci čísla
        private void Negate_Click(object sender, RoutedEventArgs e)
        {
            double currentValue = double.Parse(Display.Text);
            Display.Text = (-currentValue).ToString();
        }

        // Funkce pro přidání desetinné čárky
        private void Decimal_Click(object sender, RoutedEventArgs e)
        {
            if (!_isDecimalAdded)
            {
                Display.Text += ",";
                _isDecimalAdded = true;
            }
        }

        // Funkce pro výpočet 1/x
        private void Reciprocal_Click(object sender, RoutedEventArgs e)
        {
            double currentValue = double.Parse(Display.Text);
            if (currentValue != 0)
            {
                Display.Text = (1 / currentValue).ToString();
            }
            else
            {
                Display.Text = "Error";
            }
        }

        // Funkce pro výpočet x²
        private void Square_Click(object sender, RoutedEventArgs e)
        {
            double currentValue = double.Parse(Display.Text);
            Display.Text = (currentValue * currentValue).ToString();
        }

        // Funkce pro výpočet √x
        private void SquareRoot_Click(object sender, RoutedEventArgs e)
        {
            double currentValue = double.Parse(Display.Text);
            if (currentValue >= 0)
            {
                Display.Text = Math.Sqrt(currentValue).ToString();
            }
            else
            {
                Display.Text = "Error";
            }
        }

        // Funkce pro výpočet procenta
        private void Percentage_Click(object sender, RoutedEventArgs e)
        {
            double currentValue = double.Parse(Display.Text);
            Display.Text = (_lastValue * currentValue / 100).ToString();
        }

        // Funkce pro rovnítko
        private void Equal_Click(object sender, RoutedEventArgs e)
        {
            PerformCalculation();
        }
    }
}
