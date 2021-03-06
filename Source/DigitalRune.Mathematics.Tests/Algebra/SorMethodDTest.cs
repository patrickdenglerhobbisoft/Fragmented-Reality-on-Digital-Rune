﻿using System;
using NUnit.Framework;


namespace DigitalRune.Mathematics.Algebra.Tests
{
  [TestFixture]
  public class SorMethodDTest
  {
    [Test]
    public void Test1()
    {
      MatrixD A = new MatrixD(new double[,] { { 4 } });
      VectorD b = new VectorD(new double[] { 20 });

      SorMethodD solver = new SorMethodD();
      VectorD x = solver.Solve(A, null, b);

      Assert.IsTrue(VectorD.AreNumericallyEqual(new VectorD(1, 5), x));
      Assert.AreEqual(2, solver.NumberOfIterations);
    }


    [Test]
    public void Test2()
    {
      MatrixD A = new MatrixD(new double[,] { { 1, 0 }, 
                                              { 0, 1 }});
      VectorD b = new VectorD(new double[] { 20, 28 });

      SorMethodD solver = new SorMethodD();
      VectorD x = solver.Solve(A, null, b);

      Assert.IsTrue(VectorD.AreNumericallyEqual(b, x));
      Assert.AreEqual(2, solver.NumberOfIterations);
    }


    [Test]
    public void Test3()
    {
      MatrixD A = new MatrixD(new double[,] { { 2, 0 }, 
                                              { 0, 2 }});
      VectorD b = new VectorD(new double[] { 20, 28 });

      SorMethodD solver = new SorMethodD();
      VectorD x = solver.Solve(A, null, b);

      Assert.IsTrue(VectorD.AreNumericallyEqual(b / 2, x));
      Assert.AreEqual(2, solver.NumberOfIterations);
    }


    [Test]
    public void Test4()
    {
      MatrixD A = new MatrixD(new double[,] { { -12, 2 }, 
                                              { 2, 3 }});
      VectorD b = new VectorD(new double[] { 20, 28 });

      SorMethodD solver = new SorMethodD();
      VectorD x = solver.Solve(A, null, b);

      VectorD solution = MatrixD.SolveLinearEquations(A, b);
      Assert.IsTrue(VectorD.AreNumericallyEqual(solution, x));
    }


    [Test]
    public void Test5()
    {
      MatrixD A = new MatrixD(new double[,] { { -21, 2, -4, 0 }, 
                                              { 2, 3, 0.1, -1 },
                                              { 2, 10, 111.1, -11 },
                                              { 23, 112, 111.1, -143 }});
      VectorD b = new VectorD(new double[] { 20, 28, -12, 0.1 });

      SorMethodD solver = new SorMethodD();
      VectorD x = solver.Solve(A, null, b);

      VectorD solution = MatrixD.SolveLinearEquations(A, b);
      Assert.IsTrue(VectorD.AreNumericallyEqual(solution, x));
    }


    [Test]
    public void Test6()
    {
      MatrixD A = new MatrixD(new double[,] { { -21, 2, -4, 0 }, 
                                              { 2, 3, 0.1, -1 },
                                              { 2, 10, 111.1, -11 },
                                              { 23, 112, 111.1, -143 }});
      VectorD b = new VectorD(new double[] { 20, 28, -12, 0.1 });

      SorMethodD solver = new SorMethodD();
      solver.MaxNumberOfIterations = 4;
      VectorD x = solver.Solve(A, null, b);

      VectorD solution = MatrixD.SolveLinearEquations(A, b);
      Assert.IsFalse(VectorD.AreNumericallyEqual(solution, x));
      Assert.AreEqual(4, solver.NumberOfIterations);

      // Compare with Gauss-Seidel. Must be equal.
      GaussSeidelMethodD gsSolver = new GaussSeidelMethodD();
      gsSolver.MaxNumberOfIterations  = 4;
      VectorD gsSolution = gsSolver.Solve(A, null, b);
      Assert.IsTrue(VectorD.AreNumericallyEqual(gsSolution, x));
    }


    [Test]
    public void Test7()
    {
      MatrixD A = new MatrixD(new double[,] { { -21, 2, -4, 0 }, 
                                              { 2, 3, 0.1, -1 },
                                              { 2, 10, 111.1, -11 },
                                              { 23, 112, 111.1, -143 }});
      VectorD b = new VectorD(new double[] { 20, 28, -12, 0.1 });

      SorMethodD solver = new SorMethodD();
      solver.Epsilon = 0.0001;
      VectorD x = solver.Solve(A, null, b);

      VectorD solution = MatrixD.SolveLinearEquations(A, b);
      Assert.IsTrue(VectorD.AreNumericallyEqual(solution, x, 0.1));
      Assert.IsFalse(VectorD.AreNumericallyEqual(solution, x));
      Assert.Greater(26, solver.NumberOfIterations); // For normal accuracy (EpsilonD) we need 26 iterations.
    }


    [Test]
    public void Test9()
    {
      MatrixD A = new MatrixD(new double[,] { { -21, 2, -4, 0 }, 
                                              { 2, 3, 0.1, -1 },
                                              { 2, 10, 111.1, -11 },
                                              { 23, 112, 111.1, -143 }});
      VectorD b = new VectorD(new double[] { 20, 28, -12, 0.1 });

      SorMethodD solver = new SorMethodD();
      solver.RelaxationFactor = 1.5;
      solver.MaxNumberOfIterations = 4;
      VectorD x = solver.Solve(A, null, b);

      VectorD solution = MatrixD.SolveLinearEquations(A, b);
      Assert.IsFalse(VectorD.AreNumericallyEqual(solution, x));
      Assert.AreEqual(4, solver.NumberOfIterations);

      // Compare with Gauss-Seidel. Should be unequal because the relaxation factor is not 1.
      GaussSeidelMethodD gsSolver = new GaussSeidelMethodD();
      solver.MaxNumberOfIterations = 4;
      VectorD gsSolution = gsSolver.Solve(A, null, b);
      Assert.IsFalse(VectorD.AreNumericallyEqual(gsSolution, x));
    }


    [Test]
    public void Test10()
    {
      MatrixD A = new MatrixD(new double[,] { { -21, 2, -4, 0 }, 
                                              { 2, 3, 0.1, -1 },
                                              { 2, 10, 111.1, -11 },
                                              { 23, 112, 111.1, -143 }});
      VectorD b = new VectorD(new double[] { 20, 28, -12, 0.1 });

      SorMethodD solver = new SorMethodD();
      solver.RelaxationFactor = 1.5;
      VectorD x = solver.Solve(A, null, b);

      VectorD solution = MatrixD.SolveLinearEquations(A, b);
      Assert.IsTrue(VectorD.AreNumericallyEqual(solution, x));
    }


    [Test]
    public void TestWarmStarting()
    {
      MatrixD A = new MatrixD(new double[,] { { -21, 2, -4, 0 }, 
                                              { 2, 3, 0.1, -1 },
                                              { 2, 10, 111.1, -11 },
                                              { 23, 112, 111.1, -143 }});
      VectorD b = new VectorD(new double[] { 20, 28, -12, 0.1 });

      SorMethodD solver = new SorMethodD();
      VectorD x = solver.Solve(A, null, b);
      int fullIterationCount = solver.NumberOfIterations;

      VectorD solution = MatrixD.SolveLinearEquations(A, b);
      Assert.IsTrue(VectorD.AreNumericallyEqual(solution, x));

      // Now test make separate solve calls with warm-starting
      solver.MaxNumberOfIterations = 3;
      x = solver.Solve(A, null, b);
      Assert.AreEqual(3, solver.NumberOfIterations);
      solver.MaxNumberOfIterations = 100;
      x = solver.Solve(A, x, b);
      Assert.AreEqual(fullIterationCount, solver.NumberOfIterations + 3);
    }


    [Test]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestArgumentNullException()
    {
      SorMethodD solver = new SorMethodD();
      VectorD x = solver.Solve(null, null, new VectorD());
    }


    [Test]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestArgumentNullException2()
    {
      SorMethodD solver = new SorMethodD();
      VectorD x = solver.Solve(new MatrixD(), null, null);
    }


    [Test]
    [ExpectedException(typeof(ArgumentException))]
    public void TestArgumentException()
    {
      SorMethodD solver = new SorMethodD();
      VectorD x = solver.Solve(new MatrixD(3, 4), null, new VectorD(3));
    }


    [Test]
    [ExpectedException(typeof(ArgumentException))]
    public void TestArgumentException2()
    {
      SorMethodD solver = new SorMethodD();
      VectorD x = solver.Solve(new MatrixD(3, 3), null, new VectorD(4));
    }


    [Test]
    [ExpectedException(typeof(ArgumentException))]
    public void TestArgumentException3()
    {
      SorMethodD solver = new SorMethodD();
      VectorD x = solver.Solve(new MatrixD(3, 3), new VectorD(4), new VectorD(3));
    }
  }
}
