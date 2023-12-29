using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GaussAlgorithm;

public class Solver
{
    public static double[,] MatrixCreate(double[][] matrix, double[] freeMembers)
    {
        int n = matrix.Length;//количество строк
        int m = matrix[0].Length;//количество столбцов
        var size = m > n ? m : n;
        var Matrix = new double[size, size + 1];
    
        //Создание правильной матрицы
        if (m > n) //если число столбцов превышает число строк
        {
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Matrix[i, j] = i < n ? matrix[i][j] : matrix[n - 1][j];
                }

                Matrix[i, m] = i < n ? freeMembers[i] : freeMembers[n - 1];
            }
        }
        else //если число строк превышает число столбцов
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                    Matrix[i, j] = matrix[i][j];
                Matrix[i, n] = freeMembers[i];
            }
        }

        return Matrix;
    }

    public double[] Solve(double[][] matrix, double[] freeMembers)
	  {
        bool flag;
        var Matrix = MatrixCreate(matrix, freeMembers);
        var size = Matrix.GetLength(0);
        var solutions = new double[size];

        //Метод Гауса
        //Прямой ход. Зануление нижнего легвого угла
        for (int k = 1; k < size; k++)//номер строки
		    {
			    for(int j = k; j < size; j++)
                {
                    if (Matrix[k - 1, k - 1] == 0)
                    {
                        for (int l = k; l < size; l++)
                        {
                            if (Matrix[l, k - 1] != 0)
                            {
                                double num;
                                for (int i = 0; i < size + 1; i++)
                                {
                                    num = Matrix[k - 1, i];
                                    Matrix[k - 1, i] = Matrix[l, i];
                                    Matrix[l, i] = num;
                                }
                                break;
                            }
                        }
                    }

                    if (Matrix[k - 1, k - 1] != 0)
                    {
                        double mm = Matrix[j, k - 1] / Matrix[k - 1, k - 1];

                        for (int i = 0; i < size + 1; i++)
                        {
                        Matrix[j, i] = Matrix[j, i] - mm * Matrix[k - 1, i];
                        }
                    }
                }
            }

            //Если система уравнений не имеет решений
            for (int i = 0; i < size; i++)
            {
                flag = true;
                for (int j = 0; j < size; j++)
                {
                    if (Matrix[i, j] != 0)
                    {
                        flag = false;
                        break;
                    }
                }

                if (flag && Matrix[i, size] != 0)
                    throw new NoSolutionException("");
            }

            //Обратный ход. Зануление верхнего правого угла
            for (int i = size - 1; i >= 0; i--)
            {
                if(Matrix[i, i] != 0)
                {
                    solutions[i] = Matrix[i, size] / Matrix[i, i];

                    for (int c = size - 1; c > i; c--)
                    {
                        solutions[i] = solutions[i] - Matrix[i, c] * solutions[c] / Matrix[i, i];
                    }
                }
            }

            return solutions;
    	}
}
