namespace ExpressionEvaluator.Core;

public class Evaluator
{
    public static double Evaluate(string infix)
    {
        var postfix = InfixToPostfix(infix);
        return EvaluatePostfix(postfix);
    }
    private static List<string> Tokenize(string infix)
    {
        var tokens = new List<string>();
        var number = string.Empty;

        foreach (var item in infix)
        {
            if (char.IsDigit(item) || item == '.')
            {
                number += item;
            }
            else
            {
                if (!string.IsNullOrEmpty(number))
                {
                    tokens.Add(number);
                    number = string.Empty;
                }
                tokens.Add(item.ToString());
            }
        }
        if (!string.IsNullOrEmpty(number))
        {
            tokens.Add(number);
        }
        return tokens;
    }

    private static List<string> InfixToPostfix(string infix)
    {
        var postFix = new List<string>();
        var stack = new Stack<string>();
        var tokens = Tokenize(infix);

        foreach (var token in tokens)
        {
            if (IsOperator(token))
            {
                if (stack.Count == 0)
                {
                    stack.Push(token);
                }
                else
                {
                    if (token == ")")
                    {
                        while (stack.Peek() != "(")
                        {
                            postFix.Add(stack.Pop());
                        }
                        stack.Pop();
                    }
                    else if (token == "(")
                    {
                        stack.Push(token);
                    }
                    else
                    {
                        if (PriorityInfix(token) > PriorityStack(stack.Peek()))
                        {
                            stack.Push(token);
                        }
                        else
                        {
                            while (stack.Count > 0 && stack.Peek() != "(" &&
                                   PriorityInfix(token) <= PriorityStack(stack.Peek()))
                            {
                                postFix.Add(stack.Pop());
                            }
                            stack.Push(token);
                        }
                    }
                }
            }
            else
            {
                postFix.Add(token);
            }
        }
        while (stack.Count > 0)
        {
            postFix.Add(stack.Pop());
        }
        return postFix;
    }

    private static int PriorityStack(string item) => item switch
    {
        "^" => 3,
        "*" => 2,
        "/" => 2,
        "+" => 1,
        "-" => 1,
        "(" => 0,
        _ => 0,
    };

    private static int PriorityInfix(string item) => item switch
    {
        "^" => 4,
        "*" => 2,
        "/" => 2,
        "+" => 1,
        "-" => 1,
        "(" => 5,
        _ => 0,
    };

    private static double EvaluatePostfix(List<string> postfix)
    {
        var stack = new Stack<double>();
        foreach (var token in postfix)
        {
            if (IsOperator(token))
            {
                var b = stack.Pop();
                var a = stack.Pop();
                stack.Push(token switch
                {
                    "+" => a + b,
                    "-" => a - b,
                    "*" => a * b,
                    "/" => a / b,
                    "^" => Math.Pow(a, b),
                    _ => throw new Exception("Sintax error."),
                });
            }
            else
            {
                stack.Push(double.Parse(token, System.Globalization.CultureInfo.InvariantCulture));
            }
        }
        return stack.Pop();
    }

    private static bool IsOperator(string token) => "+-*/^()".Contains(token);
}