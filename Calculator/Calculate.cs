using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    class Calculate
    {
        private int[,] _valueForActions = new int[,]
        {
            {4, 1, 1, 1, 1, 1, 5, 1},
            {2, 2, 2, 1, 1, 1, 2, 1},
            {2, 2, 2, 1, 1, 1, 2, 1},
            {2, 2, 2, 2, 2, 1, 2, 1},
            {2, 2, 2, 2, 2, 1, 2, 1},
            {5, 1, 1, 1, 1, 1, 3, 1},
            {6, 6, 6, 6, 6, 1, 6, 6}
        };

        private int IndexActionOnXAndY(char action)
        {
            char[] actions = new char[8] { '|', '+', '-', '*', '/', '(', ')', '#'};

            for (int i = 0; i < actions.Length; i++)
            {
                if (action == actions[i])
                {
                    return i;
                }
            }
            return -1;
        }

        private bool IsDigit(char value)
        {
            if(value >= 48 && value <= 57)
            {
                return true;
            }
            return false;
        }

        private void ActionItem(ref Stack<char> action, ref List<string> RPN, char value)
        {
            if (action.Count == 0)
            {
                action.Push(value);
                return;
            }

            int IndexActionOnY;

            if (action.Peek() == '#')
            {
                IndexActionOnY = 6;
            }
            else
            {
                IndexActionOnY = IndexActionOnXAndY(action.Peek());
            }

            if (action.Peek() == ')')
            {
                RPN = null;
                return;
            }

            int valueAction = _valueForActions[IndexActionOnY, IndexActionOnXAndY(value)];

            switch (valueAction)
            {
                case 1:
                    action.Push(value);
                    break;
                case 2:
                    RPN.Add(action.Pop().ToString());
                    ActionItem(ref action, ref RPN, value);
                    break;
                case 3:
                    action.Pop();
                    break;
                case 5:
                    RPN = null;
                    return;
                case 6:
                    RPN.Add(action. Pop().ToString());
                    if (action.Count > 0 && action.Peek() != '(')
                    {
                        RPN.Add(action.Pop().ToString());
                    }
                    ActionItem(ref action, ref RPN, value);
                    break;
            }
        }

        private void TransformationInPN(ref string expression, ref List<string> RPN)
        {
            expression = expression.Replace(" ", "");

            // start можлива заміна
            do
            {
                expression = expression.Replace("/-(", "/#(");
                expression = expression.Replace("*-(", "*#(");
                expression = expression.Replace("--(", "-#(");
                expression = expression.Replace("+-(", "+#(");
                expression = expression.Replace("(-(", "(#(");
            } while (expression.IndexOf("/-(") > 0 || expression.IndexOf("*-(") > 0 || expression.IndexOf("--(") > 0 || expression.IndexOf("+-(") > 0 || expression.IndexOf("(-(") > 0);
            if (expression[0] == '-' && expression[1] == '(')
            {
                expression = expression.Remove(0, 1);
                expression = "#" + expression;
            }
            // end

            
            expression += '|';

            Stack<char> action = new Stack<char>();

            int i = 0;
            while (i < expression.Length)
            {
                                                                                                                                                                                            
                if ((i == 0 && expression[i] == '-' &&  IsDigit(expression[i + 1]))
                    || (i > 0 && i < expression.Length - 1 && expression[i - 1] != ')' && false == IsDigit(expression[i - 1]) && expression[i] == '-' && IsDigit(expression[i + 1])) 
                    || IsDigit(expression[i])) 
                {
                    RPN.Add("");
                    do
                    {
                        RPN[^1] += expression[i];
                        i++;
                    } while (i < expression.Length && (expression[i] == '.' || IsDigit(expression[i])));
                }
                else
                {
                    ActionItem(ref action, ref RPN, expression[i]);
                    if (RPN == null)
                    {
                        return; 
                    }
                    i++;
                }
            }

            while (action.Count > 0)
            {
                RPN.Add(action.Pop().ToString());
            }
        }

        public string CalculateExpression(string expression)
        {
            List<string> RPN = new List<string>();
            Stack<double> numbers = new Stack<double>();
            double firstNumber = 0;
            double secondNumber = 0;

            TransformationInPN(ref expression, ref RPN);

            if (RPN == null)
            {
                return "error";
            }

            RPN.RemoveAt(RPN.Count - 1);

            for (int i = 0; i < RPN.Count; i++)
            {
                if (IsDigit(RPN[i][^1]))
                {
                    RPN[i] = RPN[i].Replace(".", ",");
                    numbers.Push(double.Parse(RPN[i]));
                }
                else
                {
                    if (RPN[i][0] == '#')
                    {
                        firstNumber = numbers.Pop();
                    }
                    else
                    {
                        secondNumber = numbers.Pop();
                        firstNumber = numbers.Pop();
                    }

                    switch (RPN[i][0])
                    {
                        case '#':
                            numbers.Push(-1.0 * firstNumber);
                            break;
                        case '-':
                            numbers.Push(firstNumber - secondNumber);
                            break;
                        case '+':
                            numbers.Push(firstNumber + secondNumber);
                            break;
                        case '*':
                            numbers.Push(firstNumber * secondNumber);
                            break;
                        case '/':
                            numbers.Push(firstNumber / secondNumber);
                            break;
                    }
                }
            }

            return numbers.Pop().ToString();
        }
    }
}

/*
        1 2 3 4 5 6 7
      | + — * / ( ) # вагони на стрелце  
    | 4 1 1 1 1 1 5 1
    + 2 2 2 1 1 1 2 1
    — 2 2 2 1 1 1 2 1
    * 2 2 2 2 2 1 2 1
    / 2 2 2 2 2 1 2 1
    ( 5 1 1 1 1 1 3 1
    # 6 6 6 6 6 1 6 5

    1. Вагон на стрелке отправляется в Москву
    
    2. Последний вагон, направившийся в Москву, разворачивается и направляется в Киев
    
    3. Вагон, находящийся на стрелке, и последний вагон, отправившийся в Москву,
    угоняются и исчезают
    
    4. Остановка. Символы, находящиеся на Киевской ветке, представляют собой формулу 
    в обратной польской записи, если читать слева направо

    5. Остановка. Произошла ошибка. Изначальная формула была некорректно сбалансирована

    6. Вагон виштовхується забираючі і предідущий вагон якщо не пусто 

 */





