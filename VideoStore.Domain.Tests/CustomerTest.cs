using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VideoStore.Domain.CustomerAgg;
using VideoStore.Domain.MovieAgg;

namespace VideoStore.Domain.Tests
{
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        public void alugar_um_filme_disponivel()
        {
            // Arrange
            var customer = new Customer();
            customer.CustomerId = 1;

            var movie = new Movie();
            movie.MovieId = 2;

            var rental = new Rental(customer.CustomerId, movie.MovieId);
            rental.ReturnDate = null;

            // Act
            customer.RentMovie(movie);

            //Assert
            Assert.IsTrue(customer.Rentals.Any(x => x.CustomerId == customer.CustomerId &&
                                                    x.MovieId == movie.MovieId &&
                                                    x.ReturnDate == null));
        }

        [TestMethod]
        public void alugar_um_filme_indisponivel()
        {
            // Arrange
            var customer = new Customer { CustomerId = 1 };

            var movie = new Movie { MovieId = 2 };

            var rental = new Rental(customer.CustomerId, movie.MovieId) { ReturnDate = null };

            movie.Rentals.Add(rental);

            DomainException expectedExcetpion = null;

            // Act
            try
            {
                customer.RentMovie(movie);
            }
            catch (DomainException domainException)
            {
                expectedExcetpion = domainException;
            }

            //Assert
            Assert.IsNotNull(expectedExcetpion);
            Assert.IsTrue(expectedExcetpion.Message == "O filme não está disponível.");
        }

        [TestMethod]
        public void devolver_um_filme_com_atraso()
        {
            // Arrange
            var customer = new Customer { CustomerId = 1 };

            var movie = new Movie { MovieId = 2 };

            var rental = new Rental(customer.CustomerId, movie.MovieId);
            rental.DueDate = rental.DueDate.AddDays(-31);

            movie.Rentals.Add(rental);

            DomainException expectedExcetpion = null;

            // Act
            try
            {
                customer.GiveBackMovieWithDelayCheck(movie);
            }
            catch (DomainException domainException)
            {
                expectedExcetpion = domainException;
            }

            //Assert
            Assert.IsNotNull(expectedExcetpion);
            Assert.IsTrue(expectedExcetpion.Message == "A devolução do filme está com atraso.");
        }

        [TestMethod]
        public void devolver_um_filme_sem_atraso()
        {
            // Arrange
            var customer = new Customer { CustomerId = 1 };

            var movie = new Movie { MovieId = 2 };

            var rental = new Rental(customer.CustomerId, movie.MovieId);
            rental.DueDate = rental.DueDate.AddDays(-30).AddHours(2);

            movie.Rentals.Add(rental);

            DomainException expectedExcetpion = null;

            // Act
            try
            {
                customer.GiveBackMovieWithDelayCheck(movie);
            }
            catch (DomainException domainException)
            {
                expectedExcetpion = domainException;
            }

            //Assert
            Assert.IsNull(expectedExcetpion);
        }

        [TestMethod]
        public void devolver_um_filme_ja_devolvido()
        {
            // Arrange
            var customer = new Customer { CustomerId = 1 };

            var movie = new Movie { MovieId = 2 };

            var rental = new Rental(customer.CustomerId, movie.MovieId);
            rental.ReturnDate = DateTime.Now.AddDays(-2);

            movie.Rentals.Add(rental);

            DomainException expectedExcetpion = null;

            // Act
            try
            {
                customer.GiveBackMovieWithDelayCheck(movie);
            }
            catch (DomainException domainException)
            {
                expectedExcetpion = domainException;
            }

            //Assert
            Assert.IsNotNull(expectedExcetpion);
            Assert.IsTrue(expectedExcetpion.Message == "Este filme já foi devolvido.");
        }
    }
}