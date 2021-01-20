using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VideoStore.Domain.CustomerAgg;
using VideoStore.Domain.MovieAgg;

namespace VideoStore.Domain.Tests
{
    [TestClass]
    public class MovieTest
    {
        [TestMethod]
        public void verificar_disponibilidade_do_filme()
        {
            // Arrange
            var movie = new Movie();
            movie.MovieId = 2;

            var rental = new Rental(1, movie.MovieId);
            rental.ReturnDate = DateTime.Now.AddDays(-1);

            movie.Rentals.Add(rental);

            // Act
            var isAvaliable = movie.IsAvailable();

            //Assert
            Assert.IsTrue(isAvaliable);
        }

        [TestMethod]
        public void verificar_indisponibilidade_do_filme()
        {
            // Arrange
            var movie = new Movie();
            movie.MovieId = 2;

            var rental = new Rental(1, movie.MovieId);
            rental.ReturnDate = null;

            movie.Rentals.Add(rental);

            // Act
            var isAvaliable = movie.IsAvailable();

            //Assert
            Assert.IsFalse(isAvaliable);
        }

        [TestMethod]
        public void verificar_devolucao_atrasada()
        {
            // Arrange
            var customer = new Customer { CustomerId = 1 };
            var movie = new Movie { MovieId = 2 };

            var rental = new Rental(customer.CustomerId, movie.MovieId);
            rental.DueDate = rental.DueDate.AddDays(-30).AddHours(-2);

            movie.Rentals.Add(rental);

            // Act
            var isDelayed = movie.DevolutionIsDelayed(customer.CustomerId);

            //Assert
            Assert.IsTrue(isDelayed);
        }

        [TestMethod]
        public void verificar_devolucao_sem_atraso()
        {
            // Arrange
            var customer = new Customer { CustomerId = 1 };
            var movie = new Movie { MovieId = 2 };

            var rental = new Rental(customer.CustomerId, movie.MovieId);
            rental.DueDate = rental.DueDate.AddDays(-30).AddMinutes(20);

            movie.Rentals.Add(rental);

            // Act
            var isDelayed = movie.DevolutionIsDelayed(customer.CustomerId);

            //Assert
            Assert.IsFalse(isDelayed);
        }
    }
}