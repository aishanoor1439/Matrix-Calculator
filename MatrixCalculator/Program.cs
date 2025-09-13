using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter number of rows: ");
            int rows = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter number of columns: ");
            int cols = int.Parse(Console.ReadLine());

            Console.WriteLine("\n Enter elements of Matrix A:");
            double[,] A = InputMatrix(rows, cols);

            Console.WriteLine("\n Enter elements of Matrix B:");
            double[,] B = InputMatrix(rows, cols);

            Console.WriteLine("\n Matrix A:");
            PrintMatrix(A);
            Console.WriteLine("\n Matrix B:");
            PrintMatrix(B);

            Console.WriteLine("\n Addition (A+B):");
            PrintMatrix(AddMatrix(A, B));

            Console.WriteLine("\n Subtraction (A-B):");
            PrintMatrix(SubMatrix(A, B));

            Console.WriteLine("\n Multiplication (A*B):");
            if (A.GetLength(1) == B.GetLength(0))
            {
                Console.WriteLine("\n Multiplication (A*B):");
                PrintMatrix(MultiplyMatrix(A, B));
            }
            else
            {
                Console.WriteLine("\n Matrix multiplication isn't possible");
            }

            if (rows == cols)
            {
                Console.WriteLine("\n Determinant of Matrix A: " + Determinant(A, rows));
                Console.WriteLine("\n Determinant of Matrix B: " + Determinant(B, rows));

                Console.WriteLine("\n Inverse of Matrix A:");
                double[,] invA = Inverse(A);
                if (invA != null) PrintMatrix(invA);
                else Console.WriteLine("Inverse doesn't exist (Determinant = 0).");

                Console.WriteLine("\n Inverse of Matrix B:");
                double[,] invB = Inverse(B);
                if (invB != null) PrintMatrix(invB);
                else Console.WriteLine("Inverse doesn't exist(Determinant = 0).");
            }
            else
            {
                Console.WriteLine("\n Determinant and Inverse only valid for square matrices!");
            }
        }

        static double[,] InputMatrix(int rows, int cols)
        {
                double[,] mat = new double[rows, cols];
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        Console.Write($"Element[{i + 1},{j + 1}]: ");
                        mat[i, j] = double.Parse(Console.ReadLine());
                    }
                }
            return mat;
        }

        static void PrintMatrix(double[,] mat)
        {
            int rows = mat.GetLength(0);
            int cols = mat.GetLength(1);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(mat[i, j] + "\t");
                    Console.WriteLine();
                }
            }
        }

        static double[,] AddMatrix(double[,] A, double[,] B)
        {
            int rows = A.GetLength(0), cols = A.GetLength(1);
            double[,] result = new double[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    result[i, j] = A[i, j] + B[i, j];
            return result;
        }

        static double[,] SubMatrix(double[,] A, double[,] B)
        {
            int rows = A.GetLength(0), cols = A.GetLength(1);
            double[,] result = new double[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    result[i, j] = A[i, j] - B[i, j];
            return result;
        }

        static double[,] MultiplyMatrix(double[,] A, double[,] B)
        {
            int rows = A.GetLength(0), cols = B.GetLength(1), common = A.GetLength(1);
            double[,] result = new double[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    for (int k = 0; k < common; k++)
                        result[i, j] += A[i, k] * B[k, j];
            return result;
        }

        static double Determinant(double[,] matrix, int n)
        {
            if (n == 1)
                return matrix[0, 0];
            if (n == 2)
                return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];

            double det = 0;

            for (int p = 0; p < n; p++)
            {
                double[,] subMatrix = new double[n - 1, n - 1];
                for (int i = 1; i < n; i++)
                {
                    int colIndexx = 0;
                    for (int j = 0; j < n; j++)
                    {
                        if (j == p) continue;
                        subMatrix[i - 1, colIndexx++] = matrix[i, j];
                    }
                }
                det += matrix[0, p] * Math.Pow(-1, p) * Determinant(subMatrix, n - 1);
            }
            return det;
        }

        static double[,] Inverse(double[,] matrix)
        {
            int n = matrix.GetLength(0);
            double det = Determinant(matrix, n);
            if (det == 0) return null;

            double[,] adj = Adjoint(matrix);
            double[,] inv = new double[n, n];

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    inv[i, j] = adj[i, j] / det;

            return inv;
        }

        static double[,] Adjoint(double[,] matrix)
        {
            int n = matrix.GetLength(0);
            double[,] adj = new double[n, n];
            if (n == 1)
            {
                adj[0, 0] = 1;
                return adj;
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    double[,] sub = new double[n - 1, n - 1];
                    int row = 0;
                    for (int r = 0; r < n; r++)
                    {
                        if (r == i) continue;
                        int col = 0;
                        for (int c = 0; c < n; c++)
                        {
                            if (c == j) continue;
                            sub[row, col++] = matrix[r, c];
                        }
                        row++;
                    }
                    adj[j, i] = Math.Pow(-1, i + j) * Determinant(sub, n - 1);
                }
            }
            return adj;

        }
    }
}
