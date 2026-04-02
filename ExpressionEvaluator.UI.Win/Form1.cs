using ExpressionEvaluator.Core;

namespace ExpressionEvaluator.UI.Win
{
    public partial class Form1 : Form
    {
        private string _expression = string.Empty;

        public Form1()
        {
            InitializeComponent();
        }

        private void btn7_Click(object sender, EventArgs e) => AddToExpression("7");
        private void btn8_Click(object sender, EventArgs e) => AddToExpression("8");
        private void btn9_Click(object sender, EventArgs e) => AddToExpression("9");
        private void btn4_Click(object sender, EventArgs e) => AddToExpression("4");
        private void btn5_Click(object sender, EventArgs e) => AddToExpression("5");
        private void btn6_Click(object sender, EventArgs e) => AddToExpression("6");
        private void btn1_Click(object sender, EventArgs e) => AddToExpression("1");
        private void btn2_Click(object sender, EventArgs e) => AddToExpression("2");
        private void btn3_Click(object sender, EventArgs e) => AddToExpression("3");
        private void btn0_Click(object sender, EventArgs e) => AddToExpression("0");
        private void btnDot_Click(object sender, EventArgs e) => AddToExpression(".");
        private void btnPlus_Click(object sender, EventArgs e) => AddToExpression("+");
        private void btnMinus_Click(object sender, EventArgs e) => AddToExpression("-");
        private void btnMultiply_Click(object sender, EventArgs e) => AddToExpression("*");
        private void btnDivide_Click(object sender, EventArgs e) => AddToExpression("/");
        private void btnPow_Click(object sender, EventArgs e) => AddToExpression("^");
        private void btnOpenParenthesis_Click(object sender, EventArgs e) => AddToExpression("(");
        private void btnCloseParenthesis_Click(object sender, EventArgs e) => AddToExpression(")");

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_expression.Length > 0)
                _expression = _expression[..^1];
            txtDisplay.Text = _expression;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            _expression = string.Empty;
            txtDisplay.Text = _expression;
        }

        private void btnResult_Click(object sender, EventArgs e)
        {
            try
            {
                var result = Evaluator.Evaluate(_expression);
                txtDisplay.Text = $"{_expression}={result}";
                _expression = result.ToString(System.Globalization.CultureInfo.InvariantCulture);
            }
            catch
            {
                txtDisplay.Text = "Error";
                _expression = string.Empty;
            }
        }

        private void AddToExpression(string value)
        {
            _expression += value;
            txtDisplay.Text = _expression;
        }
    }
}