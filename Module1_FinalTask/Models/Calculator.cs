using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module1_FinalTask.Models
{
    internal class Calculator
    {
        //Инициализация переменных
        private double operand1; //первый операнд
        private double operand2; //второй операнд
        private string operation; //текущая операция
        private double result; //текущий результат
        private double lastNum; //последнее введенное число
        private string stringNum; //последнее введенное число в строчном виде
        private string lastCalc; //последнее вычисление
        private string errMsg; //сообщение об ошибке
        private bool err = false; //индикатор ошибки
        private bool eq = false; //индикатор нажатия клавиши равенства
        private bool newNum = true; //индикатор готовности к вводу нового числа

        //Метод вычислений (при нажатии клавиш операций)
        internal string Ariph(string op)
        {
            if (op != "=") //если введена операция не '='
            {
                if (operation == null) //если текущая операция не назначена
                {
                    operation = op; //присвоить текущей операции введенную операцию 
                    operand1 = lastNum; //присвоить первому операнду последнее введенное число 
                    result = lastNum; //присвоить текущему результату последнее введенное число
                    lastCalc = LastCalc(operand1.ToString(), operation); //присвоить последнему вычислению значения первого операнда и текущей операции
                }
                else //если текущая операция назначена
                {
                    if (!eq) //если предыдущий ввод был не '='
                    {
                        operand2 = lastNum; //присвоить второму операнду последнее введенное число 
                        result = Calculate(operation); //присвоить текущему результату результат выполнения текущей операции
                        operand1 = result; //присвоить первому операнду текущий результат 
                        operation = op; //присвоить текущей операции введенную операцию
                        lastCalc = LastCalc(result.ToString(), operation); //присвоить последнему вычислению значения текущего результата и текущей операции
                    }
                    else //если предыдущий ввод был '='
                    {
                        if (newNum) //если готовность к вводу нового числа
                        {
                            lastNum = result; //присвоить последнему введенному числу текущий результат
                        }
                        operand1 = lastNum; //присвоить первому операнду последнее введенное число
                        operation = op; //присвоить текущей операции введенную операцию
                        result = lastNum; //присвоить текущему результату последнее введенное число
                        lastCalc = LastCalc(result.ToString(), operation); //присвоить последнему вычислению значения текущего результата и текущей операции
                    }
                }
                eq = false; //повторного ввода '=' нет
            }
            else //если введена операция '='
            {
                if (eq) //если предыдущий ввод был '='
                {
                    if (newNum) //если готовность к вводу нового числа
                    {
                        lastNum = result; //присвоить последнему введенному числу текущий результат
                    }
                    operand1 = lastNum; //присвоить первому операнду последнее введенное число
                    result = Calculate(operation); //присвоить текущему результату результат выполнения текущей операции
                    lastCalc = LastCalc(operand1.ToString(), operation) + LastCalc(operand2.ToString(), op);//присвоить последнему вычислению значения первого операнда, текущей операции, второго операнда и введенной операции '='
                }
                else //если предыдущая операция была не '='
                {
                    if (operation != null) //если текущая операция не назначена
                    {
                        operand2 = lastNum; //присвоить второму операнду последнее введенное число 
                        result = Calculate(operation); //присвоить текущему результату результат выполнения текущей операции
                        lastNum = result; //присвоить последнему введенному числу текущий результат
                        lastCalc = LastCalc(operand1.ToString(), operation) + LastCalc(operand2.ToString(), op);//присвоить последнему вычислению значения первого операнда, текущей операции, второго операнда и введенной операции '='
                    }
                    else //если текущая операция назначена
                    {
                        operand1 = lastNum; //присвоить первому операнду последнее введенное число
                        result = lastNum; //присвоить текущему результату последнее введенное число
                        lastCalc = LastCalc(operand1.ToString(), op); //присвоить последнему вычислению значения первого операнда и текущей операции
                    }
                }
                eq = true; //предыдущая операция была '='
            }
            newNum = true; //готовность к вводу нового числа
            return lastCalc; //вернуть значение последнего вычисления
        }

        //Метод простых арифметических операций
        private double Calculate(string op)
        {
            switch (op)
            {
                //Сложение
                case "+":
                    {
                        return operand1 + operand2;
                    }
                //Вычитание
                case "-":
                    {
                        return operand1 - operand2;
                    }
                //Умножение
                case "×":
                    {
                        return operand1 * operand2;
                    }
                //Деление
                case "÷":
                    {
                        if (lastNum != 0)
                        {
                            return operand1 / operand2;
                        }
                        else
                        {
                            errMsg = "Деление на ноль невозможно";
                            err = true;
                            return 0;
                        }
                    }
                //Иначе 0
                default:
                    return 0;
            }
        }

        //Метод дополнительных математических операций (при нажатии клавиш дополнительных математических операций)
        internal string Calc(string p)
        {
            switch (p)
            {
                //Возведение в степень
                case "x²":
                    {
                        if (!eq)
                        {
                            lastCalc += LastCalc(lastNum.ToString(), p);
                            result = Math.Pow(lastNum, 2);
                        }
                        else
                        {
                            lastCalc = LastCalc(result.ToString(), p);
                            result = Math.Pow(result, 2);
                        }
                        lastNum = result;
                        break;
                    }
                //Квадратный корень
                case "√x":
                    {
                        if (!eq)
                        {
                            if (NegateNum(lastNum))
                            {
                                errMsg = "Неверный ввод";
                                err = true;
                                lastCalc += LastCalc(lastNum.ToString(), p);
                                break;
                            }
                            else
                            {
                                result = Math.Sqrt(lastNum);
                                lastCalc += LastCalc(lastNum.ToString(), p);
                            }
                        }
                        else
                        {
                            if (NegateNum(result))
                            {
                                errMsg = "Неверный ввод";
                                err = true;
                                lastCalc = LastCalc(result.ToString(), p);
                                break;
                            }
                            else
                            {
                                lastCalc = LastCalc(result.ToString(), p);
                                result = Math.Sqrt(result);

                            }
                        }
                        lastNum = result;
                        break;
                    }
                //Обратная величина
                case "1/x":
                    {
                        lastCalc += LastCalc(lastNum.ToString(), p);
                        result = 1 / lastNum;
                        lastNum = result;
                        break;
                    }
                //Смена полярности
                case "+/-":
                    {
                        result = -lastNum;
                        lastNum = result;
                        break;
                    }
                //Проценты
                case "%":
                    {
                        if (!eq)
                        {
                            result = operation == "+" || operation == "-" ? operand1 * lastNum / 100 : lastNum / 100;
                            lastNum = result;
                            lastCalc = operation==null ? result.ToString() : lastCalc + result;
                        }
                        else
                        {
                            result = result * result / 100;
                            lastNum = result;
                            lastCalc = result.ToString();
                        }
                        //lastCalc += lastNum;
                        break;
                    }
                default:
                    break;
            }
            newNum = true; //готовность к вводу нового числа
            return lastCalc; //вернуть значение последнего вычисления
        }

        //Метод ввода числа (при нажатии числовых клавиш)
        internal string Number(object p)
        {
            //если была ошибка, выполнить сброс переменных
            if (err)
            {
                Clear();
            }

            string strNum = p.ToString();

            if (newNum && strNum != "back" && strNum != "CE") //если готовность к вводу нового числа - положительная и параметр команды не имеет значения 'back' и 'CE'
            {
                stringNum = strNum;
                //если первым введен разделитель целой и дробной частей
                if (stringNum == ",")
                {
                    stringNum = "0,";
                }

                newNum = false; //готовность к вводу нового числа - отрицательная
            }
            else //если готовность к вводу нового числа - отрицательная
            {
                if (strNum != "back") //если параметр команды не имеет значения 'back'
                {
                    if (strNum != "CE") //параметр команды не имеет значения 'CE'
                    {
                        stringNum += strNum; //добавить в конец строки (числа) цифру
                    }
                    else // если 'CE'
                    {
                        stringNum = "0"; //обнулить введенную строку (число)
                        newNum = true; //готовность к вводу нового числа - положительная
                    }
                }
                else //если 'back'
                {
                    if (stringNum.Length > 1) //пока длина строки больше 1
                    {
                        stringNum = stringNum.Remove(stringNum.Length - 1, 1); //удалить последний символ в строке (числе)
                    }
                    else //длина строки - 1 символ
                    {
                        stringNum = "0"; //присвоить значение 0
                        newNum = true; //готовность к вводу нового числа - положительная
                    }
                }
            }
            lastNum = Convert.ToDouble(stringNum); //присвоить последнему введенному числу текущее значение введенной строки (числа)
            errMsg = ""; //стереть сообщение об ошибке
            return stringNum; //вернуть текущее значение введенной строки (числа)
        }

        //Вызов результата вычислений
        internal string Result()
        {
            return errMsg != "" ? errMsg : result.ToString(); //если есть сообщение об ошибке, вернуть сообщение, иначе вернуть текущий результат вычислений
        }

        //Вызов последнего вычисления
        public string LastCalc()
        {
            return lastCalc;
        }

        //Конструктор вывода вычислений в зависимости от вида операции
        private string LastCalc(string num, string operand)
        {
            switch (operand)
            {
                //если возведение в степень
                case "x²":
                    {
                        num = "(" + num + ")²";
                        break;
                    }
                //если квадратный корень
                case "√x":
                    {
                        num = "√(" + num + ")";
                        break;
                    }
                //если обратная величина
                case "1/x":
                    {
                        num = "1/(" + num + ")";
                        break;
                    }
                //если простая арифметическая операция
                default:
                    {
                        num = num + " " + operand + " ";
                        break;
                    }
            }
            return num;
        }

        //Инициализация класса со сбросом переменных
        public Calculator()
        {
            Clear();
        }

        //Вызов очистки переменных
        public void CalcClear()
        {
            Clear();
        }

        //Проверка на ошибки вычислений
        internal bool Error()
        {
            return err;
        }

        //Проверка числа на отрицательность
        private bool NegateNum(double num)
        {
            return num < 0;
        }

        //Очистка переменных (сброс в дефолтное состояние)
        private void Clear()
        {
            operand1 = 0;
            operand2 = 0;
            operation = null;
            result = 0;
            lastNum = 0;
            stringNum = "";
            lastCalc = "";
            errMsg = "";
            err = false;
            eq = false;
            newNum = true;

        }
    }
}
