using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathQuiz.App
{
    public class Calculation
    {
        private int num1;
        private int num2;
        private string operatorSymbol;
        private double actualAnswer;
        private Random random;

        public Calculation()
        {
            num1 = 0;
            num2 = 1;
            random = new Random();
        }
                
        public string add()
        {
            getRandomNumbers(0, 100);

            if (num1 < num2)
            {
                getRandomNumbers(0, 100);
                add();
            }

            operatorSymbol = "+";

            actualAnswer = num1 + num2;

            return display();
        }

        public string subtract()
        {
            getRandomNumbers(0, 100);

            if (num1 < num2)
            {
                getRandomNumbers(0, 100);
                subtract();
            }

            operatorSymbol = "-";

            actualAnswer = num1 - num2;

            return display();
        }

        public string divide()
        {
            getRandomNumbers(1, 100);

            if (num1 < num2)
            {
                getRandomNumbers(1, 100);
                divide();
            }

            operatorSymbol = "/";

            actualAnswer = num1 / num2;

            return display();
        }

        public string multiply()
        {
            getRandomNumbers(0, 100);

            if (num1 < num2)
            {
                getRandomNumbers(0, 100);
                multiply();
            }

            operatorSymbol = "x";

            actualAnswer = num1 * num2;

            return display();
        }

        public string display()
        {
            return $"{num1} {operatorSymbol} {num2} = ";
        }

        public bool ensureAnswerCorrect(double answer)
        {
            if (actualAnswer == answer)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void getRandomNumbers(int lowerLimit, int upperLimit)
        {
            num1 = random.Next(lowerLimit, upperLimit);
            num2 = random.Next(lowerLimit, upperLimit);
        }
    }
}
